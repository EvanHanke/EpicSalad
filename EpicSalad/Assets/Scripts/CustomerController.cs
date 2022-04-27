using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public GameObject customer_prefab;
    public Sprite c_neutral, c_angry, c_happy;
    public Plate[] customer_plates;

    public Customer SpawnCustomer() {
        GameObject g = GameObject.Instantiate(customer_prefab);
        g.transform.SetParent(transform);

        int plate = (int)(Random.value * customer_plates.Length);
        Plate p = customer_plates[plate];
        g.transform.position = p.transform.position + Vector3.forward * 2f;

        Customer c = g.GetComponent<Customer>();
        c.Set(p, GetRandomOrder());
        return c;
    }

    public void Start() {
        SpawnCustomer();
    }

    //get 2-3 random ingredients and pass them back in an array
    //ingredients must be unique
    public Ingredient[] GetRandomOrder() {
        int range = Mathf.RoundToInt(Random.value) + 2;
        Ingredient[] ings = new Ingredient[range];

        //brute force dont repear numbers
        List<int> used = new List<int>();
        for(int i = 0; i < range; i++) {
            int r = (int)(Ingredient.all_ingredients.Count * Random.value);
            while (used.Contains(r)) {
                r = (int)(Ingredient.all_ingredients.Count * Random.value);
            }
            used.Add(r);
            ings[i] = Ingredient.all_ingredients[r];
        }
        return ings;
    }
}
