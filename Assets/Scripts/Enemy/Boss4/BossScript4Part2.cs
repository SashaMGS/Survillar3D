using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript4Part2 : MonoBehaviour
{
    private BossScript4 BossPart1;
    public bool scriptCanStarted;
    public Animator anim;


    void Start()
    {
        BossPart1 = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>();
    }

    public void StartLazer()
    {
        StartCoroutine(LazerStarted());
    }

    private void Update()
    {
        if (BossPart1.hp <= 0)
        {
            BossPart1.canTakeDamageLazer = false;
            anim.SetBool("Die", true);
        }
    }

    IEnumerator LazerStarted()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Attack", true);
        while (BossPart1.hp > 0)
        {
            yield return new WaitForSeconds(1f);
            BossPart1.canTakeDamageLazer = true;
            yield return new WaitForSeconds(20f);
            BossPart1.canTakeDamageLazer = false;
            yield return new WaitForSeconds(1f);
        }
        BossPart1.canTakeDamageLazer = false;
        anim.SetBool("Die", true);
        yield return null;
    }
}
