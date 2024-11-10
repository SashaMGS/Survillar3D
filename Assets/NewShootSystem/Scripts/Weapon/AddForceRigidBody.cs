using System.Collections;
using UnityEngine;

namespace NewShootingSystem
{
    public class AddForceRigidBody : MonoBehaviour
    {
        public float _timeDestroy = 3f;


        IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(_timeDestroy);
            gameObject.SetActive(false);
            yield break;
        }

        public void Fly()
        {
            GetComponent<Animator>().Play("Down");
            StartCoroutine(DestroyObject());
        }
    }
}

