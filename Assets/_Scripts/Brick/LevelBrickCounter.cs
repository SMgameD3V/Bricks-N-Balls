using UnityEngine;

public class LevelBrickCounter : MonoBehaviour
{
    private void Start()
    {
        int count = FindObjectsByType<Brick>(FindObjectsSortMode.None).Length;
        GameManager.Instance.SetTotalBricks(count);
    }
}