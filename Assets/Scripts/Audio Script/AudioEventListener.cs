using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver
{
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
            case GameEvent.BlockedSFX:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.blocked);
                break;
            case GameEvent.MoveSFX:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.move);
                break;
            default:
                break;
        }
    }
}
