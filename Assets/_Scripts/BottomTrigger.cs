using UnityEngine;

public class BottomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.LoseBall(other.gameObject);
        }
    }
}