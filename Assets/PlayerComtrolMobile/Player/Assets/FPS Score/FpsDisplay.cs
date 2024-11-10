using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FpsCounter))]
public class FpsDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _averageLabel;
    [SerializeField]
    private Text _highestLabel;
    [SerializeField]
    private Text _lowestLabel;

    [SerializeField]
    private FPSColor[] _fpsColors;

    private FpsCounter _fpsCounter;

    private string[] _stringsFrom00To99 = {
        "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
        "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
        "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
        "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
        "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
        "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
        "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
        "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
        "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
        "100", "101", "102", "103", "104", "105", "106", "107", "108", "109",
        "110", "111", "112", "113", "114", "115", "116", "117", "118", "119",
        "120", "121", "122", "123", "124", "125", "126", "127", "128", "129",
        "130", "131", "132", "133", "134", "135", "136", "137", "138", "139",
        "140", "141", "142", "143", "144", "145", "146", "147", "148", "149",
        "150", "151", "152", "153", "154", "155", "156", "157", "158", "159",
        "160", "161"
    };

    private void Awake()
    {
        _fpsCounter = GetComponent<FpsCounter>();
    }

    private void Update()
    {
        Display(_averageLabel, _fpsCounter.AverageFPS);
        Display(_highestLabel, _fpsCounter.HighestPFS);
        Display(_lowestLabel, _fpsCounter.LowersFPS);
    }

    private void Display(Text label, int fps)
    {
        label.text = _stringsFrom00To99[Mathf.Clamp(fps, 0, 160)];
        for (int i = 0; i < _fpsColors.Length; i++)
        {
            if (fps >= _fpsColors[i].MinFPS)
            {
                label.color = _fpsColors[i].Color;
                break;
            }
        }
    }

    [Serializable]
    private struct FPSColor
    {
        public Color Color;
        public int MinFPS;
    }
}
