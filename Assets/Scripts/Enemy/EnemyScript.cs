using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnDie;

    public float hp = 100f;
    public float startHP = 100f;
    public bool isOnTriggerPlayer = false;
    public bool isHandCombat = true;
    public float rangeAttack = 3f;
    public float damageEnemy = 10f;
    public float damageTime = 1f;
    public Animator anim;
    public short idMob;

    [Header("Эффекты")]
    public ParticleSystem spawnParticle;

    [Header("Пулы обьектов")]
    public PoolObjects poolDieEnemy;
    public PoolObjects poolGear;
    public PoolObjects poolExp;

    [Header("Для турели")]
    public GameObject canon;

    [Header("Для призывателя")]
    public PoolEnemy poolBaby;
    public Transform posSpawnBaby;
    public float spawnBabyTime = 3f;

    private void Awake()
    {
        if (idMob == 3) // Если это призыватель
        {
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy4PoolDead").GetComponent<PoolObjects>();
            poolBaby = GameObject.FindGameObjectWithTag("Enemy4PoolBaby").GetComponent<PoolEnemy>();
            posSpawnBaby = gameObject.transform.GetChild(1);
        }
    }

    void Start()
    {
        if (idMob == 0)
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy1PoolDead").GetComponent<PoolObjects>();
        if (idMob == 1)
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy2PoolDead").GetComponent<PoolObjects>();
        if (idMob == 2)
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy3PoolDead").GetComponent<PoolObjects>();
        if (idMob == 3)
        {
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy4PoolDead").GetComponent<PoolObjects>();
            poolBaby = GameObject.FindGameObjectWithTag("Enemy4PoolBaby").GetComponent<PoolEnemy>();
            posSpawnBaby = gameObject.transform.GetChild(1);
        }
        if (idMob == 4)
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy5PoolDead").GetComponent<PoolObjects>();
        if (idMob == 5)
            poolDieEnemy = GameObject.FindGameObjectWithTag("Enemy4PoolBabyDead").GetComponent<PoolObjects>();

        anim.SetBool("Attack", false);

        poolGear = GameObject.FindGameObjectWithTag("GearPool").GetComponent<PoolObjects>();
        poolExp = GameObject.FindGameObjectWithTag("ExpPool").GetComponent<PoolObjects>();
    }

    void Update()
    {
        if (hp <= 0)
        {
            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();

            switch (idMob)
            {
                case 0:
                    saveAll.saveItemsClass.killMob1++;
                    break;
                case 1:
                    saveAll.saveItemsClass.killMob2++;
                    break;
                case 2:
                    saveAll.saveItemsClass.killMob3++;
                    break;
                case 3:
                    saveAll.saveItemsClass.killMob4++;
                    break;
                case 4:
                    saveAll.saveItemsClass.killMob5++;
                    break;
            }

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

            if (idMob != 5)
            {
                poolGear.PlusClon(transform);
                poolExp.PlusClon(transform);
            }
            //----------------------Скилл вампиризм----------------------
            if (idMob != 5)
            {
                PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
                if (playerScript.hp < playerScript.maxHp)
                    playerScript.hp += playerScript.maxHp * saveAll.saveItemsClass.skillVampirizm * 0.007f;
            }
            else
            {
                PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
                if (playerScript.hp < playerScript.maxHp)
                    playerScript.hp += playerScript.maxHp * saveAll.saveItemsClass.skillVampirizm * 0.007f / 10;
            }
            //----------------------Скилл вампиризм----------------------
            poolDieEnemy.PlusClon(transform);
            hp = 1;
            OnDie.Invoke();
            gameObject.SetActive(false);
            saveAll.Save();
        }

        if (isHandCombat)
        {
            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if (distance <= rangeAttack && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
            {
                if (!isOnTriggerPlayer)
                    StartCoroutine(damagePlayer());
                isOnTriggerPlayer = true;
            }
            else
            {
                isOnTriggerPlayer = false;
                StopCoroutine(damagePlayer());
                anim.SetBool("Attack", false);
            }
        }



    }

    //--------------------------Урон игроку---------------------------------
    IEnumerator damagePlayer()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(damageTime);
        while (isOnTriggerPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().takeDamage(damageEnemy);
            yield return new WaitForSeconds(damageTime);
        }
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
        while (true)
        {
            poolBaby.PlusClon(transform);
            yield return new WaitForSeconds(spawnBabyTime);
        }

    }
    //------------------------Для призывателя-------------------------------
}
