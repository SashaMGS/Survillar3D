using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using YG;

public class MenuEvent : MonoBehaviour
{
    public bool canUseCheat;
    public GameObject ObjWeapons;
    public short currentGun;
    public GameObject[] gunObjects;
    public int[] priceGun;
    public TMP_Text BuyButTxt;
    public TMP_Text gearTxt;
    public GameObject gearIcon;

    public GameObject skillMoreImg;

    public float expNeedToUpLvl; // Необходимое количество опыта для повышения уровня
    public Image experienceImg;
    public TMP_Text levelExpTxt;

    public TMP_Text skillsTxt;
    public TMP_Text playTimeTxt;
    public TMP_Text difficultyTxt;
    public TMP_Text deadCountTxt;
    public saveItems saveItemsClass;

    [Header("Панели")]
    public GameObject[] panelsMenu;
    public GameObject panStart;

    public bool CanGoToLevel;

    public GameObject[] killBossObj;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        YandexGame.FullscreenShow();
        if (!saveItemsClass.buyGun[currentGun] && currentGun > 0)
        {
            gearIcon.SetActive(true);
            BuyButTxt.text = "Купить за " + priceGun[currentGun].ToString();
            BuyButTxt.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            gearIcon.SetActive(false);
            BuyButTxt.text = "Выбранное оружие";
            BuyButTxt.alignment = TextAlignmentOptions.Center;
        }
        changePriceGuns();
        skillMoreImg.SetActive(false);
        panStart.SetActive(false);

        menuVoid();
        playTimeTxt.text = "Наиграно минут -> " + ((int)(saveItemsClass.allSecondPlay / 60)).ToString();
        difficultyTxt.text = "Прохождений -> " + saveItemsClass.difficulty.ToString();
        deadCountTxt.text = "Смертей -> " + saveItemsClass.countDeadAll.ToString();
        if (saveItemsClass.countDelAch == 0)
            saveItemsClass.countDelAch = 1;
        gunObjects[0].gameObject.SetActive(false);
        gunObjects[currentGun].gameObject.SetActive(true);

        for (int i = 0; i < killBossObj.Length; i++)
            if (killBossObj[i] != null)
                killBossObj[i].gameObject.SetActive(false);

