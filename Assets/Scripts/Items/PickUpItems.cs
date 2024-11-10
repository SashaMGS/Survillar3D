using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public bool isGear;
    public bool isExp;
    public int maxValuePlus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isGear)
                GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().gears += Random.Range(4, maxValuePlus);
            if (isExp)
                GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<IndicatorsUI>().experience += Random.Range(6, maxValuePlus);
            gameObject.GetComponent<Animator>().SetBool("pickUp", true);
            StartCoroutine(destroyCoroutine());
            GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().Save();
        }
    }

    IEnumerator destroyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().SetBool("pickUp", false);
        transform.parent.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
