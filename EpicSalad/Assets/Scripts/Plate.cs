using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//plates can hold an ingredient

public class Plate : Interactable
{
    public HeldItem held;

    //Create a buffer object to hold the sprite of the item on the plate
    void Awake()
    {
        GameObject bufferobj = new GameObject();
        bufferobj.transform.SetParent(transform);
        bufferobj.transform.position = transform.position + Vector3.up * 0.1f;
        bufferobj.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        held = new HeldItem(bufferobj.AddComponent<SpriteRenderer>());

        base.Awake();
    }

    public override void OnInteract(Player p) {
        if (held.isEmpty) held.Set(p.Place());
        else {
            p.Pickup(held.held_ingredient);
            held.Clear();
        }
    }
}
