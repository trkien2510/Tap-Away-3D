using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject totalMoves;
    [SerializeField] private GameObject levelFalse;
    [SerializeField] private GameObject levelComplete;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            currentLevel.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.CurrentLevel;
            totalMoves.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.TotalMoves.ToString();
        }
    }

    private void OnEnable()
    {
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

    public void OnNotify(GameEvent action)
    {
        switch (action)
        {
            case GameEvent.MinusMove:
                GameManager.Instance.TotalMoves -= 1;
                break;
            default:
                break;
        }
    }
}
