using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private List<GameObject> listLevels = new List<GameObject>();

    private void Awake()
    {
        LoadLevelToList();
    }

    private void LoadLevelToList()
    {
        foreach (Transform child in transform)
        {
            listLevels.Add(child.gameObject);
        }
    }

    public void LoadNextLevel()
    {
        if (GameManager.Instance.IsLoading) return;
        if (GameManager.Instance.IsProcessing) return;
        for (int i = 0; i < listLevels.Count; i++)
        {
            if (listLevels[i].activeSelf)
            {
                int nextIndex = (i + 1) % listLevels.Count;
                LoadLevel(listLevels[i], nextIndex);
                break;
            }
        }
    }

    public void LoadPreviousLevel()
    {
        if (GameManager.Instance.IsLoading) return;
        if (GameManager.Instance.IsProcessing) return;
        for (int i = 0; i < listLevels.Count; i++)
        {
            if (listLevels[i].activeSelf)
            {
                int previousIndex = (i - 1 + listLevels.Count) % listLevels.Count;
                LoadLevel(listLevels[i], previousIndex);
                break;
            }
        }
    }

    public void ReloadLevel()
    {
        if (GameManager.Instance.IsLoading) return;
        if (GameManager.Instance.IsProcessing) return;
        for (int i = 0; i < listLevels.Count; i++)
        {
            if (listLevels[i].activeSelf)
            {
                listLevels[i].SetActive(false);
                LoadLevel(listLevels[i], i);
                FindObjectOfType<TotalMoves>().ChangeDifficulty();
                break;
            }
        }
    }

    private void LoadLevel(GameObject level, int index)
    {
        foreach (Transform lvl in level.transform)
        {
            lvl.gameObject.GetComponent<MoveBlock>().isMoving = false;
        }
        level.transform.rotation = Quaternion.Euler(0, 0, 0);
        level.SetActive(false);
        listLevels[index].SetActive(true);
        EnableChild(listLevels[index]);
        listLevels[index].transform.rotation = Quaternion.Euler(45, 45, 0);
        GameManager.Instance.CurrentLevel = listLevels[index].name;
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
