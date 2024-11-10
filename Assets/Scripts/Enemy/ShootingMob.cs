using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMob : MonoBehaviour
{
    [Header("ƒл€ стрел€ющих мобов")]
    public float speedShoot = 2f;
    public bool isCanShoot = false;
    public PoolObjects poolBullet;
    public Transform PlaceShoot;
    [Header("ƒл€ стрел€ющих мобов многими снар€дами")]
    public Transform PlaceShoot1;
    public Transform PlaceShoot2;
    public Transform PlaceShoot3;
    public bool isBossUFO;
    public bool isBossWheel;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        if (isBossUFO || isBossWheel)
        {
            startShoot();
            enemyMovement = GetComponent<EnemyMovement>();
        }
    }

    private void Update()
    {
        if (isBossUFO && enemyMovement.goToPlayer)
            isCanShoot = false;
        if (isBossUFO && !enemyMovement.goToPlayer)
            isCanShoot = true;
    }

    public void startShoot()
    {
        poolBullet = GameObject.FindGameObjectWithTag("EnemyBabyPool").GetComponent<PoolObjects>();
        if (isBossUFO)
            StartCoroutine(shootUFO());
        else
            StartCoroutine(shoot());
    }

    public IEnumerator shoot()
    {
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
        {
            if (isCanShoot)
            {
                poolBullet.PlusClon(PlaceShoot);
                yield return new WaitForSeconds(speedShoot);
            }
            else
                yield return null;
        }
        yield return null;
    }
    public IEnumerator shootUFO()
    {
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
        {
            if (isCanShoot)
            {
                if (GameObject.FindGameObjectWithTag("BossUFO").GetComponent<BossScript1>().hp > 0)
                {
                    poolBullet.PlusClon(PlaceShoot);
                    poolBullet.PlusClon(PlaceShoot1);
                    poolBullet.PlusClon(PlaceShoot2);
                    poolBullet.PlusClon(PlaceShoot3);
                    yield return new WaitForSeconds(speedShoot);
                }
                else
                    yield return null;
            }
            else
                yield return null;
        }
        yield return null;
    }
}
//GameObject bullet = Instantiate(poolBullet, PlaceShoot.position, PlaceShoot.rotation);
//Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
//bulletRigidbody.velocity = transform.forward * force;