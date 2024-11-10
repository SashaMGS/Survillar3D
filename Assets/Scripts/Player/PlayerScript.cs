using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public bool canTakeDamage = true;
    [SerializeField] private float delayTakeDMG = 0.1f;
    [Min(0)] public float hp = 100f;
    public float maxHp;
    public float protect;
    public float hpRegen;
    public ParticleSystem damagePart;
    public GameObject damageImg;
    private bool isDead;
    private SaveAll saveAll;
    public AudioSource damageSourse;
    public AudioClip[] damageAudio;
    private void Start()
    {
        canTakeDamage = true;
        saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
        damageImg.SetActive(false);
        if (PlayerPrefs.HasKey("ItemsClass"))
        {
            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
            if (saveAll.saveItemsClass.currentGun == 0)
            {
                GetComponent<PlController>().walkSpeed *= 1.2f;
                GetComponent<PlController>().runSpeed *= 1.2f;
            }
            if (saveAll.saveItemsClass.currentGun == 3)
            {
                GetComponent<PlController>().walkSpeed /= 1.2f;
                GetComponent<PlController>().runSpeed /= 1.2f;
            }



            hp += hp * saveAll.saveItemsClass.skillHPplus * 0.05f;
            protect = saveAll.saveItemsClass.skillProtect * 0.05f;
            hpRegen = saveAll.saveItemsClass.skillRegenHP * 0.05f;
        }
        if (hpRegen > 0)
            StartCoroutine(hpRegenCoroutine());
        maxHp = hp;
    }
    void Update()
    {
        if (hp <= 0 && !isDead)
        {
            hp = 0;
            isDead = true;
            GetComponent<PlController>().canWalk = false;
            StartCoroutine(DeadCoroutine());
        }
        else transform.GetChild(1).gameObject.transform.transform.rotation = transform.GetChild(0).gameObject.transform.rotation;


        if (transform.position.y <= -5 && !isDead)
        {
            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
            if (!saveAll.saveItemsClass.CanShowAch6)
            {
                saveAll.saveItemsClass.CanShowAch6 = true;
                saveAll.Save();
            }
            hp = 0;
            isDead = true;
            GetComponent<PlController>().canWalk = false;
            StartCoroutine(DeadCoroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletEnemy")
        {
            if (canTakeDamage)
            {
                int i = Random.Range(0, 4);
                damageSourse.PlayOneShot(damageAudio[i]);
                damagePart.Play();

                damagePart.Play();
                StartCoroutine(damageCor());
                SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
                float damage = 15f;
                if (saveAll.saveItemsClass.difficulty > 0)
                    damage *= Mathf.Pow(1.15f, saveAll.saveItemsClass.difficulty);
                damage -= damage * saveAll.saveItemsClass.skillProtect * 0.05f;
                hp -= damage;
                StartCoroutine(canTakeDamageCoroutine());
            }
        }
        if (other.tag == "Lazer")
        {
            if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>().canTakeDamageLazer)
            {
                damagePart.Play();
                StartCoroutine(damageCor());
                hp = 0f;
            }
        }
    }

    public void takeDamage(float damage)
    {
        if (hp > 0 && canTakeDamage)
        {
            int i = Random.Range(0, 4);
            damageSourse.PlayOneShot(damageAudio[i]);
            damagePart.Play();

            StartCoroutine(damageCor());
            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
            damage -= damage * saveAll.saveItemsClass.skillProtect * 0.05f;
            hp -= damage;
            StartCoroutine(canTakeDamageCoroutine());
        }
    }
    IEnumerator damageCor()
    {
        yield return new WaitForSeconds(0.1f);
        damageImg.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damageImg.SetActive(false);
        yield break;
    }

    IEnumerator hpRegenCoroutine()
    {
        while (hpRegen > 0)
        {
            SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
            if (hp < maxHp)
                hp += maxHp * saveAll.saveItemsClass.skillRegenHP * 0.0002f;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    IEnumerator DeadCoroutine()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
        saveAll.saveItemsClass.countDeadAll++;
        saveAll.saveItemsClass.countDeadNoWin++;
        saveAll.Save();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<PauseScript>().showWinWave();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Wind").GetComponent<AudioSource>().volume = 0f;
        yield break;
    }

    IEnumerator canTakeDamageCoroutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(delayTakeDMG);
        canTakeDamage = true;
        yield break;
    }
}
