using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OponnetText : MonoBehaviour
{
    [SerializeField] LobbyController controlador;
    [SerializeField] REvents partida;
    [SerializeField] Text oponente, sala;
    void Awake()
    {
        partida.GEvent += Partida;
        oponente.gameObject.SetActive(false);
        sala.gameObject.SetActive(false);
    }
    void Partida()
    {
        StartCoroutine(PartidaEncontrada());
    }
    IEnumerator PartidaEncontrada()
    {
        yield return new WaitForSeconds(6f);
        oponente.gameObject.SetActive(true);
        oponente.text = "Oponente: " + controlador.enemy;
        sala.gameObject.SetActive(true);
        sala.text = "Sala: " + controlador.gameRoom;
    }
    private void OnDestroy()
    {
        partida.GEvent -= Partida;
    }

}
