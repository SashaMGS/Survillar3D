using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOsn : MonoBehaviour
{
    public bool isAuto = false; // �������������� ������?
    public int damageWeaponCurrent = 25; // ����
    public float speedReloadCurrent = 3f; // �������� �����������
    public int ammoCurrent = 7; // ������� � ������
    public int maxAmmoCurrent = 7; // ������������ ���-�� �������� � ������
    public int ammoPackage = 49; // �������� � �������
    public GameObject gameObjectWeaponCurrent; // ������ ������


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�������");
            /*
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<EnemyScript>().hp -= damageWeaponCurrent;
                    Debug.Log("���� ���������");
                }
            }
            */
        }
    }
}
