using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    public TMP_Text win_text;
    public TMP_Text scores_text;

    public void Reset() {
        gameObject.SetActive(false);
        GameManager.me.Reset();
        MainMenu.Show();
    }

    public void OnEnable() {
        string s= "";
        string p = (Player.playerA.score >= Player.playerB.score)? "Player 1" : "Player 2";
        if (Player.playerA.score == Player.playerB.score) s = "Tie!";
        else s = p + " wins!";
    }
}
