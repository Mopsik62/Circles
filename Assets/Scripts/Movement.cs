using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Vector2 spawnPoint;
    static public float CD = 0;
    static public Vector2 CloudPos;
    static public bool Spawned;
    RaycastHit2D hit;
    Ray ray;

    void Start()
    {
        spawnPoint = transform.position;
        Spawned = true;
        CreateNext();

    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        {
            Debug.Log("Mouse = " + mousePos.x);
        }

        if (mousePos.x != transform.position.x)
        {
            Vector2 position = new Vector2(mousePos.x, transform.position.y);
            GetComponent<Rigidbody2D>().MovePosition(position);

        }

        CloudPos = transform.position;
        if (!Spawned)
        {
            CreateNext();
            Spawned = true;

        }



    }

  /*  void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        {
            Debug.Log("Mouse = " + mousePos.x);
        }

        if (mousePos.x != transform.position.x)
        { 
            //не знаю почему координата y вообще меняется при плоском столкновении со стеной. Пока что костыль.
            Vector2 position = new Vector2(mousePos.x, transform.position.y);
            GetComponent<Rigidbody2D>().MovePosition(position);

        }

        CloudPos = transform.position;
        if (!Spawned)
        {
            CreateNext();
            Spawned = true;

        }
    }*/
   
    void CreateNext()
    {
        Instantiate(GameManager.instance.massive[Random.Range(0, 2)], transform.position, Quaternion.identity);
    }
    static void CreateById()
    {

    }

}
