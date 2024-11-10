using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTimeScript : MonoBehaviour
{
    public TMP_Text waveTxt;
    public GameObject waveObj;
    private Animator WaveTextPackAnim;
    private short NextWaveTime = 5;
    public UniversalSpawnEnemy universalSpawn;
    public IndicatorsUI indicatorsUI;
    private void Start()
    {
        indicatorsUI = GetComponent<IndicatorsUI>();
        WaveTextPackAnim = waveObj.GetComponent<Animator>();
        StartCoroutine(WaveTimeCalculate());
    }
    void Update()
    {
        if (!universalSpawn.CanSpawn)
            waveTxt.text = NextWaveTime.ToString();
    }
    public void StartNextWave()
    {
        StartCoroutine(NextWave());
    }
    IEnumerator NextWave()
    {
        universalSpawn.CanSpawn = false;
        NextWaveTime = 5;
        while (NextWaveTime > 0)
        {
            waveObj.SetActive(true);
            WaveTextPackAnim.SetBool("Visible", true);
            yield return new WaitForSeconds(1f);
            NextWaveTime--;
        }
        yield return new WaitForSeconds(1f);
        WaveTextPackAnim.SetBool("Visible", false);
        universalSpawn.CanSpawn = true;
        universalSpawn.GenerateCountEnemy();
        yield return new WaitForSeconds(1f);
        waveObj.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        if (universalSpawn.currentWave == 10)
        {
            indicatorsUI.hpBossPack.SetActive(true);
            if (GetComponent<SaveAll>().saveItemsClass.wave9SecondPlay <= 35)
                GetComponent<IndicatorsUI>().AchivementVoid(2);
        }
        yield break;
    }

    IEnumerator WaveTimeCalculate()
    {
        SaveAll saveAll = GetComponent<SaveAll>();
        saveAll.saveItemsClass.curSessionSecondPlay = 0;
        saveAll.saveItemsClass.wave1SecondPlay = 0;
        saveAll.saveItemsClass.wave2SecondPlay = 0;
        saveAll.saveItemsClass.wave3SecondPlay = 0;
        saveAll.saveItemsClass.wave4SecondPlay = 0;

        bool applyWaveTime1 = false;
        bool applyWaveTime2 = false;
        bool applyWaveTime3 = false;
        bool applyWaveTime4 = false;
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
        {
            yield return new WaitForSeconds(1f);
            saveAll.saveItemsClass.allSecondPlay++;
            saveAll.saveItemsClass.curSessionSecondPlay++;
            if (universalSpawn.currentWave == 2 && !applyWaveTime1)
            {
                saveAll.saveItemsClass.wave1SecondPlay = saveAll.saveItemsClass.curSessionSecondPlay - 15;
                applyWaveTime1 = true;
            }

            if (universalSpawn.currentWave == 3 && !applyWaveTime2)
            {
                saveAll.saveItemsClass.wave2SecondPlay = saveAll.saveItemsClass.curSessionSecondPlay - 20
                    - saveAll.saveItemsClass.wave1SecondPlay;
                applyWaveTime2 = true;
            }

            if (universalSpawn.currentWave == 4 && !applyWaveTime3)
            {
                saveAll.saveItemsClass.wave3SecondPlay = saveAll.saveItemsClass.curSessionSecondPlay - 25
                    - (saveAll.saveItemsClass.wave1SecondPlay + saveAll.saveItemsClass.wave2SecondPlay);
                applyWaveTime3 = true;
            }

            if (universalSpawn.currentWave == 5 && !applyWaveTime4)
            {
                saveAll.saveItemsClass.wave4SecondPlay = saveAll.saveItemsClass.curSessionSecondPlay - 30
                                        - (saveAll.saveItemsClass.wave1SecondPlay + saveAll.saveItemsClass.wave2SecondPlay
                                        + saveAll.saveItemsClass.wave3SecondPlay);
                applyWaveTime4 = true;
            }
        }
        saveAll.Save();
        yield break;
    }
}
