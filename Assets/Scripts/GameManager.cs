using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string currentLevel = "Level 1";
    private int totalMoves;
    private bool isProcessing;
    private bool isLoading;

    public string CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int TotalMoves { get => totalMoves; set => totalMoves = value; }
    public bool IsProcessing { get => isProcessing; set => isProcessing = value; }
    public bool IsLoading { get => isLoading; set => isLoading = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
