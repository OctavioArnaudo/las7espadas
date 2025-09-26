using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple controller for switching between UI panels.
/// </summary>
public class InitPanel : InitMixer
{
    [Header("Panels")]
    [SerializeField] public GameObject startPanelObject;
    [SerializeField] public GameObject optionsPanelObject;
    [SerializeField] public GameObject profilePanelObject;
    [SerializeField] public GameObject worldsPanelObject;
    [SerializeField] public GameObject levelsPanelObject;
    [SerializeField] public GameObject gamePanelObject;
    [SerializeField] public GameObject stopPanelObject;

    protected Dictionary<string, GameObject> panelDictionary;
    /// <summary>
    /// 
    /// An array of panels that can be activated or deactivated.
    /// 
    /// </summary>
    public GameObject[] panels;

    /// <summary>
    /// 
    /// Sets the active panel based on the index provided.
    /// 
    /// </summary>
    public void SetActivePanel(int index)
    {
        // Check if the index is out of bounds
        for (var i = 0; i < panels.Length; i++)
        {
            // If the index is out of bounds, return early
            var active = i == index;
            // Set the active state of the panel based on the index
            var g = panels[i];
            // If the panel is not null and its active state is not equal to the desired active state, set its active state
            if (g.activeSelf != active) g.SetActive(active);
        }
    }

    /// <summary>
    /// 
    /// Called when the script is enabled.
    /// 
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        // When the script is enabled, set the active panel to the first one
        SetActivePanel(0);
    }
}