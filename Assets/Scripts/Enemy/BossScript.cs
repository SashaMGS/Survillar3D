using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [Header("Босс")]
    public float hp = 2000f;
    public float startHP = 2000f;
    public float rangeAttack = 6.5f;
    public float damageEnemy = 40f;
    public float damageTime = 1f;
    public short idMob;
    public Animator anim;
    public bool isOnTriggerPlayer = false;
    private bool isDie;

    [Header("Для ближнего боя")]
    public bool isHandCombat = true;
    private float distance;
    [SerializeField] private bool isCanTakeDamage;
    public ParticleSystem ParticleAttack1;
    public ParticleSystem ParticleAttack2;
    private EnemyMovement enemyMovement;
    public bool isRunAttack2;
    public int countAttack;

    [Header("Эффекты")]
    public ParticleSystem spawnParticle;

    [Header("Пулы обьектов")]
    public PoolObjects poolGear;
    public PoolObjects poolExp;

    [Header("Для турели")]
    public GameObject canon;

    [Header("Для призывателя")]
    public PoolEnemy poolBaby;
    public Transform posSpawnBaby1;
    public Transform posSpawnBaby2;
    public Transform posSpawnBaby3;
    public Transform posSpawnBaby4;
    public float spawnBabyTime = 3f;

    private void Awake()
    {
        if (idMob == 0) // Если это призыватель
        {
            poolBaby = GameObject.FindGameObjectWithTag("Enemy1Pool").GetComponent<PoolEnemy>();
            posSpawnBaby1 = gameObject.transform.GetChild(1);
            posSpawnBaby2 = gameObject.transform.GetChild(2);
            posSpawnBaby3 = gameObject.transform.GetChild(3);
            posSpawnBaby4 = gameObject.transform.GetChild(4);
        }
    }

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.movementSpeed = enemyMovement.defaultSpeed;
        if (isHandCombat)
            StartCoroutine(RandomAttack());

        anim.SetBool("Attack", false);
        poolGear = GameObject.FindGameObjectWithTag("GearPool").GetComponent<PoolObjects>();
        poolExp = GameObject.FindGameObjectWithTag("ExpPool").GetComponent<PoolObjects>();
    }

    void Update()
    {
        hpBoss();

        if (hp > 0)
        {
            distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if (distance <= rangeAttack)
                isOnTriggerPlayer = true;
            else
                isOnTriggerPlayer = false;
        }
    }

    IEnumerator RandomAttack()
    {
        while (!isDie)
        {
            if (countAttack <= 0)
            {
                int chanceAttack = Random.Range(0, 100);
                Debug.Log("Текущий шанс атаки - " + chanceAttack);
                if (chanceAttack >= 0 && chanceAttack <= 20)
                {
                    countAttack = 5;
                    StartCoroutine(Attack2());
                }
                if (chanceAttack >= 80 && chanceAttack <= 100)
                {
                    countAttack = 1;
                    StartCoroutine(Attack1());
                }
                if (chanceAttack >= 60 && chanceAttack < 80 && hp <= startHP / 2)
                    StartSpawnBabyCor();
                yield return new WaitForSeconds(1f);
            }
            else
                yield return new WaitForSeconds(5f);
        }
        yield break;
    }

    IEnumerator Attack1()
    {
        if (!isDie)
        {
            enemyMovement.movementSpeed = enemyMovement.speedIfAttack1;
            yield return new WaitForSeconds(1f);
            anim.SetBool("Attack1", true);
            yield return new WaitForSeconds(2.7f);
            if (isDie)
                yield break;
            ParticleAttack1.Play();
            yield return new WaitForSeconds(0.3f);

            if (distance <= rangeAttack + 3f)
            {
                if (!isCanTakeDamage)
                    StartCoroutine(damagePlayer(damageEnemy * 2));
            }

            yield return new WaitForSeconds(1f);
            anim.SetBool("Attack1", false);
            enemyMovement.movementSpeed = enemyMovement.defaultSpeed;
            countAttack--;
        }
        else
            yield break;
    }

    IEnumerator Attack2()
    {
        if (!isDie)
        {
            while (countAttack > 0 && isDie)
            {
                enemyMovement.movementSpeed = enemyMovement.speedIfAttack2;
                anim.SetBool("Attack2", true);
                yield return new WaitForSeconds(1.2f);
                if (isDie)
                    yield break;
                ParticleAttack2.Play();
                yield return new WaitForSeconds(0.1f);

                if (distance <= rangeAttack)
                {
                    StartCoroutine(damagePlayer(damageEnemy));
                }
                countAttack--;
            }
            enemyMovement.movementSpeed = enemyMovement.defaultSpeed;
            anim.SetBool("Attack2", false);
        }
        else
            yield break;

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
            saveAll.saveItemsClass.countKillBoss1++;
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
            saveAll.saveItemsClass.KillBossRand1 = true;
            anim.SetBool("Die", true);
            enemyMovement.movementSpeed = 0f;
            isDie = true;
        }
    }


    //--------------------------Урон игроку---------------------------------
    IEnumerator damagePlayer(float damage)
    {
        isCanTakeDamage = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().takeDamage(damage);
        yield return new WaitForSeconds(damageTime);
        isCanTakeDamage = false;
        yield break;
    }
    //--------------------------Урон игроку---------------------------------

    //-------------------------Урон от игрока-------------------------------
    public void giveDamage(int damage)
    {
        hp -= damage;
    }
    //-------------------------Урон от игрока-------------------------------

    //------------------------Для призывателя-------------------------------
    public void StartSpawnBabyCor()
    {
        StartCoroutine(SpawnBaby());
    }
    public IEnumerator SpawnBaby()
    {
        poolBaby.PlusClon(posSpawnBaby1);
        poolBaby.PlusClon(posSpawnBaby2);
        poolBaby.PlusClon(posSpawnBaby3);
        poolBaby.PlusClon(posSpawnBaby4);
        yield return new WaitForSeconds(spawnBabyTime);
    }
    //------------------------Для призывателя-------------------------------
}
