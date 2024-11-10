using UnityEngine;

namespace NewShootingSystem 
{
    public class ChangeWeapon : MonoBehaviour
    {
        public short _currentIdWeapon = 1;
        public GameObject[] _weapons;

        private void Start()
        {
            ChangeWeaponVoid(1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeWeaponVoid(1);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeWeaponVoid(2);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ChangeWeaponVoid(3);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                ChangeWeaponVoid(4);
        }

        public void ChangeWeaponVoid(short id)
        {
            _currentIdWeapon = id;

            foreach (var item in _weapons)
            {
                item.SetActive(false);
            }
            _weapons[_currentIdWeapon - 1].SetActive(true);
            _weapons[_currentIdWeapon - 1].transform.GetChild(0).GetComponent<GunScript>()._anim.Play("Up");
        }
    }
}
