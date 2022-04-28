using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHelper : MonoBehaviour
{
    public static PickupHelper me;
    public GameObject[] prefabs;
    public Rect play_area;

    public void Awake() {
        me = this;
    }

    public void Spawn(Player p) {
        int r = (int)(Random.value * prefabs.Length);
        GameObject g = GameObject.Instantiate(prefabs[r]);
        g.transform.SetParent(transform);
        float r_x = Random.value*play_area.width;
        float r_z = Random.value*play_area.height;
        Vector3 pos = new Vector3(play_area.min.x, .2f, play_area.min.y) + new Vector3(r_x, 0f, r_z);
        g.transform.position = pos;
        g.GetComponent<Pickup>().my_player = p;
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(play_area.center, new Vector3(play_area.width, 2f, play_area.height));
    }
}
