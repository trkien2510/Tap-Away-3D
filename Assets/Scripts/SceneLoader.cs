using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Subject
{
    private void OnEnable()
    {
        FindAnyObjectByType<AudioEventListener>().RegisterObservers();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickSFX()
    {
        NotifyObserver(GameEvent.ClickSFX);
    }
}