using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
    public int id = 0;
    private bool Using = true;
    public bool CanMerge = true;
    public bool HasEntered = false;
    void Start()
    {
      if (transform.position.y < 2.4f)
        {
            Using = false;
            HasEntered = true;
            GetComponent<Rigidbody2D>().gravityScale = 1f; //можно сделать ригид боди в определении переменных чтобы 2 раза не нагружать рам
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Using)
        {
            GetComponent<Transform>().position = Movement.CloudPos;
        }
        if (Input.GetKeyDown("space") && Using)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            Using = false;
           // Movement.Spawned = false;
           // Movement.CD = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if ((collision.gameObject.tag != "Cloud")&&(HasEntered == false))
        {
            Debug.Log(collision.gameObject.tag);
            HasEntered = true;
            Movement.Spawned = false; }

        if (collision.gameObject.tag == gameObject.tag )
        {
            //Debug.Log(collision.GetContact(0).point);
            if ((collision.gameObject.GetComponent<Object>().id == id) && (collision.gameObject.GetComponent<Object>().CanMerge) && (CanMerge))
            {
                CanMerge = false;
                collision.gameObject.GetComponent<Object>().CanMerge = false;

                GameManager.instance.replaceObjects(collision.gameObject, gameObject, ((transform.position + collision.transform.position) / 2f ));

            }

        }
    }
}
