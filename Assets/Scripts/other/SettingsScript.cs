using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    public bool isSceneGame;
    [Min(0)] public int Quality = 0; // 5 - Высочайшая графика
    [Min(0)] public int vSync = 0;
    public Slider sensSlider;
    public TMP_Text sensTxt;
    public Slider audioSlider;
    public TMP_Text audioTxt;
    public AudioSource audioSourceAmbient;
    public Slider ambientSlider;
    public TMP_Text ambientTxt;
    public AudioSource[] audioSourcesSound;
    public void Awake()
    {
        QualitySettings.SetQualityLevel(Quality, true); // Установка минимального качества
    }

    private void Start()
    {
        if (isSceneGame)
        {
            for (int i = 0; i < audioSourcesSound.Length; i++)
                audioSourcesSound[i].volume = GetComponent<SaveAll>().saveItemsClass.volumeEffects;
            audioSourceAmbient.volume = GetComponent<SaveAll>().saveItemsClass.volumeBack;
            if (GetComponent<SaveAll>().saveItemsClass.sensivity <= 0)
                GetComponent<SaveAll>().saveItemsClass.sensivity = 1f;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().sensivity = GetComponent<SaveAll>().saveItemsClass.sensivity;


            sensSlider.value = GetComponent<SaveAll>().saveItemsClass.sensivity / 10;
            ambientSlider.value = GetComponent<SaveAll>().saveItemsClass.volumeBack;
            audioSlider.value = GetComponent<SaveAll>().saveItemsClass.volumeEffects;
        }
        else
        {
            audioSourceAmbient.volume = GetComponent<MenuEvent>().saveItemsClass.volumeBack;

            sensSlider.value = GetComponent<MenuEvent>().saveItemsClass.sensivity / 10;
            ambientSlider.value = GetComponent<MenuEvent>().saveItemsClass.volumeBack;
            audioSlider.value = GetComponent<MenuEvent>().saveItemsClass.volumeEffects;
        }
    }

    private void Update()
    {
        if (sensSlider.value >= 0.1)
            sensTxt.text = string.Format("{0:.00}", sensSlider.value * 10);
        else
            sensTxt.text = "0" + string.Format("{0:.00}", sensSlider.value * 10);
        audioTxt.text = ((int)(audioSlider.value * 100)).ToString() + "%";
        ambientTxt.text = ((int)(ambientSlider.value * 100)).ToString() + "%";
    }

    public void SetGraphicsQuality()
    {
        if (isSceneGame)
        {
            GetComponent<SaveAll>().saveItemsClass.sensivity = sensSlider.value * 10;
            GetComponent<SaveAll>().saveItemsClass.volumeBack = ambientSlider.value;
            GetComponent<SaveAll>().saveItemsClass.volumeEffects = audioSlider.value;
            GetComponent<SaveAll>().Save();
            for (int i = 0; i < audioSourcesSound.Length; i++)
                audioSourcesSound[i].volume = GetComponent<SaveAll>().saveItemsClass.volumeEffects;
            audioSourceAmbient.volume = GetComponent<SaveAll>().saveItemsClass.volumeBack;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().sensivity = GetComponent<SaveAll>().saveItemsClass.sensivity;
        }
        else
        {
            GetComponent<MenuEvent>().saveItemsClass.sensivity = sensSlider.value * 10;
            GetComponent<MenuEvent>().saveItemsClass.volumeBack = ambientSlider.value;
            GetComponent<MenuEvent>().saveItemsClass.volumeEffects = audioSlider.value;
            GetComponent<MenuEvent>().Save();
            audioSourceAmbient.volume = GetComponent<MenuEvent>().saveItemsClass.volumeBack;
        }
    }
}
