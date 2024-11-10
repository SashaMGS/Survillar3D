using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript3 : MonoBehaviour
{
    [Header("Босс")]
    public float hp = 2000f;
    public float startHP = 2000f;
    public float rangeAttack = 6f;
    public float damageEnemy = 5f;
    public float damageTime = 0.2f;
    public short idMob;
    public Animator anim;
    public bool isOnTriggerPlayer = false;
    private bool isDie;

    public Transform[] placeSpawnObj;
    public short isPartPumpkin;
    public GameObject part2Obj;
    public GameObject part3Obj;

    [Header("Для ближнего боя")]
    public bool isHandCombat = true;
    private EnemyMovement enemyMovement;
    public bool isRunAttack2;

    [Header("Эффекты")]
    public ParticleSystem spawnParticle;

    [Header("Пулы обьектов")]
    public PoolObjects poolGear;
    public PoolObjects poolExp;


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
                    anim.SetBool("Attack", true);
                }
                else
                {
                    isOnTriggerPlayer = false;
                    anim.SetBool("Attack", false);
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
            for (int i = 0; i < 4; i++)
            {
                poolGear.PlusClon(transform.GetChild(1).transform);
                poolExp.PlusClon(transform.GetChild(1).transform);
            }
            anim.SetBool("Die", true);
            enemyMovement.movementSpeed = 0f;
            if (isPartPumpkin == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject boss = Instantiate(part2Obj, placeSpawnObj[i]); ;
                    BossScript3 BS = boss.GetComponent<BossScript3>();
                    UniversalSpawnEnemy useScript = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<UniversalSpawnEnemy>();
                    if (useScript.difficulty > 0)
                    {
                        BS.hp = BS.startHP * Mathf.Pow(1.15f, useScript.difficulty);
                        BS.startHP = BS.hp;
                        BS.damageEnemy *= Mathf.Pow(1.2f, useScript.difficulty);
                    }
                }
            }
            if (isPartPumpkin == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject boss = Instantiate(part3Obj, placeSpawnObj[i]); ;
                    BossScript3 BS = boss.GetComponent<BossScript3>();
                    UniversalSpawnEnemy useScript = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<UniversalSpawnEnemy>();
                    if (useScript.difficulty > 0)
                    {
                        BS.hp = BS.startHP * Mathf.Pow(1.15f, useScript.difficulty);
                        BS.startHP = BS.hp;
                        BS.damageEnemy *= Mathf.Pow(1.2f, useScript.difficulty);
                    }
                }
            }
            if (isPartPumpkin == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject.FindGameObjectWithTag("Enemy4Pool").GetComponent<PoolEnemy>().PlusClon(placeSpawnObj[i]);
                }
            }
            GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().countDeadPumpBoss++;

            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
            PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            if (playerScript.hp < playerScript.maxHp)
                playerScript.hp += playerScript.maxHp * saveAll.saveItemsClass.skillVampirizm * 0.007f * 3;

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

            isDie = true;
        }
    }
    //--------------------------Урон игроку---------------------------------
    IEnumerator damagePlayer(float damage)
    {
        isOnTriggerPlayer = true;
        while (hp > 0 && isOnTriggerPlayer)
        {
            yield return new WaitForSeconds(damageTime);
            if (isOnTriggerPlayer)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().takeDamage(damage);
        }
        yield break;
    }
    //--------------------------Урон игроку---------------------------------

    //-------------------------Урон от игрока-------------------------------
    public void giveDamage(int damage)
    {
        hp -= damage;
    }
    //-------------------------Урон от игрока-------------------------------
}
