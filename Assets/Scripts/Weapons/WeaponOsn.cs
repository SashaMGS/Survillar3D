using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOsn : MonoBehaviour
{
    public bool isAuto = false; // Автоматическое оружие?
    public int damageWeaponCurrent = 25; // Урон
    public float speedReloadCurrent = 3f; // Скорость перезарядки
    public int ammoCurrent = 7; // патроны в обойме
    public int maxAmmoCurrent = 7; // максимальное кол-во патронов в обойме
    public int ammoPackage = 49; // патронов в кармане
    public GameObject gameObjectWeaponCurrent; // обьект оружия


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Выстрел");
            /*
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<EnemyScript>().hp -= damageWeaponCurrent;
                    Debug.Log("Есть попадание");
                }
            }
            */
        }
    }
}
