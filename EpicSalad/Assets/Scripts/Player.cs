using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player playerA, playerB;
    public ScoreBar myUI; //score / timer ui
    public int score = 0;
    public float time = 120f;

        //minimum distance to pick up items etc
    public static float interact_distance = 4f; 

        //movement controls
    public bool isplayerA;
    public Rigidbody myRB;
    public float speed = 8f;
    public float speed_timer = 0f;
    public float speed_mod = 1.4f;

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

        if (isplayerA) playerA = this;
        else playerB = this;
    }

    public void AddScore(int s) {
        score += s;
        myUI.SetScore(s);
    }

    private void Update() {
        if (time == -100f) return;//when score menu is shown

        time -= Time.deltaTime;
        if (time < 0f) time = 0f; 
        myUI.SetTimer(time);

        if((playerA.time == 0f && playerB.time == 0f )|| Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.me.ShowScore();
            time = -100f;
        }

        if (speed_timer > 0f) speed_timer -= Time.deltaTime;

        if(time > 0f) {
            //read in keyboard movement input
            Vector2 move = MyInput.GetMovementVector(isplayerA);
            float m = (speed_timer > 0f) ? speed_mod : 1f;
            if (!busy) myRB.velocity = new Vector3(move.x, 0f, move.y) * speed * m;

            //Check for valid interactions
            nearest = Interactable.GetNearest(this);

            //Check for interaction button pressed
            if (MyInput.SelectPressed(isplayerA)) {
                if (nearest != null) nearest.OnInteract(this);
            }

            //Check for interaction button held
            if (MyInput.SelectHeld(isplayerA)) {
                if (nearest != null) nearest.OnInteractHeld(this);
            }

            //Check for interaction button up
            if (MyInput.SelectReleased(isplayerA)) {
                if (nearest != null) nearest.OnInteractReleased(this);
            }
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
            s.player = this;
            return true;
        }
        return false;
    }

    public void ThrowAway() {
        Place();
        Salad s = PlaceSalad();
        if (s != null) s.Flush();
        held_salad = null;
        AddScore(-5);
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
    //reset player
    public void Clear() {
        held_b.Clear();
        held_a.Clear();
        score = 0;
        time = 120f;
    }
}
