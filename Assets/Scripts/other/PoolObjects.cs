using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public Transform poolParent; // Родитель, в котором создаются клоны
    private int currentPoolElementId = 0; // Текущий айди обьекта, который понимает какой обьект активировать следующим

    public GameObject prefab; // Префаб обьектов, который будем клонировать
    public int poolCount; //  Количество клонов
    public bool isBullet;
    public bool isItem;

    private GameObject[] poolAR; // Массив клонов для работы с клонами в скрипте


    private void Awake()
    {
        poolParent = transform; // Устанавливаем родителю позицию

        poolAR = new GameObject[poolCount]; // Инициализируем массив
        for (int i = 0; i < poolCount; i++) // Заполняем массив клонами
        {
            poolAR[i] = Instantiate(prefab, transform.position, transform.rotation, poolParent);
            poolAR[i].SetActive(false);
        }
    }

    public void PlusClon(Transform objTransform)
    {
        GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // Создаём ссылку на текущего обьекта
        if (isItem)
            objTransform.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        obj.transform.rotation = objTransform.transform.rotation; // Устанавливаем поворот клону
        obj.SetActive(true); // Включаем клона
        obj.transform.position = objTransform.position; // Устанавливаем позицию клону
        currentPoolElementId++; // Выбираем следующий обьект для манипуляций

        if (isBullet)
            obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 15f;
        if (objTransform.GetComponent<RotationOnObject>() != null)
            if (isBullet && objTransform.GetComponent<RotationOnObject>().isBoss)
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 45f;


        if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // Если скрипт дошёл до конца пула, начинаем с начала
    }
}
