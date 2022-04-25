using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactable
{
    public override void OnInteract(Player p) {
        p.ThrowAway();
    }
}
