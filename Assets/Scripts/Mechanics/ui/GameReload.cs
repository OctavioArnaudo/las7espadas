using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReload : GameQuit
{
    protected override void Awake()
    {
        base.Awake();
        if (reloadSceneButton != null) reloadSceneButton.onClick.AddListener(ReloadGame);
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}