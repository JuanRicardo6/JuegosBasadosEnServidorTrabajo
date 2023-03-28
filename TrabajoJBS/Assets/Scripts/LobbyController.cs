using Firesplash.UnityAssets.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public SocketIOCommunicator sioCom;

    //public int maxUsers = 6;
    [SerializeField] List<Text> userTexts = new List<Text>();

    void Start()
    {
        sioCom.Instance.On("connect", (string data) => {
            Debug.Log("LOCAL: Conectado al servidor");
            sioCom.Instance.Emit("KnockKnock");
        });

        sioCom.Instance.On("connectionRejected", (string data) =>
        {
            Debug.Log("La conexion se ha rechazado");
        });

        sioCom.Instance.On("UserConnected", (string data) =>
        {
            ListData resData = JsonUtility.FromJson<ListData>(data);
            UpdateTextList(resData);
        });

        SIOAuthPayload auth = new SIOAuthPayload();

        string token = PlayerPrefs.GetString("token");

        auth.AddElement("token", token);

        sioCom.Instance.Connect(auth);

        sioCom.Instance.On("UserDisconnected", (string data) =>
        {
            ListData resData = JsonUtility.FromJson<ListData>(data);
            UpdateTextList(resData);
        });

        sioCom.Instance.On("MatchReady", (string data) =>
        {
            MatchData matchData = JsonUtility.FromJson<MatchData>(data);
            Debug.Log("Lucha por sobrevivir!" + " Tu oponente es: " + matchData.opponent);
        });
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteKey("token");
        PlayerPrefs.DeleteKey("username");
        sioCom.Instance.Close();
        SceneManager.LoadScene("Login");
    }

    public void FindMatch()
    {
        sioCom.Instance.Emit("FindMatch");
    }

    private void UpdateTextList(ListData listData)
    {
        for (int i = 0; i < userTexts.Count; i++)
        {
            if (i < listData.onlineUsers.Count)
            {
                userTexts[i].text = listData.onlineUsers[i].username;
            }
            else
            {
                userTexts[i].text = "";
            }
        }
    }
}

[System.Serializable]
public class ListData
{
    public List<OnlineUsers> onlineUsers;
}

[System.Serializable]
public class MatchData
{
    public string opponent;
}

[System.Serializable]
public class OnlineUsers
{
    public string username;
}

