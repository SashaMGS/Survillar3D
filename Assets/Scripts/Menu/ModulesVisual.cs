using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesVisual : MonoBehaviour
{
    public Transform start;  // Первый объект
    public Transform end;    // Второй объект

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f; // Установка толщины начального конца линии
        lineRenderer.endWidth = 0.02f;   // Установка толщины конечного конца линии
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, start.position); // Установка начальной точки линии
        lineRenderer.SetPosition(1, end.position);   // Установка конечной точки линии
    }
}
