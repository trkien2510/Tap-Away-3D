using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MoveBlock))]
public class BlockProperties : Subject, IObserver
{
    private Material blockMaterial;

    [Header("Block Direction")]
    [SerializeField] private Directions direction;
    private Directions lastDirection;

    [Header("Hard Block")]
    public bool isHardBlock = false;

    [Header("Block Counting")]
    [HideInInspector] public bool isBlockCounting = false;
    [Range(0, 10)] [SerializeField] private int blockCounting = 0;

    [System.Serializable]
    public enum Directions
    {
        Forward,
        Backward,
        Left,
        Right,
        Up,
        Down
    }

    private void OnEnable()
    {
        blockMaterial = GetComponent<Renderer>().sharedMaterial;

        foreach (var subject in FindObjectsOfType<Subject>())
        {
            subject.AddObserver(this);
        }
    }

    private void OnDisable()
    {
        foreach (var subject in FindObjectsOfType<Subject>())
        {
            subject.RemoveObserver(this);
        }
    }

    private void OnValidate()
    {
        if (direction != lastDirection)
        {
            ApplyDirection();
            lastDirection = direction;
        }

        EditorApplication.delayCall += () =>
        {
            BlockType();
        };
    }

    public void OnNotify(GameEvent action)
    {
        if (action == GameEvent.MinusCounting)
        {
            blockCounting = (blockCounting >= 1) ? blockCounting-- : 0;
            BlockType();
        }
    }

    private void ApplyDirection()
    {
        switch (direction)
        {
            case Directions.Forward:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Directions.Backward:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Directions.Left:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Directions.Right:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Directions.Up:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Directions.Down:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            default:
                break;
        }
    }

    private void BlockType()
    {
        if (this == null) return;

        isBlockCounting = (blockCounting >= 1);
        if (blockCounting >= 1)
        {
            isHardBlock = false;
        }

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Counting"))
            {
                child.gameObject.GetComponent<TextMeshPro>().text = isBlockCounting ? blockCounting.ToString() : string.Empty;
                child.gameObject.SetActive(!isHardBlock);
            }
        }

        bool isBlocked;
        if (isBlockCounting || isHardBlock)
        {
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }
        CantMoveBlock(isBlocked);
    }

    private void CantMoveBlock(bool boolean)
    {
        if (blockMaterial != null)
        {
            blockMaterial.color = boolean ? Color.gray : Color.white;
        }

        GetComponent<MoveBlock>().enabled = !boolean;

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Plane"))
            {
                child.gameObject.SetActive(!boolean);
            }
        }
    }
}
