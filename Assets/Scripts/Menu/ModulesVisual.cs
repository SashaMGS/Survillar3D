using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesVisual : MonoBehaviour
{
    public Transform start;  // ������ ������
    public Transform end;    // ������ ������

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f; // ��������� ������� ���������� ����� �����
        lineRenderer.endWidth = 0.02f;   // ��������� ������� ��������� ����� �����
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, start.position); // ��������� ��������� ����� �����
        lineRenderer.SetPosition(1, end.position);   // ��������� �������� ����� �����
    }
}
