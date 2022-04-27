using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    public SpriteRenderer sr;
    public Color color; //color that the particles in the salad appear
    public static List<Ingredient> all_ingredients;

    protected void Awake() {
        sr = GetComponent<SpriteRenderer>();
        if(all_ingredients == null) {
            all_ingredients = new List<Ingredient>();
        }
        all_ingredients.Add(this);
        base.Awake();
    }

    public override void OnInteract(Player p) {
        p.Pickup(this);
    }
}
