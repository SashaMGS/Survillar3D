using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotsScript : MonoBehaviour
{
    public int superSize = 1; // Размер изображения (увеличение разрешения)
    public bool autoScreen;
    public KeyCode screenKeyCode = KeyCode.F3;

    private void Start()
    {
        StartCoroutine(delayScreenShot());
    }
    void Update()
    {
        if (Input.GetKeyDown(screenKeyCode))
        {
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HHmmss"); // Генерация уникального имени файла
            string filename = "screenshot_" + timestamp + ".png"; // Имя файла

            ScreenCapture.CaptureScreenshot(filename, superSize); // Создание скриншота с указанным именем и размером

            Debug.Log("Скриншот создан: " + filename);
        }
    }

    IEnumerator delayScreenShot()
    {
        while (autoScreen)
        {
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HHmmss"); // Генерация уникального имени файла
            string filename = "screenshot_" + timestamp + ".png"; // Имя файла

            ScreenCapture.CaptureScreenshot(filename, superSize); // Создание скриншота с указанным именем и размером

            yield return new WaitForSeconds(2f);
        }
    }
}
