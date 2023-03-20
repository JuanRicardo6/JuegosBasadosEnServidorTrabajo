using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    public string url = "http://localhost:3000";
    public string Username { get; set; }
    public string Token { get; set; }
    void Start()
    {
        Token = PlayerPrefs.GetString("token");
        Username = PlayerPrefs.GetString("username");

        if (Token == null || Token == "")
        {
            Debug.Log("No hay Token");
        }
        else
        {
            Debug.Log(Token);
            Debug.Log(Username);

            StartCoroutine(CheckToken());
        }
    }

    public async void SignUpClick()
    {
        AuthData userdata = new AuthData();

        userdata.email = GameObject.Find("InputFieldEmailLogin").GetComponent<InputField>().text;
        userdata.password = GameObject.Find("InputFieldPasswordLogin").GetComponent<InputField>().text;
        userdata.username = GameObject.Find("InputFieldUsername").GetComponent<InputField>().text;

        string postdata = JsonUtility.ToJson(userdata);

        StartCoroutine(SignUpPost(postdata));
    }

    public async  void LoginClick()
    {
        AuthData userdata = new AuthData();

        userdata.email = GameObject.Find("InputFieldEmailLogin").GetComponent<InputField>().text;
        userdata.password = GameObject.Find("InputFieldPasswordLogin").GetComponent<InputField>().text;

        string postdata = JsonUtility.ToJson(userdata);

        StartCoroutine(LoginPost(postdata));
    }

    IEnumerator SignUpPost(string postData)
    {
        UnityWebRequest request = UnityWebRequest.Put(url + "/auth/signup", postData);
        request.method = "POST";
        request.SetRequestHeader("content-type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("NETWORK ERROR :" + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            if (request.responseCode == 200)
            {
                AuthData authData = JsonUtility.FromJson<AuthData>(request.downloadHandler.text);

                Debug.Log("Te has registrado " + authData.user.username);

                PlayerPrefs.SetString("token", authData.token);
                PlayerPrefs.SetString("username", authData.user.username);

                StartCoroutine(LoginPost(postData));
            }
            else
            {
                string mensaje = "Status :" + request.responseCode;
                mensaje += "\ncontent-type:" + request.GetResponseHeader("content-type");
                mensaje += "\nError :" + request.error;
                Debug.Log(mensaje);
            }
        }
    }

    IEnumerator LoginPost(string postData)
    {
        UnityWebRequest request = UnityWebRequest.Put(url + "/auth/login", postData);
        request.method = "POST";
        request.SetRequestHeader("content-type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("NETWORK ERROR :" + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            if(request.responseCode == 200)
            {
                AuthData authData = JsonUtility.FromJson<AuthData>(request.downloadHandler.text);
                Debug.Log("Bienvenido " + authData.user.username + ", id:" + authData.user.id);
                Debug.Log("TOKEN: " + authData.token);

                PlayerPrefs.SetString("token", authData.token);
                PlayerPrefs.SetString("username", authData.user.username);

                SceneManager.LoadScene("Lobby");

            }
            else
            {
                string mensaje = "Status :" + request.responseCode;
                mensaje += "\ncontent-type:" + request.GetResponseHeader("content-type");
                mensaje += "\nError :" + request.error;
                Debug.Log(mensaje);
            }
        }
    }

    IEnumerator CheckToken()
    {
        UnityWebRequest request = UnityWebRequest.Put(url + "/auth/check","{}");
        request.method = "POST";
        request.SetRequestHeader("content-type", "application/json");
        request.SetRequestHeader("x-token", Token);

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("NETWORK ERROR :" + request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                AuthData authData = JsonUtility.FromJson<AuthData>(request.downloadHandler.text);

                SceneManager.LoadScene("Lobby");
            }
        }

    }

}

[System.Serializable]
public class AuthData
{
    public string email;
    public string username;
    public string password;
    public string token;
    public UserData user;
}

[System.Serializable]
public class UserData
{
    public int id;
    public string username;
}

