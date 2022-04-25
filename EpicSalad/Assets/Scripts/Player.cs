using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float interact_distance = 4f; //minimum distance to pick up items etc

    public bool playerA; // is this player 1 or player 2?
    public float speed = 8f; //default move speed = 8

    //the spriterenderers which correspond to the currently held items
    public SpriteRenderer sr_held_a;
    public SpriteRenderer sr_held_b;

    public Interactable nearest; //the most valid interactable near me to use

    Rigidbody myRB;
    private void Awake() {
        myRB = GetComponent<Rigidbody>();
    }

    private void Update() {

        //read in keyboard movement input
        Vector2 move = MyInput.GetMovementVector(playerA); 
        myRB.velocity = new Vector3(move.x, 0f, move.y) * speed;

        //Check for valid interactions
        nearest = Interactable.GetNearest(this);

        //Check for interaction button pressed
        if (MyInput.SelectPressed(playerA)) {
            if (nearest != null) nearest.OnInteract(this);
        }
    }

    //basic picking up and throwing away behaviors
    public void Pickup(Ingredient ingredient) {
        if (sr_held_a.sprite == null) sr_held_a.sprite = ingredient.sr.sprite;
        else if (sr_held_b.sprite == null) sr_held_b.sprite = ingredient.sr.sprite;
    }

    public void ThrowAway() {
        if (sr_held_b.sprite != null) sr_held_b.sprite = null;
        else if (sr_held_a.sprite != null) sr_held_a.sprite = null;
    }
}
