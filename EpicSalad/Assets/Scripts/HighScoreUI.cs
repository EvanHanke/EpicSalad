using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    public TMP_Text win_text;
    public TMP_Text scores_text;

    public void Reset() {
        GameObject.Destroy(gameObject);
        MainMenu.Show();
    }
}
