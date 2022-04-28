using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public static CustomerController me;
    public GameObject customer_prefab;
    public Sprite c_neutral, c_angry, c_happy;
    public Plate[] customer_plates;
    int plate_counter = 0;

    public List<Customer> customers;

    public static float customer_interval = 10f;
    public static float ingredient_time = 15f;
    float timer = 0f;

    private void Update() {
        timer += Time.deltaTime;
        if(timer >= customer_interval) {
            SpawnCustomer();
            timer = 0f;
        }
    }

    public void RemoveAllCustomers() {
        foreach(Customer c in GetComponentsInChildren<Customer>()) {
            c.RemoveMe();
        }
    }

    public Customer SpawnCustomer() {
        Plate p = GetNextPlate();

        if (p != null) {
            GameObject g = GameObject.Instantiate(customer_prefab);
            g.transform.SetParent(transform);

            g.transform.position = p.transform.position + Vector3.forward * 2f;

            Customer c = g.GetComponent<Customer>();
            c.Set(p, GetRandomOrder());
            customers.Add(c);

            return c;
        }
        return null;
    }

    private void Awake() {
        me = this;
    }

    public void Start() {
        customers = new List<Customer>();
    }

    //Get an UNUSED random plate
    public Plate GetNextPlate() {
        int safety = 0;

        while (!customer_plates[plate_counter].free) {

            if (plate_counter >= customer_plates.Length-1) plate_counter = 0;
            plate_counter++;

            safety++;
            if (safety > 2) return null;
        }
        return customer_plates[plate_counter];

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