        if (saveItemsClass.KillBoss1)
            killBossObj[0].gameObject.SetActive(true);
        if (saveItemsClass.KillBoss2)
            killBossObj[1].gameObject.SetActive(true);
        if (saveItemsClass.KillBoss3)
            killBossObj[2].gameObject.SetActive(true);
        if (saveItemsClass.KillBoss4)
            killBossObj[3].gameObject.SetActive(true);
        if (saveItemsClass.KillBoss5)
            killBossObj[4].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.F2) && canUseCheat)
        {
            saveItemsClass.GearCount = 1000;
            saveItemsClass.expSkills = 1000;
            Save();
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.F2) && canUseCheat)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Menu");
            Debug.Log("Все сохранения удалены");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && panelsMenu[0].activeSelf)
            RightBut();
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && panelsMenu[0].activeSelf)
            LeftBut();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            menuVoid();

        if (saveItemsClass.expSkills > 0)
            skillMoreImg.SetActive(true);
        else
            skillMoreImg.SetActive(false);
        gearTxt.text = saveItemsClass.GearCount.ToString();
        experienceImg.fillAmount = saveItemsClass.ExpCountCurrent / saveItemsClass.expNeedToUpLvl;
        levelExpTxt.text = saveItemsClass.levelExpS.ToString();
        skillsTxt.text = saveItemsClass.expSkills.ToString();

        if (CanGoToLevel)
            SceneManager.LoadScene("Level1");
    }

    public void changePriceGuns()
    {
        if (saveItemsClass.gunsBuy > 0)
            for (int i = 1; i < priceGun.Length; i++)
                priceGun[i] = 325 * (int)Mathf.Pow(4f, saveItemsClass.gunsBuy);
        else
            for (int i = 1; i < priceGun.Length; i++)
                priceGun[i] = 325;
    }

    public void menuVoid()
    {
        for (int i = 0; i < panelsMenu.Length; i++)
            panelsMenu[i].SetActive(false);
        panelsMenu[0].SetActive(true);
        panelsMenu[3].SetActive(true);
    }
    public void skillsVoid()
    {
        for (int i = 0; i < panelsMenu.Length; i++)
            panelsMenu[i].SetActive(false);
        panelsMenu[1].SetActive(true);
    }
    public void gideVoid()
    {
        for (int i = 0; i < panelsMenu.Length; i++)
            panelsMenu[i].SetActive(false);
        panelsMenu[2].SetActive(true);
    }

    public void AchivementsVoid()
    {
        for (int i = 0; i < panelsMenu.Length; i++)
            panelsMenu[i].SetActive(false);
        panelsMenu[4].SetActive(true);
    }

    public void SettingsVoid()
    {
        for (int i = 0; i < panelsMenu.Length; i++)
            panelsMenu[i].SetActive(false);
        panelsMenu[5].SetActive(true);
    }

    public void BuyBut()
    {
        if (!saveItemsClass.buyGun[currentGun] && saveItemsClass.GearCount >= priceGun[currentGun] && currentGun != 0)
        {
            saveItemsClass.buyGun[currentGun] = true;
            saveItemsClass.GearCount -= priceGun[currentGun];
            gearIcon.SetActive(false);
            BuyButTxt.text = "Выбранное оружие";
            BuyButTxt.alignment = TextAlignmentOptions.Center;

            saveItemsClass.gunsBuy++;
            changePriceGuns();

            Save();
        }
        else if (!saveItemsClass.buyGun[currentGun] && saveItemsClass.GearCount < priceGun[currentGun] && currentGun != 0)
            GetComponent<WarningScript>().warning(1, false);
    }
    public void RightBut()
    {
        if (currentGun == gunObjects.Length - 1)
        {
            gunObjects[currentGun].gameObject.SetActive(false);
            currentGun = 0;
        }
        else if (currentGun < gunObjects.Length - 1)
        {
            gunObjects[currentGun].gameObject.SetActive(false);
            currentGun++;
        }
        gunObjects[currentGun].gameObject.SetActive(true);
        if (!saveItemsClass.buyGun[currentGun] && currentGun > 0)
        {
            gearIcon.SetActive(true);
            BuyButTxt.text = "Купить за " + priceGun[currentGun].ToString();
            BuyButTxt.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            gearIcon.SetActive(false);
            BuyButTxt.text = "Выбранное оружие";
            BuyButTxt.alignment = TextAlignmentOptions.Center;
        }
        panelsMenu[3].transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    public void LeftBut()
    {
        if (currentGun == 0)
        {
            gunObjects[currentGun].gameObject.SetActive(false);
            currentGun = (short)(gunObjects.Length - 1);
        }
        else if (currentGun != 0)
        {
            gunObjects[currentGun].gameObject.SetActive(false);
            currentGun--;
        }
        gunObjects[currentGun].gameObject.SetActive(true);
        if (!saveItemsClass.buyGun[currentGun] && currentGun > 0)
        {
            gearIcon.SetActive(true);
            BuyButTxt.text = "Купить за " + priceGun[currentGun].ToString();
            BuyButTxt.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            gearIcon.SetActive(false);
            BuyButTxt.text = "Выбранное оружие";
            BuyButTxt.alignment = TextAlignmentOptions.Center;
        }
        panelsMenu[3].transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    public void ChangeModeGame()
    {
        if (saveItemsClass.buyGun[currentGun] == false && currentGun != 0)
            GetComponent<WarningScript>().warning(0, false);
        else
        {
            for (int i = 0; i < panelsMenu.Length; i++)
                panelsMenu[i].SetActive(false);
            panelsMenu[6].SetActive(true);
        }
    }

    public void StartDefGame()
    {
        saveItemsClass.bossRush = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(StartCoroutine());
    }

    public void StartRushModGame(bool start)
    {
        if (start)
        {
            if (saveItemsClass.GearCount >= 250)
            {
                saveItemsClass.bossRush = true;
                saveItemsClass.GearCount -= 250;
                GetComponent<WarningScript>().closeWarning();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                StartCoroutine(StartCoroutine());
            }
            else
                GetComponent<WarningScript>().warning(6, false);
        }
        else
            GetComponent<WarningScript>().warning(5, true);
    }

    IEnumerator StartCoroutine()
    {
        ObjWeapons.SetActive(false);
        for (int i = 0; i < panelsMenu.Length; i++)
            panelsMenu[i].SetActive(false);
        Save();
        yield return new WaitForSeconds(0.01f);
        GetComponent<Animator>().SetBool("Start", true);
        panStart.SetActive(true);
        yield break;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Save()
    {
        saveItemsClass.currentGun = currentGun;
        PlayerPrefs.SetString("ItemsClass", JsonUtility.ToJson(saveItemsClass));
        Debug.Log("Сохранено");
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("ItemsClass"))
        {
            saveItemsClass = JsonUtility.FromJson<saveItems>(PlayerPrefs.GetString("ItemsClass"));
            gearTxt.text = saveItemsClass.GearCount.ToString();
            skillsTxt.text = saveItemsClass.expSkills.ToString();
            currentGun = saveItemsClass.currentGun;
            Debug.Log("Загружено");
        }
        else
        {
            saveItemsClass.GearCount = 0;
            saveItemsClass.currentGun = 0;
            saveItemsClass.expSkills = 0;
            saveItemsClass.currentGun = 0;
            saveItemsClass.sensivity = 1f;
            saveItemsClass.volumeBack = 0.5f;
            saveItemsClass.volumeEffects = 0.3f;
            Save();
            Debug.Log("Загружены сохранения по умолчанию");
        }
    }
}