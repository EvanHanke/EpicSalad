using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer:MonoBehaviour
{
    public static int number_of = 0;

    public Plate my_plate;
    public Ingredient[] my_order;

    float timer = 0f;
    float timer_max = 0f;
    float time_mod = 1f;

    int minus_points = -10;
    bool angry = false;

    ChargeBar bar;

    List<Player> bad_players;

    public void Set(Plate p, Ingredient[] order) {
        bad_players = new List<Player>();

        my_plate = p;
        my_order = order;
        p.free = false;

        for(int i = 0; i < order.Length; i++) {
            //make a mini version of the ingredient 
            GameObject g = new GameObject();
            g.transform.SetParent(transform);
            g.AddComponent<SpriteRenderer>().sprite = order[i].sr.sprite;
            g.transform.localScale = Vector3.one * 0.5f;
            g.transform.position = transform.position + new Vector3(-1f + 1f * i, 0.25f, -1.5f);
            g.transform.rotation = transform.rotation;
        }

        p.checkSalad += CheckSalad;
        number_of++;
        timer_max = order.Length * CustomerController.ingredient_time;
        timer = timer_max;
        bar = UIHelper.me.ShowChargeBar(transform.position + new Vector3(0f, 0f, 1.5f));
        bar.GetComponentInChildren<TMPro.TMP_Text>().text = "time";
    }

    private void Update() {
        timer -= Time.deltaTime * time_mod;
        bar.SetFillAmt(timer / timer_max);
        if(timer < 0f) {
            RemoveMe();
            //lose points when timed out
            foreach(Player p in bad_players) {
                int mod = 1;
                if (angry) mod = 2;
                p.AddScore(minus_points * mod);
            }
        }
    }

    public void RemoveMe() {
        my_plate.ClearSalad();
        my_plate.checkSalad = null;
        my_plate.free = true;
        GameObject.Destroy(gameObject);
        GameObject.Destroy(bar.gameObject);
        number_of--;
    }

    public void SetAngry() {
        GetComponent<SpriteRenderer>().sprite = CustomerController.me.c_angry;
        time_mod = 2f;
    }

    public void Satisfy(Salad s) {
        if(timer >= timer_max * 0.6f) {
            PickupHelper.me.Spawn(s.player);
        }
        s.player.AddScore(my_order.Length * 10);
        RemoveMe();
    }


    //goes through each ingredient, if ingredient is missing, exit out of the loop
    public void CheckSalad(Salad s) {
        bool valid = true;
        //create a new list of salad ingredients based on salad parts with no dupes
        List<Ingredient> s_a = new List<Ingredient>(); 
        foreach(SaladPart sp in s.saladParts) {
            if(!s_a.Contains(sp.ingredient))
                s_a.Add(sp.ingredient);
        }
        //check
        if (my_order.Length != s_a.Count) valid = false;
        else {
            foreach (Ingredient i in my_order) {
                if (!s_a.Contains(i)) {
                    valid = false;
                    break;
                }
            }
        }

        if (valid) {
            Satisfy(s);
        }
        else {
            SetAngry();
            if (!bad_players.Contains(s.player)) bad_players.Add(s.player);
        }
    }
}
