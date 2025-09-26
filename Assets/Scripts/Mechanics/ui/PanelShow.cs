using UnityEngine;
using System.Collections.Generic;

public class PanelShow : GameReload
{
    protected override void Awake()
    {
        base.Awake();
        panelDictionary = new Dictionary<string, GameObject>
        {
            { "Start", startPanelObject },
            { "Options", optionsPanelObject },
            { "Profile", profilePanelObject },
            { "Worlds", worldsPanelObject },
            { "Levels", levelsPanelObject },
            { "Game", gamePanelObject },
            { "Stop", stopPanelObject }
        };
    }

    protected override void Start()
    {
        base.Start();
        // ShowPanel("Game");
    }

    void HidePanelObjects()
    {
        foreach (GameObject panel in panelDictionary.Values)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }

    public void ShowPanel(string panelName)
    {
        HidePanelObjects();
        if (panelDictionary.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' no encontrado en el diccionario.");
        }
    }
}