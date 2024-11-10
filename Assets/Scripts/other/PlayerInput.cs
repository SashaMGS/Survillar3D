using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string cheatCode;
    void Start()
    {
        cheatCode = "";
        StartCoroutine(delStringCheat());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            cheatCode += "1";
        if (Input.GetKeyDown(KeyCode.DownArrow))
            cheatCode += "2";
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            cheatCode += "3";
        if (Input.GetKeyDown(KeyCode.RightArrow))
            cheatCode += "4";
        if (Input.GetKeyDown(KeyCode.B))
            cheatCode += "5";
        if (Input.GetKeyDown(KeyCode.A))
            cheatCode += "6";
        if (cheatCode.Length == 10 && Time.timeScale > 0f)
        {
            if (cheatCode[0] == '1' &&
                cheatCode[1] == '1' &&
                cheatCode[2] == '2' &&
                cheatCode[3] == '2' &&
                cheatCode[4] == '3' &&
                cheatCode[5] == '4' &&
                cheatCode[6] == '3' &&
                cheatCode[7] == '4' &&
                cheatCode[8] == '5' &&
                cheatCode[9] == '6')
            {
                GetComponent<IndicatorsUI>().AchivementVoid(3);
            }
            cheatCode = "";
        }
    }

    IEnumerator delStringCheat()
    {
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().hp > 0)
        {
            if (cheatCode.Length > 0)
            {
                yield return new WaitForSeconds(10f);
                    cheatCode = "";
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
