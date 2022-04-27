using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer:MonoBehaviour
{

    public Plate my_plate;
    public Ingredient[] my_order;

    public void Set(Plate p, Ingredient[] order) {
        my_plate = p;
        my_order = order;

        for(int i = 0; i < order.Length; i++) {
            //make a mini version of the ingredient 
            GameObject g = new GameObject();
            g.transform.SetParent(transform);
            g.AddComponent<SpriteRenderer>().sprite = order[i].sr.sprite;
            g.transform.localScale = Vector3.one * 0.5f;
            g.transform.position = transform.position + new Vector3(-1f + 1f * i, -0.1f, 1.5f);
            g.transform.rotation = transform.rotation;
        }
    }
}
