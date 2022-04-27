using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    public SpriteRenderer sr;
    public Color color; //color that the particles in the salad appear 

    protected void Awake() {
        sr = GetComponent<SpriteRenderer>();
        base.Awake();
    }

    public override void OnInteract(Player p) {
        p.Pickup(this);
    }
}
