using System.Collections;
using TMPro;
using UnityEngine;

public class UIEventListener : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject totalMoves;
    [SerializeField] private GameObject levelFail;
    [SerializeField] private GameObject levelComplete;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            currentLevel.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.CurrentLevel;
            totalMoves.GetComponent<TextMeshProUGUI>().text = "Move limit: " + GameManager.Instance.TotalMoves.ToString();
        }
    }

    private void OnEnable()
    {
        RegisterObservers();
    }

    public void RegisterObservers()
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
            case GameEvent.Complete:
                StartCoroutine(ShowPanel(levelComplete));
                break;
            case GameEvent.Fail:
                StartCoroutine(ShowPanel(levelFail));
                break;
            default:
                break;
        }
    }

    IEnumerator ShowPanel(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(true);
        GameManager.Instance.IsProcessing = true;
        GameManager.Instance.IsLoading = true;
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
        GameManager.Instance.IsProcessing = false;
        GameManager.Instance.IsLoading = false;
        if (obj == levelComplete)
        {
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
        else if (obj == levelFail)
        {
            FindObjectOfType<LevelLoader>().ReloadLevel();
        }
    }
}
