using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPos : MonoBehaviour
{
    public Vector3 target; // Целевая позиция для перемещения
    public float speed = 0.1f; // Скорость перемещения
    public bool isBoss4;

    void Start()
    {
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget()
    {
        while (transform.position != target)
        {
            transform.position = Vector3.Lerp(transform.position, target, 0.05f); // Перемещаем объект с помощью линейной интерполяции
            yield return new WaitForSeconds(speed);
        }
        if (transform.position == target && isBoss4)
        {
            GetComponent<BossScript4Part2>().anim.SetBool("CanStart", true);
            GetComponent<BossScript4Part2>().StartLazer();
        }
        yield return null;
    }
}
