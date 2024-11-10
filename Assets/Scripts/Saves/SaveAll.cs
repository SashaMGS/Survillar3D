using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAll : MonoBehaviour
{
    public IndicatorsUI indicatorsUI;
    public GameObject[] gunObjects;
    public saveItems saveItemsClass;


    private void Awake()
    {
        Load();
    }
    private void Start()
    {
        if (saveItemsClass.KillBossRand1 && saveItemsClass.KillBossRand2 && saveItemsClass.KillBossRand3 && saveItemsClass.KillBossRand4 && saveItemsClass.KillBossRand5)
        {
            saveItemsClass.KillBossRand1 = false;
            saveItemsClass.KillBossRand2 = false;
            saveItemsClass.KillBossRand3 = false;
            saveItemsClass.KillBossRand4 = false;
            saveItemsClass.KillBossRand5 = false;
        }
        StartCoroutine(AutoSave());
        gunObjects[0].gameObject.SetActive(false);
        gunObjects[saveItemsClass.currentGun].gameObject.SetActive(true);
    }

    public void Save()
    {
        saveItemsClass.GearCount = indicatorsUI.gears;
        saveItemsClass.ExpCountCurrent = indicatorsUI.experience;
        saveItemsClass.levelExpS = indicatorsUI.levelExp;
        saveItemsClass.expNeedToUpLvl = indicatorsUI.expNeedToUpLvl;

        PlayerPrefs.SetString("ItemsClass", JsonUtility.ToJson(saveItemsClass));
        Debug.Log("Сохранено");
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("ItemsClass"))
        {
            saveItemsClass = JsonUtility.FromJson<saveItems>(PlayerPrefs.GetString("ItemsClass"));
            indicatorsUI.gears = saveItemsClass.GearCount;
            indicatorsUI.experience = saveItemsClass.ExpCountCurrent;
            indicatorsUI.levelExp = saveItemsClass.levelExpS;
            Debug.Log("Загружено");
        }
        else
        {
            Debug.Log("Загружены сохранения по умолчанию");
            indicatorsUI.gears = 0;
            indicatorsUI.experience = 0;
            indicatorsUI.levelExp = 0;
            saveItemsClass.currentGun = 0;
            saveItemsClass.sensivity = 1f;
            saveItemsClass.volumeBack = 0.5f;
            saveItemsClass.volumeEffects = 0.3f;
            Save();
        }

    }

    IEnumerator AutoSave()
    {
        while (true)
        {
            Save();
            Debug.Log("Авто сохранение");
            yield return new WaitForSeconds(5f);
        }
    }
}

[System.Serializable]
public class saveItems
{
    // -------------Основные сохранения-----------------
    [Header("Основные сохранения")]
    public int GearCount;
    public float ExpCountCurrent;
    public float expNeedToUpLvl;
    public float expSkills;
    public float levelExpS;
    public int difficulty;
    public int difficultyBossRush;
    public int ExpLvl;
    public bool[] buyGun = new bool[4];
    public short currentGun;
    public int countDelAch;
    public short gunsBuy;
    // -----------------Статы оружия--------------------
    [Header("Статы оружия")]
    public short maxAmmo;
    public short maxdamage;
    public short speedReload;
    public short speedShoot;
    // --------------------Скиллы------------------------
    [Header("Скиллы оружия")]
    public short skillRecoil;
    public short skillSpread;
    public short skillDamage;
    public short skillAmmoPlus;
    [Header("Скиллы на хп")]
    public short skillHPplus;
    public short skillProtect;
    public short skillRegenHP;
    public short skillVampirizm;
    [Header("Скиллы на скорость")]
    public short skillMaxStamina;
    public short skillSpeedUpStamina;
    public short skillSpeedWalk;
    public short skillSpeedRun;
    [Header("Настройки")]
    public float sensivity;
    public float volumeBack;
    public float volumeEffects;
    [Header("Общая статистика")]
    public int countDeadAll;
    public int countDeadNoWin;
    public int allSecondPlay;
    public int curSessionSecondPlay;
    public int wave1SecondPlay;
    public int wave2SecondPlay;
    public int wave3SecondPlay;
    public int wave4SecondPlay;
    public int wave5SecondPlay;
    public int wave6SecondPlay;
    public int wave7SecondPlay;
    public int wave8SecondPlay;
    public int wave9SecondPlay;
    public int wave10SecondPlay;
    public int killMob1;
    public int killMob2;
    public int killMob3;
    public int killMob4;
    public int killMob5;
    public int countKillBoss1;
    public int countKillBoss2;
    public int countKillBoss3;
    public int countKillBoss4;
    public int countKillBoss5;
    public int countKillsPistol;
    public int countKillsShotGun;
    public int countKillsAutomat;
    public int countKillsMachinegun;
    [Header("Достижения")]
    public bool Achivement1;
    public bool Achivement2;
    public bool Achivement3;
    public bool Achivement4;
    public bool Achivement5;
    public bool Achivement6;
    public bool CanShowAch6;
    public bool Achivement7;
    public bool Achivement8;
    public bool Achivement9;
    public bool Achivement10;
    [Header("Убитые боссы")]
    public bool KillBoss1;
    public bool KillBoss2;
    public bool KillBoss3;
    public bool KillBoss4;
    public bool KillBoss5;
    [Header("Убитые боссы для исключения рандома")]
    public bool KillBossRand1;
    public bool KillBossRand2;
    public bool KillBossRand3;
    public bool KillBossRand4;
    public bool KillBossRand5;
    [Header("Обучение")]
    public bool trGame;
    public bool trMenu;
    [Header("Режим игры")]
    public bool bossRush;
}

