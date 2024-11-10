using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnObject : MonoBehaviour
{
    public Transform objPos;
    public bool isPlayer = true;
    public bool isBoss;

    private void Start()
    {
        objPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    void Update()
    {
        if (isBoss)
        {
            if(GetComponentInParent<BossScript>() != null)
                if(GetComponentInParent<BossScript>().hp > 0)
                    transform.LookAt(objPos); // где x, y - соответственные координаты нужного объекта
            if (GetComponentInParent<BossScript1>() != null)
                if (GetComponentInParent<BossScript1>().hp > 0)
                    transform.LookAt(objPos); // где x, y - соответственные координаты нужного объекта
            if (GetComponentInParent<BossScript2>() != null)
                if (GetComponentInParent<BossScript2>().hp > 0)
                    transform.LookAt(objPos); // где x, y - соответственные координаты нужного объекта
            if (GetComponentInParent<BossScript4>() != null)
                if (GetComponentInParent<BossScript4>().hp > 0)
                    transform.LookAt(objPos); // где x, y - соответственные координаты нужного объекта
        }
        else
            transform.LookAt(objPos); // где x, y - соответственные координаты нужного объекта
    }
}
