using Firesplash.UnityAssets.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public SocketIOCommunicator sioCom;

    public int maxUsers = 6;
    [SerializeField] List<Text> userTexts = new List<Text>();
    [SerializeField] Text winnerText;
    [SerializeField] Text searchText;
    [SerializeField] Button searchButton;
    [SerializeField] Button cancelSearchButton;
    [SerializeField] REvents bienvenido, jugadorNuevo, jugadorMenos, rechazado, partida;
    [SerializeField] GameObject lobbyCanvas;
    [SerializeField] GameObject gameCanvas;
    public string gameRoom,player1,player2;

    void Start()
    {
        sioCom.Instance.On("connect", (string data) => {
            Debug.Log("LOCAL: Conectado al servidor");
            //bienvenido
            bienvenido.FireEvent();
            sioCom.Instance.Emit("KnockKnock");
        });

        sioCom.Instance.On("connectionRejected", (string data) =>
        {
            Debug.Log("La conexion se ha rechazado");
            //rechazado
            rechazado.FireEvent();
        });

        sioCom.Instance.On("UserConnected", (string data) =>
        {
            ListData resData = JsonUtility.FromJson<ListData>(data);
            UpdateTextList(resData);
            //jugador se ha unido
            jugadorNuevo.FireEvent();
        });

        SIOAuthPayload auth = new SIOAuthPayload();

        string token = PlayerPrefs.GetString("token");

        auth.AddElement("token", token);

        sioCom.Instance.Connect(auth);

        sioCom.Instance.On("UserDisconnected", (string data) =>
        {
            ListData resData = JsonUtility.FromJson<ListData>(data);
            UpdateTextList(resData);
            //jugador se ha ido
            jugadorMenos.FireEvent();
        });

        sioCom.Instance.On("WaitingForMatch", (string data) =>
        {
            searchText.gameObject.SetActive(true);
            Debug.Log("Estás en cola de espera. Buscando partida...");
            //anim searching
        });

        sioCom.Instance.On("MatchReady", (string data) =>
        {
            searchText.gameObject.SetActive(false);
            searchButton.gameObject.SetActive(false);
            cancelSearchButton.gameObject.SetActive(false);
            MatchData matchData = JsonUtility.FromJson<MatchData>(data);
            gameRoom = matchData.gameRoom;
            player1 = matchData.player1;
            player2 = matchData.player2;
            Debug.Log("Lucha por sobrevivir!" + " Te has unido a la sala: " + gameRoom + " Los luchadores son: " + player1 + " y " + player2);
            //mensaje pelea
            //partida.FireEvent();
            lobbyCanvas.gameObject.SetActive(false);
            gameCanvas.gameObject.SetActive(true);
        });

        sioCom.Instance.On("MatchCanceled", (string data) =>
        {
            searchText.gameObject.SetActive(false);
            Debug.Log("Búsqueda cancelada.");
            //stop searching
        });

        sioCom.Instance.On("gameOver", (string data) =>
        {
            winnerText.text = "El ganador es: " + data.ToString();
            winnerText.gameObject.SetActive(true);
            StartCoroutine(GameOver());
        });
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);

        winnerText.gameObject.SetActive(false);
        lobbyCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        searchButton.gameObject.SetActive(true);
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

    public void StopSearchMatch()
    {
        sioCom.Instance.Emit("CancelMatchmaking");
    }

    private void UpdateTextList(ListData listData)
    {
        if (userTexts.Count > maxUsers)
        {
            Debug.Log("El servidor se encuentra lleno. Intenta más tarde.");
            //mensaje de salir
            LogOut();
        }
        else
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
}

[System.Serializable]
public class ListData
{
    public List<OnlineUsers> onlineUsers;
}

[System.Serializable]
public class MatchData
{
    public string gameRoom;
    public string player1;
    public string player2;
}

[System.Serializable]
public class OnlineUsers
{
    public string username;
}

