using Firesplash.UnityAssets.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncController : MonoBehaviour
{
    public SocketIOCommunicator sioCom;

    public GameObject player1;
    public GameObject player2;
    public GameObject ball;
    public Text scorePlayer1;
    public Text scorePlayer2;

    void Start()
    {
        sioCom.Instance.On("updateState", (string data) =>
        {
            StateData state = JsonUtility.FromJson<StateData>(data);

            Vector3 position = new Vector3(state.player1.x, -state.player1.y,-1);
            player1.transform.position = position;

            position = new Vector3(state.player2.x, -state.player2.y,-1);
            player2.transform.position = position;

            Vector3 ballPos = new Vector3(state.ball.x, -state.ball.y);
            ball.transform.position = ballPos;

            scorePlayer1.text = state.ball.scorePlayer1.ToString();
            scorePlayer2.text = state.ball.scorePlayer2.ToString();
        });
    }
}

[System.Serializable]
public class StateData
{
    public PlayerData player1;
    public PlayerData player2;
    public BallData ball;
}

[System.Serializable]
public class PlayerData
{
    public int id;
    public int x, y;
}

[System.Serializable]
public class BallData
{
    public int scorePlayer1, scorePlayer2;
    public int x, y;
}
