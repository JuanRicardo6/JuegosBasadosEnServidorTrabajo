using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchingAnim : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(0.5f);
        text.text = "Searching.";
        yield return new WaitForSeconds(0.5f);
        text.text = "Searching..";
        yield return new WaitForSeconds(0.5f);
        text.text = "Searching...";
        StartCoroutine(Anim());
    }
    private void OnEnable()
    {
        StartCoroutine(Anim());
    }
}
