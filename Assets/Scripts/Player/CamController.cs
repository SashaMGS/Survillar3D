using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CamController : MonoBehaviour
{
    public bool canMoveMouse = true;
    [Min(0)] public float sensivity;
    public Transform playerBody;
    public FixedTouchField fixedTouchField;
    public Vector2 LockAxis;

    private float xRotation = 0f;
    private float yRotation = 0.0f;
    public float xRecoil; // Отдача по x
    public float yRecoil; // Отдача по y

    private float mouseX;
    private float mouseY;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<PauseScript>().isPause && canMoveMouse)
        {
            sensivity = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().saveItemsClass.sensivity;

            mouseX = Input.GetAxis("Mouse X") * sensivity + xRecoil;
            mouseY = Input.GetAxis("Mouse Y") * sensivity + yRecoil;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

    }
}