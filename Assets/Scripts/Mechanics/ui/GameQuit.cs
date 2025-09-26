using UnityEngine;

public class GameQuit : JumpStatic
{
    protected override void Awake()
    {
        base.Awake();
        if (quitGameButton != null) quitGameButton.onClick.AddListener(QuitGame);
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}