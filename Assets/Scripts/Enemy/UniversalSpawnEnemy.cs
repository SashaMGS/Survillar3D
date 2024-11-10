using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalSpawnEnemy : MonoBehaviour
{
    public bool canUseCheat;
    public float difficulty = 0f;
    public short currentWave;
    public bool CanSpawn = false;
    public float secondSpawnfl = 3f; // Время спавна врагов
    public GameObject[] SpawnPlaces;
    public short spawnPlaceCurrent = 0;
    public WaveTimeScript waveTime;
    public short bossSpawnNumber;

    [Header("Пулы мобов")]
    public PoolEnemy poolEnemy;
    public PoolEnemy poolEnemy1;
    public PoolEnemy poolEnemy2;
    public PoolEnemy poolEnemy3;
    public PoolEnemy poolEnemy4;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;
    public GameObject Boss4;
    public GameObject Boss5;
    public GameObject[] bossKillObj;

    [Header("Частицы")]
    public ParticleSystem spawnBossParticle;
    /*
    [Header("Переменные для отбора спавна определённых мобов")]
    public short maxSpawnEnemy = 65;
    public short maxCountSpawnMob1 = 6;
    public short maxCountSpawnMob2 = 6;
    public short maxCountSpawnMob3 = 5;
    public short maxCountSpawnMob4 = 3;
    public short maxCountSpawnMob5 = 3;
    */
    [Header("Сколько должно появиться мобов в данный момент")]
    public short countSpawnMob1;
    public short countSpawnMob2;
    public short countSpawnMob3;
    public short countSpawnMob4;
    public short countSpawnMob5;
    public GameObject WavesObj;
    private void Start()
    {
        for (int i = 0; i < bossKillObj.Length; i++)
            if (bossKillObj[i] != null)
                bossKillObj[i].SetActive(false);
        SaveAll saveAll = GetComponent<SaveAll>();

        if (saveAll.saveItemsClass.bossRush)
            WavesObj.SetActive(false);
        else
            WavesObj.SetActive(true);

        if (bossKillObj[0] != null)
            if (saveAll.saveItemsClass.KillBoss1)
                bossKillObj[0].SetActive(true);
        if (bossKillObj[1] != null)
            if (saveAll.saveItemsClass.KillBoss2)
                bossKillObj[1].SetActive(true);
        if (bossKillObj[2] != null)
            if (saveAll.saveItemsClass.KillBoss3)
                bossKillObj[2].SetActive(true);
        if (bossKillObj[3] != null)
            if (saveAll.saveItemsClass.KillBoss4)
                bossKillObj[3].SetActive(true);
        if (bossKillObj[4] != null)
            if (saveAll.saveItemsClass.KillBoss5)
                bossKillObj[4].SetActive(true);

        difficulty = GetComponent<SaveAll>().saveItemsClass.difficulty;

        StartCoroutine(currentWaveCoroutine());

        if (saveAll.saveItemsClass.bossRush)
            currentWave = 4;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.F2) && currentWave != 5 && canUseCheat)
            currentWave = 4;
    }

    IEnumerator currentWaveCoroutine()
    {
        yield return new WaitForSeconds(3f);
        if (GetComponent<SaveAll>().saveItemsClass.CanShowAch6)
            GetComponent<IndicatorsUI>().AchivementVoid(5);
        if (GetComponent<SaveAll>().saveItemsClass.countDeadNoWin == 10)
            GetComponent<IndicatorsUI>().AchivementVoid(4);
        while (currentWave < 5)
        {
            int numberOfObjects = FindObjectsOfType<EnemyScript>().Length + FindObjectsOfType<BossScript>().Length; // Проверяем количество обьектов на сцене
            Debug.Log(numberOfObjects);
            if (numberOfObjects == 0 && CanSpawn)
            {
                if (countSpawnMob1 == 0 && countSpawnMob2 == 0 && countSpawnMob3 == 0 && countSpawnMob4 == 0 && countSpawnMob5 == 0)
                {
                    waveTime.StartNextWave();
                }
            }
            yield return new WaitForSeconds(2f);
        }
        while (currentWave == 5)
        {
            yield return new WaitForSeconds(5f);
            if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>() != null)
            {
                float hpBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>().hp; // Проверяем хп босса
                if (hpBoss <= 0)
                {
                    GetComponent<SaveAll>().saveItemsClass.countDeadNoWin = 0;
                    GetComponent<IndicatorsUI>().AchivementVoid(7);
                    GetComponent<PauseScript>().EndScene();
                    GetComponent<SaveAll>().saveItemsClass.KillBoss1 = true;
                }
            }
            int numberOfBossUfo = FindObjectsOfType<BossScript1>().Length; // Проверяем количество обьектов на сцене
            if (numberOfBossUfo > 0)
                if (GameObject.FindGameObjectWithTag("BossUFO").GetComponent<BossScript1>() != null)
                {
                    float hpBoss = GameObject.FindGameObjectWithTag("BossUFO").GetComponent<BossScript1>().hp; // Проверяем хп босса
                    if (hpBoss <= 0)
                    {
                        GetComponent<SaveAll>().saveItemsClass.countDeadNoWin = 0;
                        GetComponent<IndicatorsUI>().AchivementVoid(7);
                        GetComponent<PauseScript>().EndScene();
                        GetComponent<SaveAll>().saveItemsClass.KillBoss2 = true;
                    }
                }
            if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript2>() != null)
            {
                float hpBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript2>().hp; // Проверяем хп босса
                if (hpBoss <= 0)
                {
                    GetComponent<SaveAll>().saveItemsClass.countDeadNoWin = 0;
                    GetComponent<IndicatorsUI>().AchivementVoid(7);
                    GetComponent<PauseScript>().EndScene();
                    GetComponent<SaveAll>().saveItemsClass.KillBoss3 = true;
                }
            }
            if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>() != null)
            {
                float hpBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>().hp; // Проверяем хп босса
                if (hpBoss <= 0)
                {
                    GetComponent<SaveAll>().saveItemsClass.countDeadNoWin = 0;
                    GetComponent<IndicatorsUI>().AchivementVoid(7);
                    GetComponent<PauseScript>().EndScene();
                    GetComponent<SaveAll>().saveItemsClass.KillBoss5 = true;
                }
            }
        }
        yield break;
    }

    IEnumerator spawnEnemyCoroutine()
    {
        while (countSpawnMob1 > 0 || countSpawnMob2 > 0 || countSpawnMob3 > 0 || countSpawnMob4 > 0 || countSpawnMob5 > 0)
        {
            if (countSpawnMob1 > 0)
            {
                poolEnemy.PlusClon(SpawnPlaces[spawnPlaceCurrent].gameObject.transform);

                countSpawnMob1--;
                spawnPlaceCurrent++;
                if (spawnPlaceCurrent > SpawnPlaces.LongLength - 1)
                    spawnPlaceCurrent = 0;
                yield return new WaitForSeconds(secondSpawnfl);
            }
            if (countSpawnMob2 > 0)
            {
                poolEnemy1.PlusClon(SpawnPlaces[spawnPlaceCurrent].gameObject.transform);

                countSpawnMob2--;
                spawnPlaceCurrent++;
                if (spawnPlaceCurrent > SpawnPlaces.LongLength - 1)
                    spawnPlaceCurrent = 0;
                yield return new WaitForSeconds(secondSpawnfl);
            }
            if (countSpawnMob3 > 0)
            {
                poolEnemy2.PlusClon(SpawnPlaces[spawnPlaceCurrent].gameObject.transform);

                countSpawnMob3--;
                spawnPlaceCurrent++;
                if (spawnPlaceCurrent > SpawnPlaces.LongLength - 1)
                    spawnPlaceCurrent = 0;
                yield return new WaitForSeconds(secondSpawnfl);
            }
            if (countSpawnMob4 > 0)
            {
                poolEnemy3.PlusClon(SpawnPlaces[spawnPlaceCurrent].gameObject.transform);

                countSpawnMob4--;
                spawnPlaceCurrent++;
                if (spawnPlaceCurrent > SpawnPlaces.LongLength - 1)
                    spawnPlaceCurrent = 0;
                yield return new WaitForSeconds(secondSpawnfl);
            }
            if (countSpawnMob5 > 0)
            {
                poolEnemy4.PlusClon(SpawnPlaces[spawnPlaceCurrent].gameObject.transform);

                countSpawnMob5--;
                spawnPlaceCurrent++;
                if (spawnPlaceCurrent > SpawnPlaces.LongLength - 1)
                    spawnPlaceCurrent = 0;
                yield return new WaitForSeconds(secondSpawnfl);
            }
        }
    }

    public void GenerateCountEnemy()
    {
        /*
        while (countSpawnMob1 + countSpawnMob2 * 2 + countSpawnMob3 * 5 + countSpawnMob4 * 7 + countSpawnMob5 * 10 != maxSpawnEnemy)
        {
            countSpawnMob1 = (short)Random.Range(0, maxCountSpawnMob1);
            countSpawnMob2 = (short)Random.Range(0, maxCountSpawnMob2);
            countSpawnMob3 = (short)Random.Range(0, maxCountSpawnMob3);
            countSpawnMob4 = (short)Random.Range(0, maxCountSpawnMob4);
            countSpawnMob5 = (short)Random.Range(0, maxCountSpawnMob5);
        }
            countSpawnMob1 = ;
            countSpawnMob2 = ;
            countSpawnMob3 = ;
            countSpawnMob4 = ;
            countSpawnMob5 = ;
        */
        currentWave++;
        #region Первая сложность
        if (difficulty == 0)
        {
            if (currentWave == 1)
            {
                countSpawnMob1 = 3;
                countSpawnMob2 = 1;
            }
            if (currentWave == 2)
            {
                countSpawnMob1 = 4;
                countSpawnMob2 = 2;
                countSpawnMob3 = 1;
            }
            if (currentWave == 3)
            {
                countSpawnMob1 = 3;
                countSpawnMob2 = 3;
                countSpawnMob3 = 2;
                countSpawnMob4 = 1;
            }
            if (currentWave == 4)
            {
                countSpawnMob1 = 3;
                countSpawnMob2 = 3;
                countSpawnMob3 = 2;
                countSpawnMob4 = 2;
                countSpawnMob5 = 3;
            }
        }
        #endregion



        #region Вторая сложность и выше
        if (difficulty > 0)
        {
            if (currentWave == 1)
            {
                countSpawnMob1 = 3;
                countSpawnMob2 = 5;
                countSpawnMob3 = 3;
                countSpawnMob4 = 1;
                countSpawnMob5 = 2;
            }
            if (currentWave == 2)
            {
                countSpawnMob1 = 5;
                countSpawnMob2 = 6;
                countSpawnMob3 = 3;
                countSpawnMob4 = 2;
                countSpawnMob5 = 3;
            }
            if (currentWave == 3)
            {
                countSpawnMob1 = 7;
                countSpawnMob2 = 6;
                countSpawnMob3 = 3;
                countSpawnMob4 = 3;
                countSpawnMob5 = 4;
            }
            if (currentWave == 4)
            {
                countSpawnMob1 = 6;
                countSpawnMob2 = 7;
                countSpawnMob3 = 4;
                countSpawnMob4 = 4;
                countSpawnMob5 = 5;
            }
        }
        #endregion
        if (currentWave == 5)
        {
            saveItems SI = GetComponent<SaveAll>().saveItemsClass;
            if (!SI.KillBossRand1 && !SI.KillBossRand2 && !SI.KillBossRand3 && !SI.KillBossRand4 && !SI.KillBossRand5)
            {
                bossSpawnNumber = 1;
            }
            if (SI.KillBossRand1 && !SI.KillBossRand2 && !SI.KillBossRand3 && !SI.KillBossRand4 && !SI.KillBossRand5)
            {
                bossSpawnNumber = 2;
            }
            if (SI.KillBossRand1 && SI.KillBossRand2 && !SI.KillBossRand3 && !SI.KillBossRand4 && !SI.KillBossRand5)
            {
                bossSpawnNumber = 3;
            }
            if (SI.KillBossRand1 && SI.KillBossRand2 && SI.KillBossRand3 && !SI.KillBossRand4 && !SI.KillBossRand5)
            {
                bossSpawnNumber = 4;
            }
            if (SI.KillBossRand1 && SI.KillBossRand2 && SI.KillBossRand3 && SI.KillBossRand4 && !SI.KillBossRand5)
            {
                bossSpawnNumber = 5;
            }
            Debug.Log("Спавним босса с id = " + bossSpawnNumber);
            if (bossSpawnNumber == 1)
                StartCoroutine(SpawnBoss(1)); // 1
            if (bossSpawnNumber == 2)
                StartCoroutine(SpawnBoss(2)); // 2
            if (bossSpawnNumber == 3)
                StartCoroutine(SpawnBoss(3)); // 3
            if (bossSpawnNumber == 4)
                StartCoroutine(SpawnBoss(4)); // 4
            if (bossSpawnNumber == 5)
                StartCoroutine(SpawnBoss(5)); // 5
        }

        StartCoroutine(spawnEnemyCoroutine());
    }

    IEnumerator SpawnBoss(int idBossSpawn)
    {
        spawnBossParticle.Play();
        yield return new WaitForSeconds(2f);
        SaveAll sv = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
        saveItems si = sv.saveItemsClass;
        if (idBossSpawn == 1)
        {
            GameObject boss = Instantiate(Boss1, new Vector3(0, 0, 0), transform.rotation);
            BossScript BS = boss.GetComponent<BossScript>();
            if (si.bossRush)
            {
                if (si.difficultyBossRush > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, si.difficultyBossRush);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, si.difficultyBossRush);
                }
            }
            else
            {
                if (difficulty > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, difficulty);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, difficulty);
                }
            }
        }
        if (idBossSpawn == 2)
        {
            GameObject boss = Instantiate(Boss2, new Vector3(0, 0, 0), transform.rotation);
            BossScript1 BS = boss.GetComponent<BossScript1>();
            if (si.bossRush)
            {
                if (si.difficultyBossRush > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, si.difficultyBossRush);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, si.difficultyBossRush);
                }
            }
            else
            {
                if (difficulty > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, difficulty);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, difficulty);
                }
            }
        }
        if (idBossSpawn == 3)
        {
            GameObject boss = Instantiate(Boss3, new Vector3(0, 0, 0), transform.rotation);
            BossScript2 BS = boss.GetComponent<BossScript2>();
            if (si.bossRush)
            {
                if (si.difficultyBossRush > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, si.difficultyBossRush);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, si.difficultyBossRush);
                }
            }
            else
            {
                if (difficulty > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, difficulty);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, difficulty);
                }
            }
        }
        if (idBossSpawn == 4)
        {
            GameObject boss = Instantiate(Boss4, new Vector3(0, 0, 0), transform.rotation);
            BossScript3 BS = boss.GetComponent<BossScript3>();
            if (si.bossRush)
            {
                if (si.difficultyBossRush > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, si.difficultyBossRush);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, si.difficultyBossRush);
                }
            }
            else
            {
                if (difficulty > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, difficulty);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, difficulty);
                }
            }
        }
        if (idBossSpawn == 5)
        {
            GameObject boss = Instantiate(Boss5, new Vector3(0, 0, 0), transform.rotation);
            BossScript4 BS = boss.GetComponent<BossScript4>();
            if (si.bossRush)
            {
                if (si.difficultyBossRush > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, si.difficultyBossRush);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, si.difficultyBossRush);
                }
            }
            else
            {
                if (difficulty > 0)
                {
                    BS.hp = BS.startHP * Mathf.Pow(1.15f, difficulty);
                    BS.startHP = BS.hp;
                    BS.damageEnemy *= Mathf.Pow(1.2f, difficulty);
                }
            }
        }
        yield break;
    }
}