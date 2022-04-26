using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        //minimum distance to pick up items etc
    public static float interact_distance = 4f; 

        //movement controls
    public bool playerA;
    Rigidbody myRB;
    public float speed = 8f;

        //the spriterenderers which correspond to the currently held items
    public SpriteRenderer sr_held_a;
    public SpriteRenderer sr_held_b;

        //objects which control the held item logic
    HeldItem held_a;
    HeldItem held_b;

        //the most valid interactable near me to use
    public Interactable nearest;



    //instantiation / update loop

    private void Awake() {
        myRB = GetComponent<Rigidbody>();

        held_a = new HeldItem(sr_held_a);
        held_b = new HeldItem(sr_held_b);
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
        if (held_a.isEmpty) held_a.Set(ingredient);
        else if (held_b.isEmpty) held_b.Set(ingredient);
    }

    public void ThrowAway() {
        Place();
    }
        //Place() acts like discarding if return value is ignored
    public Ingredient Place() {
        Ingredient i = null;
        if (!held_b.isEmpty) {
            i = held_b.held_ingredient;
            held_b.Clear();
        }
        else if (!held_a.isEmpty) {
            i = held_a.held_ingredient;
            held_a.Clear();
        }
        return i;
    }

    //
}
