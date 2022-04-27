using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton in charge of instantiating prefab instances

public class UIHelper : MonoBehaviour
{
    public static UIHelper me;
    public GameObject chargebar;
    public Sprite circle_sprite;

    private void Awake() {
        me = this;
    }

    //instantiate prefab at position, return controller
    public ChargeBar ShowChargeBar(Vector3 world_pos) {
        GameObject g = GameObject.Instantiate(chargebar);
        g.transform.SetParent(transform);

        Vector3 screen_pos = Camera.main.WorldToScreenPoint(world_pos);
        g.transform.position = screen_pos;
        return g.GetComponent<ChargeBar>();
    }
}
