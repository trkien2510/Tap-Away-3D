using UnityEngine;

public class LevelManager : Subject
{
    private bool sended = false;

    private void OnEnable()
    {
        sended = false;
        FindAnyObjectByType<BlockProperties>().RegisterObservers();
        FindAnyObjectByType<UIEventListener>().RegisterObservers();
        FindAnyObjectByType<AudioEventListener>().RegisterObservers();
    }

    void Update()
    {
        if (IsLevelComplete() && GameManager.Instance.TotalMoves >= 0 && !sended)
        {
            NotifyObserver(GameEvent.Complete);
            NotifyObserver(GameEvent.LevelCompleteSFX);
            sended = true;
        }
        else if (!IsLevelComplete() && GameManager.Instance.TotalMoves <= 0 && !sended)
        {
            NotifyObserver(GameEvent.Fail);
            NotifyObserver(GameEvent.LevelFailSFX);
            sended = true;
        }
    }

    private bool IsLevelComplete()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf &&
                child.GetComponent<BlockProperties>() != null &&
                !child.GetComponent<BlockProperties>().isHardBlock &&
                !child.GetComponent<MoveBlock>().isMoving)
            {
                return false;
            }
        }
        return true;
    }
}
