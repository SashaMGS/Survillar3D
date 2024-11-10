using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchScript : MonoBehaviour
{
    [Header("Скрипты")]
    private saveItems saveItemsClass;
    private MenuEvent menuEvent;
    [Header("Переменные")]
    public short currentIdAch;

    public int currentPage;
    public TMP_Text curPageTxt;
    public TMP_Text pageMaxTxt;
    public TMP_Text curApplyAchTxt;
    public GameObject[] achPagePack;
    public GameObject[] achLockImgs;

    private void Start()
    {
        menuEvent = GetComponent<MenuEvent>();
        saveItemsClass = menuEvent.saveItemsClass;
        
        for (int i = 0; i < achPagePack.Length; i++)
            achPagePack[i].SetActive(false);
        achPagePack[0].SetActive(true);
        curPageTxt.text = (currentPage + 1).ToString();
        pageMaxTxt.text = achPagePack.Length.ToString();

        for (int i = 0; i < achLockImgs.Length; i++)
            achLockImgs[i].SetActive(true);

        int applyAchivements = 0;
        if (saveItemsClass.Achivement1)
        {
            achLockImgs[0].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement2)
        {
            achLockImgs[1].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement3)
        {
            achLockImgs[2].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement4)
        {
            achLockImgs[3].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement5)
        {
            achLockImgs[4].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement6)
        {
            achLockImgs[5].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement7)
        {
            achLockImgs[6].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement8)
        {
            achLockImgs[7].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement9)
        {
            achLockImgs[8].SetActive(false);
            applyAchivements++;
        }
        if (saveItemsClass.Achivement10)
        {
            achLockImgs[9].SetActive(false);
            applyAchivements++;
        }
        curApplyAchTxt.text = applyAchivements.ToString();
    }

    public void NextPage()
    {
        if (currentPage < achPagePack.Length - 1)
        {
            for (int i = 0; i < achPagePack.Length; i++)
                achPagePack[i].SetActive(false);
            currentPage++;
            achPagePack[currentPage].SetActive(true);
            curPageTxt.text = (currentPage + 1).ToString();
        }
    }

    public void BackPage()
    {
        if (currentPage > 0)
        {
            for (int i = 0; i < achPagePack.Length; i++)
                achPagePack[i].SetActive(false);
            currentPage--;
            achPagePack[currentPage].SetActive(true);
            curPageTxt.text = (currentPage + 1).ToString();
        }
    }
}
