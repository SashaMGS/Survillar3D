using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public bool canRotate = true;
    private float rotationSpeed = 5f;
    private bool isRotating;
    public float sensivityScroll = 0.1f;
    public float posX_Max = 1.5f;
    public float posX_Min = 0.5f;
    public Rect clickArea; // Область, в которой нужно определять нажатие

    private void Update()
    {
        if (Input.GetMouseButton(0) && clickArea.Contains(Input.mousePosition))
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating && canRotate)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Вращение объекта вокруг осей X и Y с учетом скорости и положения мыши
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed, Space.World);
        }

        /*
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0 && gameObject.transform.position.z < posX_Max)
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + sensivityScroll);
        if (mw < 0 && gameObject.transform.position.z > posX_Min)
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - sensivityScroll);
        */
    }
}
