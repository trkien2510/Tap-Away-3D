using UnityEngine;

[ExecuteInEditMode]
public class TotalMoves : MonoBehaviour
{
    private int totalMoves = 0;
    [SerializeField] private MoveBonus moveBonus;

    public enum MoveBonus
    {
        Six,
        Five,
        Four,
        Three,
        Two,
        One,
    }

    private void Start()
    {
        ChangeDifficulty();
    }

    private void OnEnable()
    {
        ChangeDifficulty();
    }

    public void ChangeDifficulty()
    {
        switch (moveBonus)
        {
            case MoveBonus.Six:
                AddMoves(6);
                break;
            case MoveBonus.Five:
                AddMoves(5);
                break;
            case MoveBonus.Four:
                AddMoves(4);
                break;
            case MoveBonus.Three:
                AddMoves(3);
                break;
            case MoveBonus.Two:
                AddMoves(2);
                break;
            case MoveBonus.One:
                AddMoves(1);
                break;
            default:
                break;
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
            var blockProps = child.GetComponent<BlockProperties>();
            if (child.gameObject.activeSelf && blockProps != null && !blockProps.isHardBlock)
            {
                num++;
            }
        }
        totalMoves = num;
    }
}