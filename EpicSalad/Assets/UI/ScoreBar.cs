using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text timer;

    public void SetScore(int s) {
        score.text = "Score: " + s.ToString("D3");
    }

    public void SetTimer(float t) {
        timer.text = "Time: " + ((int)t).ToString("D3");
    }
}
