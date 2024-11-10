using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    public float force = 10f; // ����, � ������� ������ ����� ��������
    public float minAngle = 0f; // ����������� ���� ��� ����������� ������������ (� ��������)
    public float maxAngle = 90f; // ������������ ���� ��� ����������� ������������ (� ��������)

    private void Start()
    {
        // �������� ��������� ����������� ������������ �������
        float angle = Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.up;

        // ����������� ������ � ��������� �����������
        GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
