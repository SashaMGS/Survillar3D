using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreateObjects : MonoBehaviour
{
    [Min(0)] public int countGrassObj;
    [Min(0)] public int countStoneObj;
    [Min(0)] public float minPosX = 30f;
    [Min(0)] public float minPosZ = 30f;
    public GameObject GrassPref;
    public GameObject StonePref;

    private void Awake()
    {
        if (countGrassObj > 0)
            for (int i = 0; i < countGrassObj; i++)
            {
                GameObject GrassPrefObj = Instantiate(GrassPref, new Vector3(Random.Range(-minPosX, minPosX), 0, Random.Range(-minPosZ, minPosZ)), transform.rotation);
                GrassPrefObj.transform.localScale = new Vector3(1, Random.Range(0.6f, 1), 1);
            }
        if (countStoneObj > 0)
            for (int i = 0; i < countStoneObj; i++)
        {
            GameObject StonePrefObj = Instantiate(StonePref, new Vector3(Random.Range(-minPosX, minPosX), -0.05f, Random.Range(-minPosZ, minPosZ)), transform.rotation);
            StonePrefObj.transform.localScale = new Vector3(Random.Range(0.5f, 1), Random.Range(0.1f, 0.2f), Random.Range(0.5f, 1));
        }
    }
}
