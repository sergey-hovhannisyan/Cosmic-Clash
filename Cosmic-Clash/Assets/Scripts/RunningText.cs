using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunningText : MonoBehaviour
{
    public TMP_Text textObject;
    public float delay = 0.1f;

    private string fullText;
    private string currentText = "";

    void Start()
    {
        fullText = textObject.text;
        textObject.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}