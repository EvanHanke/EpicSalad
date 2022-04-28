using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//plates can hold an ingredient

public class Plate : Interactable
{
    public bool free = true; // is a customer waiting at this plate?

    public HeldItem placed;
    public bool allow_ingredients = true;

    public delegate void OnCheck(Salad s);
    public OnCheck checkSalad;

    //Create a buffer object to hold the sprite of the item on the plate
    void Awake()
    {
        GameObject bufferobj = new GameObject();
        bufferobj.transform.SetParent(transform);
        bufferobj.transform.position = transform.position + Vector3.up * 0.1f;
        bufferobj.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        placed = new HeldItem(bufferobj.AddComponent<SpriteRenderer>());

        base.Awake();
    }

    //put down ingredients / salads
    public override void OnInteract(Player p) {

        Ingredient ing = p.Place();
        if (ing != null) {//put an ingredient down
            
            if(placed.held_ingredient != null) {
                p.Pickup(placed.held_ingredient);
            }
            placed.Set(ing);
        }
        else if (!placed.isEmpty) { //pick a salad or ingredient up
            if (placed.held_ingredient != null) {
                if (p.Pickup(placed.held_ingredient))
                    placed.Clear();
            }
            else if (placed.held_salad != null) {
                if (p.Pickup(placed.held_salad))
                    placed.Clear();
            }

        }
        else if (placed.held_salad == null) { //put a salad down
            Salad s = p.held_salad;
            if (s != null) {
                placed.Set(p.PlaceSalad());
                p.held_salad = null;

                if (checkSalad != null) checkSalad(s); //trigger customer callback
            }
        }

    }

    public void ClearSalad() {
        if(placed.held_salad != null) {
            placed.held_salad.Flush();
            placed.held_salad = null;
        }
    }
}
