using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconVisible : MonoBehaviour {

    private Image _image;
    private bool _isShown = true;
    public bool startVisible;

    private void Awake() {
        _image = GetComponent<Image>();
        _image.enabled = false;
        _isShown = false;
    }

    private void Start()
    {
        if (startVisible)
        {
            _isShown = false;
            Show();
        }
    }

    public void SetIconPosition(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Show() {
        if (_isShown) return;
        _isShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowProcess());
    }

    public void Hide() {
        if (!_isShown) return;
        _isShown = false;

        StopAllCoroutines();
        StartCoroutine(HideProcess());
    }

    IEnumerator ShowProcess() {
        _image.enabled = true;
        transform.localScale = Vector3.zero;
        for (float t = 0; t < 1f; t += Time.deltaTime * 4f) {
            transform.localScale = Vector3.one * t;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    IEnumerator HideProcess() {

        for (float t = 0; t < 1f; t += Time.deltaTime * 4f) {
            transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }
        _image.enabled = false;
    }

}
