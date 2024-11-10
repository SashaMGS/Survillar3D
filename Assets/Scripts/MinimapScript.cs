using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public bool canSetPos = true;
    public bool canRotate = true;
    [SerializeField] private Transform _player;

    private void Start()
    {
        if (_player == null) FindAnyObjectByType<PlayerScript>().GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (canSetPos)
        {
            Vector3 newPos = _player.position;
            newPos.y = transform.position.y;
            transform.position = newPos;


        }
        if (canRotate)
            transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0f);
        /*
        else
        {
            Vector3 newPos = new Vector3(0f,0f,0f);
            newPos.y = transform.position.y;
            transform.position = newPos;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        */

    }
}
