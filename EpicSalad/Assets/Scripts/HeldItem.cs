using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a held item can be an ingredient or a salad...
public class HeldItem
{
    public SpriteRenderer sr;

    public Ingredient held_ingredient;
    public Salad held_salad;

    public bool isEmpty {
        get {
            return (held_ingredient == null && held_salad == null);
        }
    }

    public HeldItem(SpriteRenderer sr) {
        this.sr = sr;
    }

    public void Set(Ingredient s) {
        if(s != null) {
            sr.sprite = s.sr.sprite;
            held_ingredient = s;
        }
    }

    public void Set(Salad s) {
        if(s != null) {
            held_salad = s;
            s.SetNewRoot(sr.gameObject);
        }
    }

    public void Clear() {
        sr.sprite = null;
        held_ingredient = null;
        held_salad = null;
    }

    //adds the held item to the salad slot
    public void AddHeldToSalad() {
        if (held_ingredient == null) return;
        if(held_salad == null) {
            held_salad = new Salad();
            held_salad.SetNewRoot(sr.gameObject);
        }
        //move ingredient to salad slot
        held_salad.AddTo(held_ingredient);
        
        sr.sprite = null;
    }

}
