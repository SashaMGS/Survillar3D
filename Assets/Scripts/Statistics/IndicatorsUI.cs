using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Code.Source.Lessons.RaycastAttack;
using NewShootingSystem;

public class IndicatorsUI : MonoBehaviour
{
    public SaveAll saveAllScript;
    [Header("Монеты")]
    public int gears; // 
    public TMP_Text gearsTxt;
    [Header("Опыт")]
    public float experience; // Опыт игрока
    public float expNeedToUpLvl; // Необходимое количество опыта для повышения уровня
    public float levelExp; // Текущий уровень опыта
    public Image experienceImg;
    public TMP_Text levelExpTxt;
    [Header("Статы игрока")]
    public GameObject Player;
    public Image hpImageHeart;
    public Image staminaImage;
    [Header("Статы оружия")]
    public RaycastAttackLesson raycastAttack;
    public GunScript gunScript;
    public Image ammoCurrentImage;
    public Image ammoPackImage;
    public RaycastAttackLesson[] Weapons;
    [Header("Текущая волна")]
    public UniversalSpawnEnemy universalSpawnScript;
    public TMP_Text waveCurrentTxt;
    public GameObject warningTxt;
    [Header("Босс")]
    public GameObject hpBossPack;
    public Image hpBossImg;
    [Header("Достижения")]
    public Animator achivementAnim;
    public GameObject[] achImgs;
    public TMP_Text achText;

    private bool runWarningBoss;
    private bool getkeyDownShift;
    public int countDeadPumpBoss;

    #region StartVoid
    private void Start()
    {
        if (Player == null)
            GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < achImgs.Length; i++)
            achImgs[i].SetActive(false);

        warningTxt.SetActive(false);
        hpBossPack.SetActive(false);

        raycastAttack = Weapons[saveAllScript.saveItemsClass.currentGun];

