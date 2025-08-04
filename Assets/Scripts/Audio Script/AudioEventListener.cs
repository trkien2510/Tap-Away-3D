using System.Collections;
using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver
{
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
            case GameEvent.BlockedSFX:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.blocked);
                break;
            case GameEvent.MoveSFX:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.move);
                break;
            case GameEvent.LevelCompleteSFX:
                StartCoroutine(DelaySFX(1f, AudioManager.Instance.complete));
                break;
            case GameEvent.LevelFailSFX:
                StartCoroutine(DelaySFX(1f, AudioManager.Instance.fail));
                break;
            default:
                break;
        }
    }

    IEnumerator DelaySFX(float time, AudioClip sfx)
    {
        yield return new WaitForSeconds(time);
        AudioManager.Instance.PlaySFX(sfx);
    }
}
