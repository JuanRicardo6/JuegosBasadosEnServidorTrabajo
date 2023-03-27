using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearMessage : MonoBehaviour
{
    [SerializeField] REvents appearMessage;
    [SerializeField] float timeAppear,timeMessage;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        appearMessage.GEvent += Message;
    }

    void Message()
    {
        StartCoroutine(AnimMessage());
    }
    IEnumerator AnimMessage()
    {
        transform.LeanScale(Vector3.one, timeAppear).setEaseOutQuart();
        yield return new WaitForSeconds(timeMessage);
        transform.LeanScale(Vector3.zero, timeAppear).setEaseOutQuart();
    }
    private void OnDestroy()
    {
        appearMessage.GEvent -= Message;
    }
}
