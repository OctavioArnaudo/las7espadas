using System.Collections;
using UnityEngine;

public class GameQuit : ActionReleased
{
    protected override void Awake()
    {
        base.Awake();
        if (quitGameButton != null) quitGameButton.onClick.AddListener(_QuitGame);
    }
    private void _QuitGame() {        
        RestartCoroutine(QuitGame());
    }
    public virtual IEnumerator QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        yield return null;
    }
}