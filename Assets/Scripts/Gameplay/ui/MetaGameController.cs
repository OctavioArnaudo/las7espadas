using UnityEngine;

/// <summary>
/// The MetaGameController is responsible for switching control between the high level
/// contexts of the application, eg the Main Menu and Gameplay systems.
/// </summary>
public class MetaGameController : MonoController
{
    /// <summary>
    /// The main UI object which used for the menu.
    /// </summary>

    /// <summary>
    /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
    /// </summary>

    /// <summary>
    /// 
    /// A flag to indicate whether the main menu is currently shown or not.
    /// 
    /// </summary>
    /// <summary>
    /// 
    /// The input action for toggling the main menu.
    /// 
    /// </summary>

    /// <summary>
    /// 
    /// Called when the script is enabled. It initializes the main menu state and finds the input action for toggling the menu.
    /// 
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        // Initialize the main menu state
        _ToggleMenu(menuShown);
        // Find the input action for toggling the main menu
    }

    /// <summary>
    /// Turn the main menu on or off.
    /// </summary>
    /// <param name="show"></param>
    public void ToggleMenu(bool show)
    {
        // If the current state is the same as the requested state, do nothing
        if (this.menuShown != show)
        {
            // If the game controller is not null, set the game state to the requested state
            _ToggleMenu(show);
        }
    }

    /// <summary>
    /// 
    /// Internal method to toggle the main menu on or off.
    /// 
    /// </summary>
    void _ToggleMenu(bool show)
    {
        HideMenu();
        // If the requested state is the same as the current state, do nothing
        if (show)
        {
            // If the game controller is not null, set the game state to the requested state
            Time.timeScale = 0;
            // Pause the game
            foreach (var i in menuPanelCanvas.Values) i.gameObject.SetActive(true);
            // Set the main menu to active
            foreach (var i in gamePlayCanvas) i.gameObject.SetActive(false);
        }
        else
        {
            // If the requested state is false, set the game state to the requested state
            Time.timeScale = 1;
            // Resume the game
            foreach (var i in menuPanelCanvas.Values) i.gameObject.SetActive(false);
            // Set the main menu to inactive
            foreach (var i in gamePlayCanvas) i.gameObject.SetActive(true);
        }
        // Set the active state of the game play canvas objects
        this.menuShown = show;
    }

    public void ShowMenu(string panelName)
    {
        if (menuPanelCanvas.TryGetValue(panelName, out var panel))
        {
            ToggleMenu(show: !menuShown);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' no encontrado en el diccionario.");
        }
    }

    void HideMenu()
    {
        foreach (var i in menuPanelCanvas.Values) i.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// Called every frame to check for input actions.
    /// 
    /// </summary>
    protected override void Update()
    {
        base.Update();
        // If the game controller is null, return early
        if (menuAction.WasPressedThisFrame())
        {
            // If the menu action was pressed, toggle the main menu
            ToggleMenu(show: !menuShown);
        }
    }

}