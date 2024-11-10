using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public Transform poolParent; // ��������, � ������� ��������� �����
    private int currentPoolElementId = 0; // ������� ���� �������, ������� �������� ����� ������ ������������ ���������

    public GameObject prefab; // ������ ��������, ������� ����� �����������
    public int poolCount; //  ���������� ������
    public bool isBullet;
    public bool isItem;

    private GameObject[] poolAR; // ������ ������ ��� ������ � ������� � �������


    private void Awake()
    {
        poolParent = transform; // ������������� �������� �������

        poolAR = new GameObject[poolCount]; // �������������� ������
        for (int i = 0; i < poolCount; i++) // ��������� ������ �������
        {
            poolAR[i] = Instantiate(prefab, transform.position, transform.rotation, poolParent);
            poolAR[i].SetActive(false);
        }
    }

    public void PlusClon(Transform objTransform)
    {
        GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // ������ ������ �� �������� �������
        if (isItem)
            objTransform.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        obj.transform.rotation = objTransform.transform.rotation; // ������������� ������� �����
        obj.SetActive(true); // �������� �����
        obj.transform.position = objTransform.position; // ������������� ������� �����
        currentPoolElementId++; // �������� ��������� ������ ��� �����������

        if (isBullet)
            obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 15f;
        if (objTransform.GetComponent<RotationOnObject>() != null)
            if (isBullet && objTransform.GetComponent<RotationOnObject>().isBoss)
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 45f;


        if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // ���� ������ ����� �� ����� ����, �������� � ������
    }
}
