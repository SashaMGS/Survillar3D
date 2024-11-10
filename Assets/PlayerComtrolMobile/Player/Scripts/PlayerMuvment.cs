using UnityEngine;
using System.Collections.Generic;

public class PlayerMuvment : MonoBehaviour
{
    
    public float speed = 5;
    Vector2 velocity;
    public Transform playerCamera;
    public FloatingJoystick floatingJoystick;
    private float Hor, Ver;
    public float jumpStrength = 2f;
    public bool isGrounded;
    Rigidbody rigidb;

    
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private void Start() {
        rigidb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision other) 
    {
        isGrounded = true;
    }

    public void JumpButton()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rigidb.AddForce(Vector3.up * 100 * jumpStrength);
        }
    }

    void FixedUpdate()
    {
       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,playerCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
      
       Hor = floatingJoystick.Horizontal;
       Ver = floatingJoystick.Vertical;
    
        float movingSpeed = speed; 
        if (speedOverrides.Count > 0)
            movingSpeed = speedOverrides[speedOverrides.Count - 1]();
        velocity.y = Ver * movingSpeed * Time.deltaTime;
        velocity.x = Hor * movingSpeed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);
    }
}
