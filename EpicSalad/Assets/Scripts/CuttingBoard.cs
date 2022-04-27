using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Interactable
{
    const float cutting_time = 2f; 

    public HeldItem placed;

    ChargeBar cb;
    float charge = 0f;

    private void Awake() {
        base.Awake();
        GameObject g = new GameObject();

        //add the graphic
        g.transform.position = transform.position+Vector3.up*0.1f;
        g.transform.SetParent(transform);
        g.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        placed = new HeldItem(g.AddComponent<SpriteRenderer>());

    }


    //activate the cutting board, show the charge bar, pause player
    public override void OnInteract(Player p) {

        Ingredient ing = p.Place();
        if (ing != null) {//put an ingredient down
            placed.Set(ing);

            cb = UIHelper.me.ShowChargeBar(p.transform.position + Vector3.forward * 2f);
            charge = 0f;
            my_player.busy = true;
            my_player.myRB.velocity = Vector3.zero;
        }
        else if (!placed.isEmpty) { //pick a salad or ingredient up
            if (placed.held_ingredient != null) {
                if(p.Pickup(placed.held_ingredient))
                    placed.Clear();
            }
            else if (placed.held_salad != null) { 
                if(p.Pickup(placed.held_salad))
                    placed.Clear();
            }
            
        }
        else if (placed.held_salad == null){ //put a salad down
            Salad s = p.held_salad;
            if(s != null) {
                placed.Set(p.PlaceSalad());
                p.held_salad = null;
            }
        }

    }

    //when the ingredient is cut up
    public void MakeSalad() {
        placed.AddHeldToSalad();
        placed.held_ingredient = null;
        RemoveBar();
    }

    //charge the bar
    public override void OnInteractHeld(Player p) {
        if (cb == null) return;

        cb.SetFillAmt(charge);
        charge += Time.deltaTime / cutting_time;
        if(charge >= 1f) {
            MakeSalad();
        }
    }

    //premature... release
    public override void OnInteractReleased(Player p) {
        if(cb != null) {
            RemoveBar();
            p.Pickup(placed.held_ingredient);
            placed.Clear();
        }
            
    }

    //remove bar
    void RemoveBar() {
        GameObject.Destroy(cb.gameObject);
        my_player.busy = false;
    }
}
