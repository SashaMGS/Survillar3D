using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyPointer : MonoBehaviour {

    [SerializeField] EnemyScript _enemyScript;

    private void Start() {
        PointerManager.Instance.AddToList(this);
        _enemyScript.OnDie.AddListener(Destroy);
    }

    private void create() {
        PointerManager.Instance.enabled = true;
    }


    private void Destroy() {
        PointerManager.Instance.enabled = false;
    }

}