        expNeedToUpLvl = 30;
        if (levelExp > 0)
            expNeedToUpLvl *= Mathf.Pow(1.06f, levelExp);
    }
    #endregion

    #region Update void
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AchivementVoid(6);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            getkeyDownShift = true;
        if (universalSpawnScript.currentWave == 5)
        {
            if (!getkeyDownShift)
                AchivementVoid(1);
            if (!runWarningBoss)
            {
                runWarningBoss = true;
                StartCoroutine(WarningSpawnBossCor());
            }
            hpBossPack.SetActive(true);
            int numberOfObjects = FindObjectsOfType<BossScript>().Length + FindObjectsOfType<BossScript1>().Length + FindObjectsOfType<BossScript2>().Length + FindObjectsOfType<BossScript3>().Length + FindObjectsOfType<BossScript4>().Length; // Проверяем количество обьектов на сцене
            if (numberOfObjects > 0)
            {
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>() != null)
                {
                    BossScript bossScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
                    hpBossImg.color = new Color32(255, 65, 0, 255);
                    hpBossImg.fillAmount = bossScript.hp / bossScript.startHP;
                }
                int numberOfBossUfo = FindObjectsOfType<BossScript1>().Length; // Проверяем количество обьектов на сцене
                if (numberOfBossUfo > 0)
                    if (GameObject.FindGameObjectWithTag("BossUFO").GetComponent<BossScript1>() != null)
                    {
                        BossScript1 bossScript = GameObject.FindGameObjectWithTag("BossUFO").GetComponent<BossScript1>();
                        hpBossImg.color = new Color32(255, 255, 0, 255);
                        hpBossImg.fillAmount = bossScript.hp / bossScript.startHP;
                    }
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript2>() != null)
                {
                    BossScript2 bossScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript2>();
                    hpBossImg.color = new Color32(155, 255, 255, 255);
                    hpBossImg.fillAmount = bossScript.hp / bossScript.startHP;
                }
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript3>() != null)
                {
                    int numberOfBoss = FindObjectsOfType<BossScript3>().Length; // Проверяем количество босс тыкв на сцене
                    BossScript3[] bossScript3 = new BossScript3[numberOfBoss];
                    float fillAmount = 0f;
                    float maxAmount = 0f;
                    for (int i = 0; i < bossScript3.Length; i++)
                    {
                        bossScript3[i] = FindObjectsOfType<BossScript3>()[i];
                        if (bossScript3[i].hp > 0)
                        {
                            fillAmount += bossScript3[i].hp;
                            maxAmount += bossScript3[i].startHP;
                        }
                    }
                    hpBossImg.color = new Color32(255, 200, 0, 255);
                    hpBossImg.fillAmount = fillAmount / maxAmount;



                    int numberOfPumpkin = FindObjectsOfType<EnemyScript>().Length; // Проверяем количество обьектов на сцене
                    EnemyScript[] enemyScript = new EnemyScript[numberOfBoss];
                    if (fillAmount <= 0)
                    {
                        if (numberOfPumpkin <= 0 && countDeadPumpBoss >= 6)
                        {
                            GetComponent<SaveAll>().saveItemsClass.countDeadNoWin = 0;
                            GetComponent<SaveAll>().saveItemsClass.KillBoss4 = true;
                            AchivementVoid(7);
                            GetComponent<PauseScript>().EndScene();
                        }
                    }
                }
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>() != null)
                {
                    BossScript4 bossScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>();
                    hpBossImg.color = new Color32(30, 255, 0, 255);
                    hpBossImg.fillAmount = bossScript.hp / bossScript.startHP;
                }
            }
        }

        gearsTxt.text = gears.ToString();

        staminaImage.fillAmount = Player.GetComponent<PlController>().staminaCurrent / Player.GetComponent<PlController>().staminaFull;

        hpImageHeart.fillAmount = Player.GetComponent<PlayerScript>().hp / Player.GetComponent<PlayerScript>().maxHp;

        //ammoCurrentImage.fillAmount = (float)raycastAttack.ammoCurrent / (float)raycastAttack.ammoFull;
        ammoCurrentImage.fillAmount = (float)gunScript._curAmmo / (float)gunScript._maxAmmo;
        //ammoPackImage.fillAmount = (float)raycastAttack.ammoPack / (float)raycastAttack.ammoPackFull;

        waveCurrentTxt.text = universalSpawnScript.currentWave.ToString();

        expVoid();
    }
    #endregion

    #region expVoid
    public void expVoid()
    {
        if (experience >= expNeedToUpLvl)
        {
            experience = 0;
            levelExp++;
            saveAllScript.saveItemsClass.expSkills++;
            if (levelExp > 0)
                expNeedToUpLvl *= Mathf.Pow(1.06f, levelExp);
        }

        levelExpTxt.text = levelExp.ToString();
        experienceImg.fillAmount = experience / expNeedToUpLvl;
    }
    #endregion

    IEnumerator WarningSpawnBossCor()
    {
        warningTxt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningTxt.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        warningTxt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningTxt.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        warningTxt.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        warningTxt.SetActive(false);
    }

    public void AchivementVoid(short id)
    {
        if (id == 0 && !saveAllScript.saveItemsClass.Achivement1)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Топ 1";
            saveAllScript.saveItemsClass.Achivement1 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 1 && !saveAllScript.saveItemsClass.Achivement2)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Улитка";
            saveAllScript.saveItemsClass.Achivement2 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 2 && !saveAllScript.saveItemsClass.Achivement3)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Скорость";
            saveAllScript.saveItemsClass.Achivement3 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 3 && !saveAllScript.saveItemsClass.Achivement4)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Олд";
            saveAllScript.saveItemsClass.Achivement4 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 4 && !saveAllScript.saveItemsClass.Achivement5)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Невезучий";
            saveAllScript.saveItemsClass.Achivement5 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 5 && !saveAllScript.saveItemsClass.Achivement6)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Случайность";
            saveAllScript.saveItemsClass.Achivement6 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 6 && !saveAllScript.saveItemsClass.Achivement7)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "Тяжеловес";
            saveAllScript.saveItemsClass.Achivement7 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
        if (id == 7 && !saveAllScript.saveItemsClass.Achivement8 && saveAllScript.saveItemsClass.difficulty == 10)
        {
            for (int i = 0; i < achImgs.Length; i++)
                achImgs[i].SetActive(false);
            achImgs[id].SetActive(true);
            achText.text = "День сурка";
            saveAllScript.saveItemsClass.Achivement8 = true;
            saveAllScript.Save();
            achivementAnim.Play("Start");
        }
    }
}
