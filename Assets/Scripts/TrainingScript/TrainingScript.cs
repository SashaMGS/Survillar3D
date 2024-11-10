using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingScript : MonoBehaviour
{
    public RotationObject rotationWeapon;
    public bool isSceneGame;
    public short idTraining;
    public GameObject PanTraining;
    public GameObject[] TrImgs;
    public TMP_Text TrTxt;
    public string[] TrString;

    private saveItems SI;

    private void Start()
    {
        PanTraining.SetActive(false);
        if (isSceneGame)
        {
            SI = GetComponent<SaveAll>().saveItemsClass;
        }
        else
        {
            SI = GetComponent<MenuEvent>().saveItemsClass;
            if (!SI.trMenu)
            {
                PanTraining.SetActive(true);
                idTraining = 0;
                showTraining();
            }
        }
    }

    void showTraining()
    {
        rotationWeapon.canRotate = false;

        for (int i = 0; i < TrImgs.Length; i++)
            TrImgs[i].SetActive(false);

        if (idTraining < TrImgs.Length)
        {
            TrImgs[idTraining].SetActive(true);
            TrTxt.text = TrString[idTraining];
        }
    }

    public void RightPan()
    {
        if(idTraining == TrImgs.Length - 1)
            GetComponent<WarningScript>().warning(8, true);
        if (idTraining < TrImgs.Length - 1)
            idTraining++;
        showTraining();
    }

    public void LeftPan()
    {
        if (idTraining > 0)
            idTraining--;
        showTraining();
    }
    public void SkipPan()
    {
        GetComponent<WarningScript>().warning(8, true);
    }

    public void StartNewTrVoid()
    {
        PanTraining.SetActive(true);
        idTraining = 0;
        showTraining();
    }

    public void endTraining()
    {
        if (!isSceneGame)
        {
            SI.trMenu = true;
            GetComponent<MenuEvent>().Save();
        }
        PanTraining.SetActive(false);
        rotationWeapon.canRotate = true;
        GetComponent<WarningScript>().closeWarning();
    }
}
