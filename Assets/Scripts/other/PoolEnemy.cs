using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
    public int poolCount; //  ���������� ������

    public Transform poolParent; // ��������, � ������� ��������� �����

    public GameObject prefab; // ������ ��������, ������� ����� �����������

    private int currentPoolElementId = 0; // ������� ���� �������, ������� �������� ����� ������ ������������ ���������

    private GameObject[] poolAR; // ������ ������ ��� ������ � ������� � �������

    public UniversalSpawnEnemy USE_Script;


    private void Awake()
    {
        poolParent = transform; // ������������� �������� �������
        USE_Script = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<UniversalSpawnEnemy>();
        poolAR = new GameObject[poolCount]; // �������������� ������
        for (int i = 0; i < poolCount; i++) // ��������� ������ �������
        {
            poolAR[i] = Instantiate(prefab, transform.position, transform.rotation, poolParent);
            poolAR[i].SetActive(false);
        }
    }

    public void PlusClon(Transform spawnPlace)
    {
        GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // ������ ������ �� �������� �������
        EnemyScript enemyScript = obj.GetComponent<EnemyScript>();

        //-----------------------����������� �������������� ����� � ����������� �� ������� ���������---------------------------
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


        //-----------------------����������� �������������� ����� � ����������� �� ������� ���������---------------------------

        obj.transform.position = spawnPlace.position; // ������������� ������� �����
        
        obj.SetActive(true); // �������� �����
        enemyScript.spawnParticle.Play();

        if (enemyScript.idMob == 2)
            enemyScript.canon.GetComponent<ShootingMob>().startShoot();
        if (enemyScript.idMob == 3)
        {
            enemyScript.StartSpawnBabyCor();
            obj.GetComponent<EnemyMovement>().StartCor_posSummuner();
        }


        obj.transform.rotation = spawnPlace.transform.rotation; // ������������� ������� �����
        currentPoolElementId++; // �������� ��������� ������ ��� �����������
        if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // ���� ������ ����� �� ����� ����, �������� � ������
    }
}

