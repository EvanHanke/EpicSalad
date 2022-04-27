using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        //minimum distance to pick up items etc
    public static float interact_distance = 4f; 

        //movement controls
    public bool playerA;
    public Rigidbody myRB;
    public float speed = 8f;

        //the spriterenderers which correspond to the currently held items
    public SpriteRenderer sr_held_a;
    public SpriteRenderer sr_held_b;

        //objects which control the held item logic
    HeldItem held_a;
    HeldItem held_b;
    public Salad held_salad;

        //the most valid interactable near me to use
    public Interactable nearest;

        //if busy, cannot move
    public bool busy = false;


    //instantiation / update loop
    private void Awake() {
        myRB = GetComponent<Rigidbody>();

        held_a = new HeldItem(sr_held_a);
        held_b = new HeldItem(sr_held_b);
    }

    private void Update() {

            //read in keyboard movement input
        Vector2 move = MyInput.GetMovementVector(playerA);
        if(!busy) myRB.velocity = new Vector3(move.x, 0f, move.y) * speed;

            //Check for valid interactions
        nearest = Interactable.GetNearest(this);

            //Check for interaction button pressed
        if (MyInput.SelectPressed(playerA)) {
            if (nearest != null) nearest.OnInteract(this);
        }

            //Check for interaction button held
        if (MyInput.SelectHeld(playerA)) {
            if (nearest != null) nearest.OnInteractHeld(this);
        }

            //Check for interaction button up
        if (MyInput.SelectReleased(playerA)) {
            if (nearest != null) nearest.OnInteractReleased(this);
        }
    }



    //basic picking up and throwing away behaviors

    public bool Pickup(Ingredient ingredient) {
        if (held_salad != null) return false;
        if (held_a.isEmpty) {
            held_a.Set(ingredient);
            return true;
        }
        else if (held_b.isEmpty) {
            held_b.Set(ingredient);
            return true;
        }
        return false;
    }

    public bool Pickup(Salad s) {
        if (held_a.isEmpty && held_b.isEmpty && held_salad == null) {
            held_salad = s;
            s.SetNewRoot(sr_held_a.gameObject);
            return true;
        }
        return false;
    }

    public void ThrowAway() {
        Place();
        Salad s = PlaceSalad();
        if (s != null) s.Flush();
        held_salad = null;
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

    public Salad PlaceSalad() {
        if (held_salad != null) {
            Salad s = held_salad;
            return s;
        }
        else return null;
    }

    //
}
