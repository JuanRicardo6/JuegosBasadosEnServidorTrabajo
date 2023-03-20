using Firesplash.UnityAssets.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{

    public SocketIOCommunicator sioCom;

    // Start is called before the first frame update
    void Start()
    {
        SIOAuthPayload auth = new SIOAuthPayload();

        string token = PlayerPrefs.GetString("token");

        auth.AddElement("token", token);

        sioCom.Instance.Connect(auth);

        sioCom.Instance.On("connect", (string data) => {
            Debug.Log("LOCAL: Conectado al servidor");
            //sioCom.Instance.Emit("KnockKnock");
        });

        sioCom.Instance.On("UserConnected", (string data) => {
             UserData userData = JsonUtility.FromJson<UserData>(data);
             Debug.Log(userData.username + " se ha conectado");
        });

        sioCom.Instance.On("connectionRejected", (string data) =>
        {
            Debug.Log("La conexion se ha rechazado");
        });
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteKey("token");
        PlayerPrefs.DeleteKey("username");
        SceneManager.LoadScene("Login");
    }

    public void FindMatch()
    {
        sioCom.Instance.Emit("FindMatch");
    }
}

