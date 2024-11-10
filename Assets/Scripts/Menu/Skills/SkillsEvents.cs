using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsEvents : MonoBehaviour
{
    [Header("Скрипты")]
    private saveItems saveItemsClass;
    private MenuEvent menuEvent;
    [Header("Текст")]
    public TMP_Text descriptTxt_1;
    public TMP_Text descriptTxt_2;
    public TMP_Text descriptMaxLvlTxt;
    public TMP_Text descriptLevelSkillTxt;
    public TMP_Text priceDelSkTxt;
    [Header("Переменные")]
    public short currentIdSkill;

    public Image[] skillsImgs;

    private void Start()
    {
        menuEvent = GetComponent<MenuEvent>();
        saveItemsClass = menuEvent.saveItemsClass;
        for (int i = 0; i < skillsImgs.Length; i++)
            skillsImgs[i].color = Color.white;
        skillsImgs[0].color = Color.gray;
    }
    private void Update()
    {
        priceDelSkTxt.text = "(" + (100 * saveItemsClass.countDelAch * 1.6f).ToString() + ")";

        if (currentIdSkill == 0)
            descriptLevelSkillTxt.text = saveItemsClass.skillSpread.ToString();
        if (currentIdSkill == 1)
            descriptLevelSkillTxt.text = saveItemsClass.skillDamage.ToString();
        if (currentIdSkill == 2)
            descriptLevelSkillTxt.text = saveItemsClass.skillAmmoPlus.ToString();
        if (currentIdSkill == 3)
            descriptLevelSkillTxt.text = saveItemsClass.skillRecoil.ToString();
        if (currentIdSkill == 4)
            descriptLevelSkillTxt.text = saveItemsClass.skillHPplus.ToString();
        if (currentIdSkill == 5)
            descriptLevelSkillTxt.text = saveItemsClass.skillProtect.ToString();
        if (currentIdSkill == 6)
            descriptLevelSkillTxt.text = saveItemsClass.skillRegenHP.ToString();
        if (currentIdSkill == 7)
            descriptLevelSkillTxt.text = saveItemsClass.skillVampirizm.ToString();
        if (currentIdSkill == 8)
            descriptLevelSkillTxt.text = saveItemsClass.skillMaxStamina.ToString();
        if (currentIdSkill == 9)
            descriptLevelSkillTxt.text = saveItemsClass.skillSpeedUpStamina.ToString();
        if (currentIdSkill == 10)
            descriptLevelSkillTxt.text = saveItemsClass.skillSpeedWalk.ToString();
        if (currentIdSkill == 11)
            descriptLevelSkillTxt.text = saveItemsClass.skillSpeedRun.ToString();
    }

    public void skillTapVoid(SkillId skillId)
    {
        currentIdSkill = skillId.idSkill;
        descriptTxt_1.text = skillId.descriptTxt_1;
        descriptTxt_2.text = skillId.descriptTxt_2;
        descriptMaxLvlTxt.text = skillId.descriptMaxLvlTxt;
        for (int i = 0; i < skillsImgs.Length; i++)
            skillsImgs[i].color = Color.white;
        skillsImgs[skillId.idSkill].color = Color.gray;
    }

    public void buySkill()
    {
        if (saveItemsClass.expSkills > 0)
        {
            if (currentIdSkill == 0 && saveItemsClass.skillSpread < 5)
            {
                saveItemsClass.skillSpread++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 1)
            {
                saveItemsClass.skillDamage++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 2 && saveItemsClass.skillAmmoPlus < 5)
            {
                saveItemsClass.skillAmmoPlus++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 3 && saveItemsClass.skillRecoil < 5)
            {
                saveItemsClass.skillRecoil++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 4)
            {
                saveItemsClass.skillHPplus++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 5 && saveItemsClass.skillProtect < 5)
            {
                saveItemsClass.skillProtect++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 6 && saveItemsClass.skillRegenHP < 5)
            {
                saveItemsClass.skillRegenHP++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 7 && saveItemsClass.skillVampirizm < 5)
            {
                saveItemsClass.skillVampirizm++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 8 && saveItemsClass.skillMaxStamina < 10)
            {
                saveItemsClass.skillMaxStamina++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 9 && saveItemsClass.skillSpeedUpStamina < 5)
            {
                saveItemsClass.skillSpeedUpStamina++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 10 && saveItemsClass.skillSpeedWalk < 5)
            {
                saveItemsClass.skillSpeedWalk++;
                saveItemsClass.expSkills--;
            }
            if (currentIdSkill == 11 && saveItemsClass.skillSpeedRun < 5)
            {
                saveItemsClass.skillSpeedRun++;
                saveItemsClass.expSkills--;
            }
            menuEvent.Save();
        }
        else
            GetComponent<WarningScript>().warning(3, false);
    }

    public void delAllSkills(bool start)
    {
        if (start)
        {
            menuEvent = GetComponent<MenuEvent>();
            saveItemsClass = menuEvent.saveItemsClass;
            if (saveItemsClass.GearCount >= 160 * saveItemsClass.countDelAch
                && (saveItemsClass.skillSpread > 0
                || saveItemsClass.skillDamage > 0
                || saveItemsClass.skillAmmoPlus > 0
                || saveItemsClass.skillRecoil > 0
                || saveItemsClass.skillHPplus > 0
                || saveItemsClass.skillProtect > 0
                || saveItemsClass.skillRegenHP > 0
                || saveItemsClass.skillVampirizm > 0
                || saveItemsClass.skillMaxStamina > 0
                || saveItemsClass.skillSpeedUpStamina > 0
                || saveItemsClass.skillSpeedWalk > 0
                || saveItemsClass.skillSpeedRun > 0))
            {
                if (saveItemsClass.skillSpread > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillSpread;
                    saveItemsClass.skillSpread = 0;
                }
                if (saveItemsClass.skillDamage > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillDamage;
                    saveItemsClass.skillDamage = 0;
                }
                if (saveItemsClass.skillAmmoPlus > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillAmmoPlus;
                    saveItemsClass.skillAmmoPlus = 0;
                }
                if (saveItemsClass.skillRecoil > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillRecoil;
                    saveItemsClass.skillRecoil = 0;
                }
                if (saveItemsClass.skillHPplus > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillHPplus;
                    saveItemsClass.skillHPplus = 0;
                }
                if (saveItemsClass.skillProtect > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillProtect;
                    saveItemsClass.skillProtect = 0;
                }
                if (saveItemsClass.skillRegenHP > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillRegenHP;
                    saveItemsClass.skillRegenHP = 0;
                }
                if (saveItemsClass.skillVampirizm > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillVampirizm;
                    saveItemsClass.skillVampirizm = 0;
                }
                if (saveItemsClass.skillMaxStamina > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillMaxStamina;
                    saveItemsClass.skillMaxStamina = 0;
                }
                if (saveItemsClass.skillSpeedUpStamina > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillSpeedUpStamina;
                    saveItemsClass.skillSpeedUpStamina = 0;
                }
                if (saveItemsClass.skillSpeedWalk > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillSpeedWalk;
                    saveItemsClass.skillSpeedWalk = 0;
                }
                if (saveItemsClass.skillSpeedRun > 0)
                {
                    saveItemsClass.expSkills += saveItemsClass.skillSpeedRun;
                    saveItemsClass.skillSpeedRun = 0;
                }
                saveItemsClass.GearCount -= 160 * saveItemsClass.countDelAch;
                saveItemsClass.countDelAch++;
                menuEvent.Save();
                GetComponent<WarningScript>().closeWarning();
            }
            else if (saveItemsClass.skillSpread == 0
            && saveItemsClass.skillDamage == 0
            && saveItemsClass.skillAmmoPlus == 0
            && saveItemsClass.skillRecoil == 0
            && saveItemsClass.skillHPplus == 0
            && saveItemsClass.skillProtect == 0
            && saveItemsClass.skillRegenHP == 0
            && saveItemsClass.skillVampirizm == 0
            && saveItemsClass.skillMaxStamina == 0
            && saveItemsClass.skillSpeedUpStamina == 0
            && saveItemsClass.skillSpeedWalk == 0
            && saveItemsClass.skillSpeedRun == 0)
                GetComponent<WarningScript>().warning(4, false);
            else if (saveItemsClass.GearCount < 160 * saveItemsClass.countDelAch)
                GetComponent<WarningScript>().warning(2, false);
        }
        else
            GetComponent<WarningScript>().warning(7, true);
    }
}
