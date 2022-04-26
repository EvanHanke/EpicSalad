using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a held item can be an ingredient or a salad...
public class HeldItem
{
    public SpriteRenderer sr;
    public Ingredient held_ingredient;

    public bool isEmpty {
        get {
            return sr.sprite == null;
        }
    }

    public HeldItem(SpriteRenderer sr) {
        this.sr = sr;
    }

    public void Set(Ingredient s) {
        sr.sprite = s.sr.sprite;
        held_ingredient = s;
    }

    public void Clear() {
        sr.sprite = null;
        held_ingredient = null;
    }
}
