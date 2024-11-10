using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public bool destroyFall = true;
    public bool destroyDefault;
    public float destroyTime;
    public int speedfall = 10;
    private bool isDestroyed = false;
    void Start()
    {
        if (destroyFall)
            StartCoroutine(destroyFallCoroutine());
        else if (destroyDefault)
            Destroy(gameObject, destroyTime);
    }
    private void Update()
    {
        if (!isDestroyed)
            StartCoroutine(destroyFallCoroutine());
    }
    IEnumerator destroyFallCoroutine()
    {
        isDestroyed = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().drag = 0.05f;
        yield return new WaitForSeconds(destroyTime);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().drag = speedfall;
        yield return new WaitForSeconds(3f);
        isDestroyed = false;
        gameObject.SetActive(false);
    }
}
