using System.Collections;
using UnityEngine;

public class PaddleScaler : MonoBehaviour
{
    [Header("Absolute X scale values (Y and Z stay untouched)")]
    [SerializeField] private float growScaleX = 0.2f;
    [SerializeField] private float shrinkScaleX = 0.1f;

    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void GrowFor(float duration) => ApplyScale(growScaleX, duration);
    public void ShrinkFor(float duration) => ApplyScale(shrinkScaleX, duration);

    private void ApplyScale(float targetX, float duration)
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine); // restarting = duration reset
        scaleRoutine = StartCoroutine(ScaleRoutine(targetX, duration));
    }

    private IEnumerator ScaleRoutine(float targetX, float duration)
    {
        transform.localScale = new Vector3(targetX, originalScale.y, originalScale.z);
        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
        scaleRoutine = null;
    }
}