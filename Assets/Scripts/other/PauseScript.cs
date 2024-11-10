using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using YG;

public class PauseScript : MonoBehaviour
{
    public bool endScene;
    public bool canApplyPause = true;
    public GameObject[] panels;
    public bool isPause;
    public GameObject panStart;
    public GameObject panRestart;
    public GameObject panEnd;
    [Header("Текст конца сцены")]
    public GameObject endTxt;
    public TMP_Text[] waveTimeApplyTxt;
    private bool applyWaveTime5 = false;
    public bool visibleUIPan = true;
    private bool curSS;
    private void Start()
    {
        YandexGame.FullscreenShow();
        endTxt.SetActive(false);
        disablePanelsVoid(0);
        panStart.SetActive(true);
        panRestart.SetActive(false);
        panEnd.SetActive(false);
        StartCoroutine(StartScene());
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P)) && canApplyPause)
            PauseVoid();
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (visibleUIPan)
            {
                visibleUIPan = false;
                panels[0].SetActive(false);
            }
            else
            {
                visibleUIPan = true;
                panels[0].SetActive(true);
            }
        }
    }
    public void MuteWind()
    {
        GameObject.FindGameObjectWithTag("Wind").GetComponent<AudioSource>().volume = 0f;
    }
    public void LoadTimeWhenCloseAdv()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("Wind").GetComponent<AudioSource>().volume = GetComponent<SaveAll>().saveItemsClass.volumeBack;
    }
    private void disablePanelsVoid(short ActivePanel)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[ActivePanel].SetActive(true);
    }

    public void PauseVoid()
    {
        if (!isPause)
        {
            isPause = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            disablePanelsVoid(1);
        }
        else
        {
            isPause = false;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            disablePanelsVoid(0);
        }
    }

    public void SettingVoid()
    {
        disablePanelsVoid(2);
    }

    public void ReturnVoid()
    {
        disablePanelsVoid(1);
    }

    public void InMenuVoid()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<SaveAll>().Save();
        SceneManager.LoadScene("Menu");
    }

    public void resumeVoid()
    {
        isPause = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        disablePanelsVoid(0);
    }

    public void Restart()
    {
        if (!GetComponent<SaveAll>().saveItemsClass.bossRush)
        {
            Time.timeScale = 1.0f;
            StartCoroutine(RestartSceneCor());
        }
        else if (GetComponent<SaveAll>().saveItemsClass.bossRush && GetComponent<SaveAll>().saveItemsClass.GearCount >= 250)
        {
            Time.timeScale = 1.0f;
            GetComponent<IndicatorsUI>().gears -= 250;
            StartCoroutine(RestartSceneCor());
        }
    }

    IEnumerator StartScene()
    {
        canApplyPause = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().canWalk = false; // должно быть false
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().canMoveMouse = false; // должно быть false
        for (int i = 0; i < GetComponent<IndicatorsUI>().Weapons.Length; i++)
            GetComponent<IndicatorsUI>().Weapons[i].canShoot = false;
        yield return new WaitForSeconds(3.5f);
        panStart.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().canWalk = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().canMoveMouse = true;
        for (int i = 0; i < GetComponent<IndicatorsUI>().Weapons.Length; i++)
            GetComponent<IndicatorsUI>().Weapons[i].canShoot = true;
        yield return new WaitForSeconds(0.5f);
        canApplyPause = true;
        yield return null;
    }

    IEnumerator RestartSceneCor()
    {
        GetComponent<SaveAll>().Save();
        panRestart.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
        yield break;
    }
    public void EndScene()
    {
        StartCoroutine(EndSceneCor());
    }
    IEnumerator EndSceneCor()
    {
        if (!endScene)
        {
            SaveAll saveAll = GetComponent<SaveAll>();
            if (!applyWaveTime5)
            {
                saveAll.saveItemsClass.wave5SecondPlay = saveAll.saveItemsClass.curSessionSecondPlay - 25 -
                                (saveAll.saveItemsClass.wave1SecondPlay + saveAll.saveItemsClass.wave2SecondPlay +
                                saveAll.saveItemsClass.wave3SecondPlay + saveAll.saveItemsClass.wave4SecondPlay);
                applyWaveTime5 = true;
            }
            GetComponent<IndicatorsUI>().AchivementVoid(0);
            showWinWave();
            yield return new WaitForSeconds(10f);
            panEnd.SetActive(true);
            yield return new WaitForSeconds(1f);
            if (GetComponent<UniversalSpawnEnemy>().bossSpawnNumber == 4)
            {
                GetComponent<SaveAll>().saveItemsClass.countKillBoss4++;
                GetComponent<SaveAll>().saveItemsClass.KillBossRand4 = true;
            }
            if (GetComponent<SaveAll>().saveItemsClass.bossRush)
                GetComponent<SaveAll>().saveItemsClass.difficultyBossRush++;
            else
                GetComponent<SaveAll>().saveItemsClass.difficulty++;
            GetComponent<SaveAll>().Save();
            SceneManager.LoadScene("Level1");
            endScene = true;
            yield break;
        }
    }

    public void showWinWave()
    {
        if (!curSS)
        {
            SaveAll saveAll = GetComponent<SaveAll>();
            for (int i = GetComponent<UniversalSpawnEnemy>().currentWave; i < 5; i++)
                waveTimeApplyTxt[i].enabled = false;
            if (saveAll.saveItemsClass.bossRush)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
                    waveTimeApplyTxt[0].text = "Потрачено времени на убийство босса -> \n" + saveAll.saveItemsClass.curSessionSecondPlay + " секунд";
                else
                    waveTimeApplyTxt[0].text = "Потрачено времени \n на попытку убийства босса -> " + saveAll.saveItemsClass.curSessionSecondPlay + " секунд";
                waveTimeApplyTxt[1].text = "";
                waveTimeApplyTxt[2].text = "";
                waveTimeApplyTxt[3].text = "";
                waveTimeApplyTxt[4].text = "";
            }
            else
            {
                waveTimeApplyTxt[0].text = "Первая волна -> " + saveAll.saveItemsClass.wave1SecondPlay + " секунд";
                waveTimeApplyTxt[1].text = "Вторая волна -> " + saveAll.saveItemsClass.wave2SecondPlay + " секунд";
                waveTimeApplyTxt[2].text = "Третья волна -> " + saveAll.saveItemsClass.wave3SecondPlay + " секунд";
                waveTimeApplyTxt[3].text = "Четвёртая волна -> " + saveAll.saveItemsClass.wave4SecondPlay + " секунд";
                waveTimeApplyTxt[4].text = "Пятая волна -> " + saveAll.saveItemsClass.wave5SecondPlay + " секунд";
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp <= 0 && GetComponent<UniversalSpawnEnemy>().currentWave > 0)
                    waveTimeApplyTxt[GetComponent<UniversalSpawnEnemy>().currentWave - 1].text = GetComponent<UniversalSpawnEnemy>().currentWave + " волна не пройдена.";
            }
            endTxt.SetActive(true);
            endTxt.GetComponent<IconVisible>().Show();
            curSS = true;
        }
        else
            return;
    }
}
