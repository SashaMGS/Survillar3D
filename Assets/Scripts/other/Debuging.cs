using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Debuging : MonoBehaviour
{
    public bool showInfo;
    float deltaTime = 0.0f;
    int fps;
    public TMP_Text fpsTxt;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!showInfo)
                showInfo = true;
            else
                showInfo = false;
        }
        if (!showInfo)
            fpsTxt.gameObject.SetActive(false);
        else
            fpsTxt.gameObject.SetActive(true);
        if (fpsTxt.gameObject.activeSelf)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            fps = Mathf.RoundToInt(1.0f / deltaTime);
            fpsTxt.text = "тоя - " + fps.ToString();
        }
    }
}
