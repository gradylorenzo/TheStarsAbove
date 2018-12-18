using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSACore;
using TSACore.CommonData;

public class TestBall : MonoBehaviour {

    public Vector3 wantedPosition;
    public float speed;
    public bool hasDoneFirstMove = false;

    private Vector3 currentPosition;

    [Serializable]
    public struct CharacterStats
    {
        public float speed;
        public float range;
        public float MaxHP;
        public TSACore.CommonData.Types CharacterType;

        public bool canSwim;
        public bool canWalk;
        public bool canFly;
    }

    public CharacterStats stats;

    private void Awake()
    {
        EventManager.MapTileClicked += MapTileClicked;
    }

    private void MapTileClicked(GameObject go)
    {
        if (hasDoneFirstMove)
        {
            Vector2 this2DPositon = new Vector2(transform.position.x, transform.position.z);
            Vector2 other2DPosition = new Vector2(go.transform.position.x, go.transform.position.z);

            float dist = Vector2.Distance(this2DPositon, other2DPosition);

            if (dist <= stats.speed * 0.87)
            {
                if(go.GetComponent<Tile>().Type == TSACore.CommonData.Types.Water)
                {
                    if (stats.canFly || stats.canSwim)
                    {
                        wantedPosition = go.transform.position + Vector3.up;
                        EventManager.TurnComplete();
                    }
                    else
                    {
                        Debug.Log("Cannot swim or fly!");
                    }
                }
                else
                {
                    wantedPosition = go.transform.position + Vector3.up;
                    EventManager.TurnComplete();
                }
            }
            else
            {
                Debug.Log("Tile not in range! Distance: " + dist);
            }
        }
        else
        {
            wantedPosition = go.transform.position + Vector3.up;
            EventManager.TurnComplete();
            hasDoneFirstMove = true;
        }
    }

    private void FixedUpdate()
    {
        currentPosition = Vector3.Lerp(currentPosition, wantedPosition, speed);
        transform.position = currentPosition;
    }
}
