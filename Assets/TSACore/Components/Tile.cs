using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSACore;
using TSACore.CommonData;

public class Tile : MonoBehaviour {

    public TSACore.CommonData.Types Type;
    public string TileID;

    public void Start()
    {
        TileID = new System.Guid().ToString();
    }

    public void OnMouseUpAsButton()
    {
        EventManager.MapTileClicked(gameObject);
    }
}
