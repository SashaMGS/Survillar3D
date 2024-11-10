using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Source.Lessons.RaycastAttack;
using NewShootingSystem;

public class WaeponChange : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject ShotGun;
    public GameObject Automat;
    public GameObject Machinegun;
    public short _currentWeapon;
    public GameObject[] _weapons;
    void Start()
    {
        ChangeWeapons();
        Pistol.SetActive(true);
        ShotGun.SetActive(false);
        Automat.SetActive(false);
        Machinegun.SetActive(false);
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<PauseScript>().isPause)
        {
            //Input.GetAxis("Mouse ScrollWheel")
            if (Input.mouseScrollDelta.y > 0 && _currentWeapon < _weapons.Length - 1)
            {
                _currentWeapon++;
                ChangeWeapons();
            }
            else if (Input.mouseScrollDelta.y < 0 && _currentWeapon > 0)
            {
                _currentWeapon--;
                ChangeWeapons();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangePistol();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeShotGun();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeAutomat();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeMachinegun();
        }
    }
    public void ChangeWeapons()
    {
        foreach (var weapon in _weapons)
        {
            weapon.SetActive(false);
        }
        _weapons[_currentWeapon].SetActive(true);
        GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().gunScript = _weapons[_currentWeapon].transform.GetChild(0).GetComponent<GunScript>();
    }
    public void ChangePistol()
    {
        Pistol.SetActive(true);
        ShotGun.SetActive(false);
        Automat.SetActive(false);
        Machinegun.SetActive(false);
        GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().raycastAttack = Pistol.GetComponent<RaycastAttackLesson>();
    }
    public void ChangeShotGun()
    {
        Pistol.SetActive(false);
        ShotGun.SetActive(true);
        Automat.SetActive(false);
        Machinegun.SetActive(false);
        GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().raycastAttack = ShotGun.GetComponent<RaycastAttackLesson>();
    }
    public void ChangeAutomat()
    {
        Pistol.SetActive(false);
        ShotGun.SetActive(false);
        Automat.SetActive(true);
        Machinegun.SetActive(false);
        GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().raycastAttack = Automat.GetComponent<RaycastAttackLesson>();
    }
    public void ChangeMachinegun()
    {
        Pistol.SetActive(false);
        ShotGun.SetActive(false);
        Automat.SetActive(false);
        Machinegun.SetActive(true);
        GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().raycastAttack = Machinegun.GetComponent<RaycastAttackLesson>();
    }
}
