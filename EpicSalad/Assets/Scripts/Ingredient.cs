using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    public SpriteRenderer sr;

    protected void Awake() {
        sr = GetComponent<SpriteRenderer>();
        base.Awake();
    }

    public override void OnInteract(Player p) {
        p.Pickup(this);
    }
}
