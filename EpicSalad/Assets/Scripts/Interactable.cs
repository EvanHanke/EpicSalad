using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines blueprint for all interactable objects,
//ingredients, cutting boards, customers
public class Interactable : MonoBehaviour
{
    public Player my_player; //the player using this interactable
    public static List<Interactable> all_interactables;
    Vector3 base_scale;


    public virtual void OnInteract(Player p) {

    }

    public virtual void OnInteractHeld(Player p) {
        //for when the interact button is held i.e the cutting board
    }
    public virtual void OnInteractReleased(Player p) {
        //for when the interact button is released i.e the cutting board
    }


    //Method for returning the nearest valid interactable object to the given player
    //Used to decide what to interact with when interact button is pressed
    public static Interactable GetNearest(Player p) {
        Interactable nearest = null;
        float m = Player.interact_distance;
        foreach(Interactable i in all_interactables) {
            if(i.my_player == null || i.my_player == p) {
                float x = Vector3.Distance(i.transform.position, p.transform.position);
                if (x < m) {
                    nearest = i;
                    m = x;
                }
            }
        }
        if (p.nearest != null) p.nearest.my_player = null; //remove link between old nearest
        if (nearest != null) nearest.my_player = p; //create new link between nearest
        return nearest;
    }

    protected void Awake() {
        if (all_interactables == null) all_interactables = new List<Interactable>();
        all_interactables.Add(this);
        base_scale = transform.localScale;
    }


    //simple animation when nearby
    public void Update() {
        if (my_player != null) {
            transform.localScale = base_scale + (Vector3.one * Mathf.Sin(Time.time * 2f) * 0.2f);
        }
        else transform.localScale = Vector3.MoveTowards(transform.localScale, base_scale, Time.deltaTime);
    }
}
