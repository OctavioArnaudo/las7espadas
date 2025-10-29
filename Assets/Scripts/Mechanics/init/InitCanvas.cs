using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InitCanvas : InitCamera
{
    public Dictionary<string, Canvas> menuPanelCanvas;
    public Canvas[] gamePlayCanvas;

    public Canvas levelsCanvas;
    public Canvas optionsCanvas;
    public Canvas profileCanvas;
    public Canvas startCanvas;
    public Canvas stopCanvas;
    public Canvas worldsCanvas;

    protected override void Awake()
    {
        base.Awake();
        menuPanelCanvas = new Dictionary<string, Canvas>
        {
            { "Game", null },
            { "Levels", null },
            { "Options", null },
            { "Profile", null },
            { "Start", null },
            { "Stop", null },
            { "Worlds", null }
        };
    }

}