using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningScript : MonoBehaviour
{
    public RotationObject rotationWeapon;
    public GameObject warningPanel;
    public TMP_Text warningText;
    public GameObject[] buts;
    private short idYes;

    private void Start()
    {
        warningPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P))
            closeWarning();
    }

    public void warning(short idWarning, bool isInteractiveWarning)
    {
        rotationWeapon.canRotate = false;
        if (isInteractiveWarning)
        {
            for (int i = 0; i < buts.Length; i++)
                buts[i].gameObject.SetActive(false);
            buts[1].gameObject.SetActive(true);
            buts[2].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < buts.Length; i++)
                buts[i].gameObject.SetActive(false);
            buts[0].gameObject.SetActive(true);
        }

        #region ����� ��������������
        if (idWarning == 0)
            warningText.text = "�� �� ������ ������ ����, ��� ��� �� �� ������ ������ ������.";
        if (idWarning == 1)
        {
            MenuEvent me = GetComponent<MenuEvent>();
            warningText.text = "��� �� ������� ��������� ��� ������� ������ -> " + (me.priceGun[me.currentGun] - me.saveItemsClass.GearCount).ToString() + ".";
        }
        if (idWarning == 2)
        {
            MenuEvent me = GetComponent<MenuEvent>();
            warningText.text = "��� �� ������� ��������� ��� ������ ������� -> " + (160 * me.saveItemsClass.countDelAch - me.saveItemsClass.GearCount).ToString() + ".";
        }
        if (idWarning == 3)
            warningText.text = "��� �� ������� ������ ���� ������ ��� ��������.";
        if (idWarning == 4)
            warningText.text = "��� ������� ��� ������";
        if (idWarning == 5)
        {
            warningText.text = "����� � ���� ����� ����� 250 ���������. �� ������� ��� ������ ���� �����������?";
            idYes = 0;
        }
        if (idWarning == 6)
        {
            MenuEvent me = GetComponent<MenuEvent>();
            warningText.text = "��� �� ������� ��������� ��� ������� ������ -> " + (250 - me.saveItemsClass.GearCount).ToString() + ".";
        }
        if (idWarning == 7)
        {
            MenuEvent me = GetComponent<MenuEvent>();
            warningText.text = "����� ������� ����� -> " + (160 * me.saveItemsClass.countDelAch).ToString() + " ���������. �� ������� ��� ������ �������� ������?";
            idYes = 1;
        }
        if (idWarning == 8)
        {
            warningText.text = "��������� ��������?";
            idYes = 2;
        }
        #endregion

        warningPanel.SetActive(true);
    }
    public void closeWarning()
    {
        warningPanel.gameObject.SetActive(false);
        rotationWeapon.canRotate = true;
    }

    public void YesVoid()
    {
        if (idYes == 0)
            GetComponent<MenuEvent>().StartRushModGame(true);
        if (idYes == 1)
            GetComponent<SkillsEvents>().delAllSkills(true);
        if (idYes == 2)
            GetComponent<TrainingScript>().endTraining();
    }
}
