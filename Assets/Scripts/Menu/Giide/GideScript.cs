using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GideScript : MonoBehaviour
{
    public GameObject EnemyButPack;
    public GameObject BossButPack;
    public GameObject WeaponsButPack;

    public Image EnemyBut;
    public Image BossBut;
    public Image WeaponsBut;

    public TMP_Text killEnemysTxt;
    public TMP_Text str1;
    public TMP_Text str1_1;
    public TMP_Text str2;
    public TMP_Text str2_1;
    public TMP_Text str3;
    public TMP_Text str3_1;
    public TMP_Text str4;
    public TMP_Text str4_1;

    public GameObject[] screenPack;
    public Image[] butsImg;
    void Start()
    {
        EnemyPackVoid();
    }

    public void EnemyPackVoid()
    {
        EnemyBut.color = Color.green;
        BossBut.color = Color.white;
        WeaponsBut.color = Color.white;
        EnemyButPack.SetActive(true);
        BossButPack.SetActive(false);
        WeaponsButPack.SetActive(false);
        str1.text = "���-�� ��������:";
        str2.text = "��������� ����:";
        str3.text = "�������� �����:";
        str4.text = "��� �����:";
        str1_1.text = "�������";
        str2_1.text = "�������";
        str3_1.text = "�������";
        str4_1.text = "�������";
        for (int i = 0; i < screenPack.Length; i++)
            screenPack[i].SetActive(false);
        screenPack[0].SetActive(true);
        for (int i = 0; i < screenPack.Length; i++)
            butsImg[i].color = Color.white;
        butsImg[0].color = Color.green;
        killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.killMob1.ToString();
    }
    public void BossPackVoid()
    {
        EnemyBut.color = Color.white;
        BossBut.color = Color.green;
        WeaponsBut.color = Color.white;
        EnemyButPack.SetActive(false);
        BossButPack.SetActive(true);
        WeaponsButPack.SetActive(false);
        str1.text = "���-�� ��������:";
        str2.text = "��������� ����:";
        str3.text = "�������� �����:";
        str4.text = "��� �����:";
        str1_1.text = "�������";
        str2_1.text = "�������";
        str3_1.text = "�������";
        str4_1.text = "�������";
        for (int i = 0; i < screenPack.Length; i++)
            screenPack[i].SetActive(false);
        screenPack[9].SetActive(true);
        for (int i = 0; i < screenPack.Length; i++)
            butsImg[i].color = Color.white;
        butsImg[9].color = Color.green;
        killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.countKillBoss1.ToString();
    }
    public void WeaponsPackVoid()
    {
        EnemyBut.color = Color.white;
        BossBut.color = Color.white;
        WeaponsBut.color = Color.green;
        EnemyButPack.SetActive(false);
        BossButPack.SetActive(false);
        WeaponsButPack.SetActive(true);
        str1.text = "���� ������:";
        str2.text = "��������� ����:";
        str3.text = "����������������:";
        str4.text = "��������:";
        str1_1.text = "������";
        str2_1.text = "�������";
        str3_1.text = "�������";
        str4_1.text = "�������";
        for (int i = 0; i < screenPack.Length; i++)
            screenPack[i].SetActive(false);
        screenPack[5].SetActive(true);
        for (int i = 0; i < screenPack.Length; i++)
            butsImg[i].color = Color.white;
        butsImg[5].color = Color.green;
        killEnemysTxt.text = "���������� � ������ -> " + GetComponent<MenuEvent>().saveItemsClass.countKillsPistol.ToString();
    }

    public void butGideTap(GideObjects GO)
    {
        str1_1.text = GO.str1;
        str2_1.text = GO.str2;
        str3_1.text = GO.str3;
        str4_1.text = GO.str4;
        for (int i = 0; i < screenPack.Length; i++)
            screenPack[i].SetActive(false);
        screenPack[GO.idScreen].SetActive(true);
        for (int i = 0; i < screenPack.Length; i++)
            butsImg[i].color = Color.white;
        butsImg[GO.idScreen].color = Color.green;


        switch(GO.idScreen) 
        {
            case 0:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.killMob1.ToString();
                break;
            case 1:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.killMob2.ToString();
                break;
            case 2:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.killMob3.ToString();
                break;
            case 3:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.killMob4.ToString();
                break;
            case 4:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.killMob5.ToString();
                break;
            case 5:
                killEnemysTxt.text = "���������� � ������ -> " + GetComponent<MenuEvent>().saveItemsClass.countKillsPistol.ToString();
                break;
            case 6:
                killEnemysTxt.text = "���������� � ������ -> " + GetComponent<MenuEvent>().saveItemsClass.countKillsShotGun.ToString();
                break;
            case 7:
                killEnemysTxt.text = "���������� � ������ -> " + GetComponent<MenuEvent>().saveItemsClass.countKillsAutomat.ToString();
                break;
            case 8:
                killEnemysTxt.text = "���������� � ������ -> " + GetComponent<MenuEvent>().saveItemsClass.countKillsMachinegun.ToString();
                break;
            case 9:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.countKillBoss1.ToString();
                break;
            case 10:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.countKillBoss2.ToString();
                break;
            case 11:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.countKillBoss3.ToString();
                break;
            case 12:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.countKillBoss4.ToString();
                break;
            case 13:
                killEnemysTxt.text = "���������� -> " + GetComponent<MenuEvent>().saveItemsClass.countKillBoss5.ToString();
                break;
        }
    }
}
