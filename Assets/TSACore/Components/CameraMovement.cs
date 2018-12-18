using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float Speed;
    public float Height;
    public Vector2 Limit;
    public int currentTurn = 1;
    private Vector3 position;

    private void Awake()
    {
        EventManager.MapGenerationComplete += MapGenerationComplete;
        EventManager.MapTileClicked += MapTileClicked;
        EventManager.TurnComplete += TurnComplete;
    }

    private void TurnComplete()
    {
        currentTurn++;
    }

    private void MapTileClicked(GameObject go)
    {
        Debug.Log(go.GetComponent<Tile>().Type.ToString() + " tile clicked!");
    }

    private void MapGenerationComplete(float x, float y)
    {
        transform.position = new Vector3((x / 2) * 0.75f, transform.position.y, (y / 2) * 0.25f);
        Limit = new Vector2((x * 0.75f) - 4.5f, y - 25);
    }

    private void FixedUpdate()
    {
        float x, z;

        x = Mathf.Clamp(transform.position.x + (Input.GetAxis("Horizontal") * Speed), 4.5f, Limit.x);
        z = Mathf.Clamp(transform.position.z + (Input.GetAxis("Vertical") * Speed), -3, Limit.y);

        Vector3 newPosition = new Vector3(x, Height, z);

        transform.position = newPosition;
    }
}
