using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    public float force = 10f; // Сила, с которой объект будет подкинут
    public float minAngle = 0f; // Минимальный угол для направления подкидывания (в градусах)
    public float maxAngle = 90f; // Максимальный угол для направления подкидывания (в градусах)

    private void Start()
    {
        // Получаем случайное направление подкидывания объекта
        float angle = Random.Range(minAngle, maxAngle);
        Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.up;

        // Подкидываем объект в случайном направлении
        GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
