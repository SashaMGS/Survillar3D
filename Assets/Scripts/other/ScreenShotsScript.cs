using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotsScript : MonoBehaviour
{
    public int superSize = 1; // ������ ����������� (���������� ����������)
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
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HHmmss"); // ��������� ����������� ����� �����
            string filename = "screenshot_" + timestamp + ".png"; // ��� �����

            ScreenCapture.CaptureScreenshot(filename, superSize); // �������� ��������� � ��������� ������ � ��������

            Debug.Log("�������� ������: " + filename);
        }
    }

    IEnumerator delayScreenShot()
    {
        while (autoScreen)
        {
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HHmmss"); // ��������� ����������� ����� �����
            string filename = "screenshot_" + timestamp + ".png"; // ��� �����

            ScreenCapture.CaptureScreenshot(filename, superSize); // �������� ��������� � ��������� ������ � ��������

            yield return new WaitForSeconds(2f);
        }
    }
}
