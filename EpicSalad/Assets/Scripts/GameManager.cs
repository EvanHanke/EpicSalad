using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager me;
    public GameObject ScoreUI;

    private void Awake() {
        me = this;
    }

    public void ShowScore() {
        ScoreUI.gameObject.SetActive(true);
    }

    public void Reset() {
        CustomerController.me.RemoveAllCustomers();
        foreach(Plate p in GetComponentsInChildren<Plate>(true)) {
            p.placed.Clear();
        }
        foreach (CuttingBoard c in GetComponentsInChildren<CuttingBoard>(true)) {
            c.placed.Clear();
        }
        foreach(Player p in GetComponentsInChildren<Player>(true)) {
            if (p.held_salad != null) p.held_salad.Flush();
            p.held_salad = null;
            p.Clear();
        }
        
    }
}
