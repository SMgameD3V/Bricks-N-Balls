using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float edgeMargin = 0.1f;
    private Camera cam;
    private Collider2D col;
    private bool wasPressedLastFrame;
    private float previousX;
    private float currentSwipeVelocityX;

    private void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
        previousX = transform.position.x;
    }

    private void Update()
    {
        bool isPressedThisFrame = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            MovePaddleToScreenX(touch.position.x);
            isPressedThisFrame = touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled;
        }
        else if (Input.GetMouseButton(0))
        {
            MovePaddleToScreenX(Input.mousePosition.x);
            isPressedThisFrame = true;
        }

        currentSwipeVelocityX = (transform.position.x - previousX) / Time.deltaTime;
        previousX = transform.position.x;

        if (wasPressedLastFrame && !isPressedThisFrame)
            GameManager.Instance.TryLaunchPendingBall(currentSwipeVelocityX);

        wasPressedLastFrame = isPressedThisFrame;
    }

    private void MovePaddleToScreenX(float screenX)
    {
        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenX, 0, 0));

        // Read the CURRENT half-width every call, not a cached value from Start() —
        // this is what keeps the clamp correct while Grow/Shrink is active.
        float halfPaddleWidth = col.bounds.extents.x;
        float limit = ScreenBounds.HalfWidth - halfPaddleWidth - edgeMargin;

        float clampedX = Mathf.Clamp(worldPoint.x, -limit, limit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}