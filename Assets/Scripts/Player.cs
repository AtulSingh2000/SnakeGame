using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject Foodsprite;
    public Vector3 direction;
    public float speed;
    public Text text;
    public int life=3;
    public bool isAlive = true;
    public GameObject Gameover,Healthimage,Tail, borderLeft, borderRight, borderBottom, borderTop;
    private float width;
    public GameObject Button;
    
    private List<GameObject> tails = new List<GameObject>();
    public List<GameObject> Food = new List<GameObject>();
    public GameObject can;

    void Start()
    {

        Foodsprite.SetActive(true);
        can.SetActive(true);
        Healthimage.SetActive(true);
        direction = Vector3.right;
        var sprite = GetComponent<SpriteRenderer>().sprite;
        width = sprite.rect.width / sprite.pixelsPerUnit;
        InvokeRepeating("Move", 0, 0.1f);
        Food.Add(Foodsprite);
    }
    private void Move()
    {
        Vector3 prevPos = transform.position;
        Vector3 current = prevPos + (direction * width);
        transform.position = current;

        for (int i=0;i<tails.Count;i++)
        {
            var temp = prevPos;
            prevPos = tails[i].transform.position;
            tails[i].transform.position = temp;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("destroy"))
        {
           
            if (life > 0  )
            {                
                life--;
                transform.position = new Vector2(0,0);
                text.text = life.ToString();
            }
            else if(life==0)
            {
                Destroy(gameObject);
                Gameover.SetActive(true);
                Button.SetActive(true);
                
                can.SetActive(false);
                Healthimage.SetActive(false);
               
                Foodsprite.SetActive(false);
                
                

            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Helo");
            var tail = Instantiate(Tail) as GameObject;
            var prevPos = transform.position;
            transform.position = collision.gameObject.transform.position;
            Removefood(collision.gameObject);
            Destroy(collision.gameObject);
            tail.transform.position = prevPos;
            tails.Insert(0, tail);
            int x = (int)Random.Range(borderLeft.transform.position.x,
                               borderRight.transform.position.x);
            int y = (int)Random.Range(borderBottom.transform.position.y,
                                      borderTop.transform.position.y);

            // Instantiate the food at (x, y)
            var food = Instantiate(Foodsprite) as GameObject;
            food.transform.position = new Vector2(x, y);

            

        }
    }
    public void Removefood(GameObject food)
    {
        Food.Remove(food);
    }


    // Update is called once per frame
    void Update()
    {


        //rb.velocity = direction * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;

        }

    }
}
