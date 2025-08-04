using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;


    private void Start()
    {
        ReloadLevel();
    }

    public void LoadNextLevel()
    {
        if (GameManager.Instance.IsLoading) return;
        foreach (GameObject level in levels)
        {
            if (level.activeSelf)
            {
                int nextIndex = (System.Array.IndexOf(levels, level) + 1) % levels.Length;
                LoadLevel(level, nextIndex);
                break;
            }
        }
    }

    public void LoadPreviousLevel()
    {
        if (GameManager.Instance.IsLoading) return;
        foreach (GameObject level in levels)
        {
            if (level.activeSelf)
            {
                int previousIndex = (System.Array.IndexOf(levels, level) - 1 + levels.Length) % levels.Length;
                LoadLevel(level, previousIndex);
                break;
            }
        }
    }

    public void ReloadLevel()
    {
        foreach (GameObject level in levels)
        {
            if (level.activeSelf)
            {
                level.SetActive(false);
                LoadLevel(level, System.Array.IndexOf(levels, level));
                break;
            }
        }
        FindObjectOfType<TotalMoves>().ChangeDifficulty();
    }

    private void LoadLevel(GameObject level, int index)
    {
        foreach (Transform lvl in level.transform)
        {
            lvl.gameObject.GetComponent<MoveBlock>().isMoving = false;
        }
        level.transform.rotation = Quaternion.Euler(0, 0, 0);
        level.SetActive(false);
        levels[index].SetActive(true);
        EnableChild(levels[index]);
        levels[index].transform.rotation = Quaternion.Euler(45, 45, 0);
        GameManager.Instance.CurrentLevel = levels[index].name;
        GameManager.Instance.IsLoading = false;
        GameManager.Instance.IsProcessing = false;
    }

    private void EnableChild(GameObject level)
    {
        foreach (Transform child in level.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
