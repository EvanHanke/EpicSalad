using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public Player my_player;
    public void OnTriggerEnter(Collider other) {
        Player p = other.GetComponent<Player>();
            if(p == my_player) {
                if(type == PickupType.speed) {
                    p.speed_timer = 15f;
                }
                else if(type == PickupType.score) {
                    p.AddScore(25);
                }
                else {
                    p.time += 15;
                }

                GameObject.Destroy(gameObject);
            }
    }
}

public enum PickupType {
speed, score, time}