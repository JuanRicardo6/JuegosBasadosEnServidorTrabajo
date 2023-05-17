using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.UnityAssets.SocketIO;

public class MoveBlock : MonoBehaviour
{
    public SocketIOCommunicator sioCom;

    public AxisData Axis { get; set; }

    private void Start()
    {
        Axis = new AxisData();
    }

    void Update()
    {
        // Mover hacia arriba al presionar el botón "Arriba"
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Axis.vertical = -1;
        }

        // Mover hacia abajo al presionar el botón "Abajo"
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Axis.vertical = 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            sioCom.Instance.Emit("move", JsonUtility.ToJson(Axis), false);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            Axis.vertical = 0;
            sioCom.Instance.Emit("move", JsonUtility.ToJson(Axis), false);
        }
    }
}

public class AxisData
{
    public int vertical;
}
