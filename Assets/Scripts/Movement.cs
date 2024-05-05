using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float MovementX;
    static public float CD = 0;
    static public Vector2 CloudPos;
    static public bool Spawned;

    void Start()
    {
        Spawned = true;
        CreateNext();

    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            MovementX = Input.GetAxisRaw("Horizontal");
            GetComponent<Rigidbody2D>().velocity = new Vector2(MovementX * 3.5f, 0);
        }

        if (!Input.GetButton("Horizontal"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        }
        
        CloudPos = transform.position;
        if (!Spawned)
        {
            CreateNext();
            Spawned = true;

        }

    }

    void FixedUpdate()
    {
       
    }
   
    void CreateNext()
    {
        Instantiate(GameManager.instance.massive[Random.Range(0, 2)], transform.position, Quaternion.identity);
    }
    static void CreateById()
    {

    }

}
