using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
    public int poolCount; //  Количество клонов

    public Transform poolParent; // Родитель, в котором создаются клоны

    public GameObject prefab; // Префаб обьектов, который будем клонировать

    private int currentPoolElementId = 0; // Текущий айди обьекта, который понимает какой обьект активировать следующим

    private GameObject[] poolAR; // Массив клонов для работы с клонами в скрипте

    public UniversalSpawnEnemy USE_Script;


    private void Awake()
    {
        poolParent = transform; // Устанавливаем родителю позицию
        USE_Script = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<UniversalSpawnEnemy>();
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
        EnemyScript enemyScript = obj.GetComponent<EnemyScript>();

        //-----------------------Настраиваем характеристики врага в зависимости от текущей сложности---------------------------
        if (USE_Script.difficulty == 0)
            enemyScript.hp = enemyScript.startHP;
        else
            enemyScript.hp = enemyScript.startHP * Mathf.Pow(1.15f, USE_Script.difficulty);

        if (USE_Script.difficulty == 0)
            enemyScript.damageEnemy = enemyScript.GetComponent<EnemyScript>().damageEnemy;
        else
            enemyScript.damageEnemy *= Mathf.Pow(1.2f, USE_Script.difficulty);

        EnemyMovement enemyMovement = obj.GetComponent<EnemyMovement>();


        if (USE_Script.currentWave == 5)
            enemyMovement.movementSpeed *= 2;


        //-----------------------Настраиваем характеристики врага в зависимости от текущей сложности---------------------------

        obj.transform.position = spawnPlace.position; // Устанавливаем позицию клону
        
        obj.SetActive(true); // Включаем клона
        enemyScript.spawnParticle.Play();

        if (enemyScript.idMob == 2)
            enemyScript.canon.GetComponent<ShootingMob>().startShoot();
        if (enemyScript.idMob == 3)
        {
            enemyScript.StartSpawnBabyCor();
            obj.GetComponent<EnemyMovement>().StartCor_posSummuner();
        }


        obj.transform.rotation = spawnPlace.transform.rotation; // Устанавливаем поворот клону
        currentPoolElementId++; // Выбираем следующий обьект для манипуляций
        if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // Если скрипт дошёл до конца пула, начинаем с начала
    }
}

