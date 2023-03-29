using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AncientMessageController : MonoBehaviour
{
    [SerializeField] REvents bienvenido, jugadorNuevo, jugadorMenos, rechazado, partida;
    [SerializeField] float tiempoDesplazamiento, duracionMensaje;
    [SerializeField] Vector3 posIni, posMensaje, posF;
    [SerializeField] Text mensaje;
    void Start()
    {
        transform.localPosition = posIni;
        mensaje.text = "";
        bienvenido.GEvent += Bienvenido;
        jugadorNuevo.GEvent += JugadorNuevo;
        jugadorMenos.GEvent += JugadorMenos;
        rechazado.GEvent += Rechazado;
        partida.GEvent += Partida;
    }
    void Bienvenido()
    {
        mensaje.text = "Bienvenido al reino de los cielos campeon!";
        CloudAnim();
    }
    void JugadorNuevo()
    {
        mensaje.text = "un nuevo Arcangel se ha unido!";
        CloudAnim();
    }
    void JugadorMenos()
    {
        mensaje.text = "un Arcangel ha caido de los cielos!";
        CloudAnim();
    }
    void Rechazado()
    {
        mensaje.text = "la entrada al cielo no se te ha sido concedida";
        CloudAnim();
    }
    void Partida()
    {
        mensaje.text = "Lucha por sobrevivir!";
        CloudAnim();
    }
    void CloudAnim()
    {
        StartCoroutine(Desplazamiento());
    }
    IEnumerator Desplazamiento()
    {
        transform.LeanMoveLocal(posMensaje,tiempoDesplazamiento).setEaseOutQuart();
        yield return new WaitForSeconds(duracionMensaje);
        transform.LeanScale(Vector3.zero, tiempoDesplazamiento).setEaseOutQuart();
        yield return new WaitForSeconds(tiempoDesplazamiento+1f);
        transform.localPosition = posIni;
        transform.localScale = Vector3.one;
    }
    private void OnDestroy()
    {
        bienvenido.GEvent -= Bienvenido;
        jugadorNuevo.GEvent -= JugadorNuevo;
        jugadorMenos.GEvent -= JugadorMenos;
        rechazado.GEvent -= Rechazado;
        partida.GEvent -= Partida;
    }
}
//                .
//                .
//      666       .
//                .
//            \   .   /
//             \  .  /
//              \ . /
//               \./
//                +












































































































































































































































































































































































































































































































































































































    //el secreto esta en la nube antes de iniciar