using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//salads are made of part, which is an ingrediant and the visual representation of the ingredient
public class Salad
{
    public List<SaladPart> saladParts;
    public GameObject prefab_root; //holds for the particle objects

    public Player player; //player who last touched this salad

    public Salad() {
        saladParts = new List<SaladPart>();
        prefab_root = new GameObject();

        Debug.Log("new salad");
    }

    public void SetNewRoot(GameObject g) {
        prefab_root.transform.SetParent(g.transform, false);
    }

    public void AddTo(Ingredient ing) {
        saladParts.Add(new SaladPart(ing, this));
    }

    public void Flush() {
        foreach(SaladPart sp in saladParts) {
            GameObject.Destroy(sp.particles);
        }
        saladParts.Clear();
    }


    //generate a gameobject with a couple simple sprite primitaves as salad effect
    public static GameObject CreateIngParticles(Ingredient ing) {
        GameObject g = new GameObject();
        Color c = ing.color;
        int amt = 5;
        for(int i = 0; i < amt; i++) {
            GameObject a = new GameObject();
            SpriteRenderer sr = a.AddComponent<SpriteRenderer>();
            sr.sprite = UIHelper.me.circle_sprite;
            sr.color = c;
            float r_x = 1f*(Random.value-0.5f);
            float r_z = 1f*(Random.value-0.5f);
            sr.transform.position = new Vector3(r_x, r_z, 0f);
            sr.transform.localScale = Vector3.one / 2f;
            sr.transform.SetParent(g.transform);
        }
        return g;
    }
}

public class SaladPart {
    public Ingredient ingredient;
    public GameObject particles;

    public SaladPart(Ingredient ing, Salad s) {
        particles = Salad.CreateIngParticles(ing);
        ingredient = ing;
        particles.transform.SetParent(s.prefab_root.transform, false);
        particles.transform.position = s.prefab_root.transform.position + Vector3.up * 0.1f;
    }
}