using UnityEngine;

[ExecuteInEditMode]
public class TotalMove : MonoBehaviour
{
    private int totalMoves = 0;
    [SerializeField] private Difficulty difficulty;

    public enum Difficulty
    {
        easy,
        medium,
        hard
    }

    private void Start()
    {
        ChangeDifficulty();
    }

    private void OnEnable()
    {
        ChangeDifficulty();
    }

    private void ChangeDifficulty()
    {
        if (difficulty == Difficulty.easy)
        {
            AddMoves(5);
        }
        else if (difficulty == Difficulty.medium)
        {
            AddMoves(3);
        }
        else if (difficulty == Difficulty.hard)
        {
            AddMoves(1);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.TotalMoves = totalMoves;
        }
    }

    private void AddMoves(int num)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf && !child.GetComponent<BlockProperties>().isHardBlock)
            {
                num++;
            }
        }
        totalMoves = num;
    }
}