using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript4 : MonoBehaviour
{
    [Header("����")]
    public float hp = 2000f;
    public float startHP = 2000f;
    public float rangeAttack = 6f;
    public float damageEnemy = 5f;
    public float damageTime = 0.2f;
    public short idMob;
    public Animator anim;
    public bool isOnTriggerPlayer = false;
    private bool isDie;
    public bool canTakeDamageLazer = false;

    [Header("��� �������� ���")]
    public bool isHandCombat = true;
    private EnemyMovement enemyMovement;
    public bool isRunAttack2;

    [Header("�������")]
    public ParticleSystem spawnParticle;

    [Header("���� ��������")]
    public PoolObjects poolGear;
    public PoolObjects poolExp;
    public GameObject PrefPart2Boss;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.movementSpeed = enemyMovement.defaultSpeed;

        poolGear = GameObject.FindGameObjectWithTag("GearPool").GetComponent<PoolObjects>();
        poolExp = GameObject.FindGameObjectWithTag("ExpPool").GetComponent<PoolObjects>();
    }

    void Update()
    {
        hpBoss();

        if (!isDie)
        {
            if (isHandCombat)
            {
                float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                if (distance <= rangeAttack && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
                {
                    if (!isOnTriggerPlayer)
                        StartCoroutine(damagePlayer(damageEnemy));
                    isOnTriggerPlayer = true;
                }
                else
                {
                    isOnTriggerPlayer = false;
                    StopCoroutine(damagePlayer(damageEnemy));
                }
            }
        }
    }

    public void hpBoss()
    {
        if (hp <= 0 && !isDie)
        {
            if (hp < 0)
                hp = 0;

            for (int i = 0; i < 20; i++)
            {
                poolGear.PlusClon(transform.GetChild(1).transform);
                poolExp.PlusClon(transform.GetChild(1).transform);
            }

            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
            saveAll.saveItemsClass.countKillBoss5++;
            switch (saveAll.saveItemsClass.currentGun)
            {
                case 0:
                    saveAll.saveItemsClass.countKillsPistol++;
                    break;
                case 1:
                    saveAll.saveItemsClass.countKillsShotGun++;
                    break;
                case 2:
                    saveAll.saveItemsClass.countKillsAutomat++;
                    break;
                case 3:
                    saveAll.saveItemsClass.countKillsMachinegun++;
                    break;
            }
            saveAll.saveItemsClass.KillBossRand5 = true;
            anim.SetBool("Die", true);
            isDie = true;
            enemyMovement.movementSpeed = 0f;
        }

        if (hp <= startHP / 2 && !isRunAttack2)
        {
            Instantiate(PrefPart2Boss, transform.position, Quaternion.identity);
            isRunAttack2 = true;
        }
    }


    //--------------------------���� ������---------------------------------
    IEnumerator damagePlayer(float damage)
    {
        isOnTriggerPlayer = true;
        while (hp > 0 && isOnTriggerPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().takeDamage(damage);
            yield return new WaitForSeconds(damageTime);
        }
        yield break;
    }
    //--------------------------���� ������---------------------------------

    //-------------------------���� �� ������-------------------------------
    public void giveDamage(int damage)
    {
        hp -= damage;
    }
    //-------------------------���� �� ������-------------------------------
}
