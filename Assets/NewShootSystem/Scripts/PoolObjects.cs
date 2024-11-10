using UnityEngine;

namespace NewShootingSystem
{
    public class PoolObjects : MonoBehaviour
    {
        public int poolCount; //  Количество клонов

        public Transform poolParent; // Родитель, в котором создаются клоны

        public GameObject prefab; // Префаб обьектов, который будем клонировать

        private int currentPoolElementId = 0; // Текущий айди обьекта, который понимает какой обьект активировать следующим

        private GameObject[] poolAR; // Массив клонов для работы с клонами в скрипте



        private void Awake()
        {
            poolParent = transform; // Устанавливаем родителю позицию
            poolAR = new GameObject[poolCount]; // Инициализируем массив
            for (int i = 0; i < poolCount; i++) // Заполняем массив клонами
            {
                poolAR[i] = Instantiate(prefab, transform.position, transform.rotation, poolParent);
                poolAR[i].SetActive(false);
            }
        }

        public void PlusClon(Transform spawnPlace)
        {
            GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // Создаём ссылку на текущего обьекта


            //-----------------------Настраиваем характеристики врага в зависимости от текущей сложности---------------------------

            obj.transform.position = spawnPlace.position; // Устанавливаем позицию клону

            obj.SetActive(true); // Включаем клона
            if (GetComponent<ParticleSystem>())
                GetComponent<ParticleSystem>().Play();
            if (obj.transform.GetChild(0).GetComponent<AddForceRigidBody>())
                obj.transform.GetChild(0).GetComponent<AddForceRigidBody>().Fly();

            obj.transform.rotation = spawnPlace.transform.rotation; // Устанавливаем поворот клону
            currentPoolElementId++; // Выбираем следующий обьект для манипуляций
            if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // Если скрипт дошёл до конца пула, начинаем с начала
        }

        public void PlusClon(Vector3 spawnPlace, Quaternion spawnRotate)
        {
            GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // Создаём ссылку на текущего обьекта


            //-----------------------Настраиваем характеристики врага в зависимости от текущей сложности---------------------------

            obj.transform.position = spawnPlace; // Устанавливаем позицию клону

            obj.SetActive(true); // Включаем клона
            if (obj.GetComponent<ParticleSystem>())
                obj.GetComponent<ParticleSystem>().Play();
            if (obj.GetComponent<AddForceRigidBody>())
                obj.GetComponent<AddForceRigidBody>().Fly();

            obj.transform.rotation = spawnRotate; // Устанавливаем поворот клону
            currentPoolElementId++; // Выбираем следующий обьект для манипуляций
            if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // Если скрипт дошёл до конца пула, начинаем с начала
        }

        [System.Obsolete]
        public void PlusClon(Vector3 spawnPlace, Quaternion spawnRotate, Color color)
        {
            GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // Создаём ссылку на текущего обьекта


            //-----------------------Настраиваем характеристики врага в зависимости от текущей сложности---------------------------

            obj.transform.position = spawnPlace; // Устанавливаем позицию клону

            obj.SetActive(true); // Включаем клона
            if (obj.GetComponent<ParticleSystem>())
            {
                obj.GetComponent<ParticleSystem>().Play();
                obj.GetComponent<ParticleSystem>().startColor = color;
            }
            if (obj.GetComponent<AddForceRigidBody>())
                obj.GetComponent<AddForceRigidBody>().Fly();

            obj.transform.rotation = spawnRotate; // Устанавливаем поворот клону
            currentPoolElementId++; // Выбираем следующий обьект для манипуляций
            if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // Если скрипт дошёл до конца пула, начинаем с начала
        }
    }
}
