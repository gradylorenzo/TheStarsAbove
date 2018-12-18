using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void MapGenerationCompleteEvent(float x, float y);
    public static MapGenerationCompleteEvent MapGenerationComplete;

    public delegate void MapTileClickedEvent(GameObject go);
    public static MapTileClickedEvent MapTileClicked;

    public delegate void TurnCompletedEvent();
    public static TurnCompletedEvent TurnComplete;
}
