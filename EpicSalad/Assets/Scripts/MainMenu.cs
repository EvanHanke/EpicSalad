using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu me;
    public GameObject gameArea;

    public void Awake() {
        me = this;
    }

    public void Play() {
        gameObject.SetActive(false);
        gameArea.SetActive(true);
    }

    public static void Show() {
        me.gameArea.SetActive(false);
        me.gameObject.SetActive(true);
    }
}
