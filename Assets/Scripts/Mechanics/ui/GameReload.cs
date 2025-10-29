using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReload : GameQuit
{
    protected override void Awake()
    {
        base.Awake();
        if (reloadSceneButton != null) reloadSceneButton.onClick.AddListener(_ReloadGame);
    }

    private void _ReloadGame()
    {
        RestartCoroutine(ReloadGame());
    }

    public virtual IEnumerator ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }
}