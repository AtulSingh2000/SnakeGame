using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foodspawn : MonoBehaviour
{
    public GameObject Foodsprite;
    public GameObject upperwall, lowerwall, rightwall, leftwall;
    public List<GameObject> Food = new List<GameObject>();
    
    void FoodInstance()
    {
        var food = Instantiate(Foodsprite) as GameObject;
        var x = Random.Range(1, 10) * 0.4f;
        var y = Random.Range(1, 10) * 0.4f;
        food.transform.position = new Vector3(x, y, 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FoodInstance", 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Removefood(GameObject food)
    {
        Food.Remove(food);
    }
}
