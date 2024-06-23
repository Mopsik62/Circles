using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int id = 0;
    private bool Using = true;
    public bool CanMerge = true;
    public bool HasEntered = false;
    private float timer = 0f;
    void Start()
    {
      if (transform.position.y < Movement.CloudPos.y)
        {
            Using = false;
            HasEntered = true;
            GetComponent<Rigidbody2D>().gravityScale = 1f; //можно сделать ригид боди в определении переменных чтобы 2 раза не нагружать рам
        }
    }

    void Update()
    {
        if (Using)
        {
            CanMerge = false;
            GetComponent<Transform>().position = Movement.CloudPos;
        }
 
    }
    public void Drop()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        Using = false;
        CanMerge = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ((collision.gameObject.tag != "Cloud")&&(HasEntered == false))
        {
            HasEntered = true;
            Movement.Spawned = false; 
        }

        if (collision.gameObject.tag == gameObject.tag )
        {

            if ((collision.gameObject.GetComponent<Object>().id == id) && (collision.gameObject.GetComponent<Object>().CanMerge) && (CanMerge))
            {
                CanMerge = false;
                collision.gameObject.GetComponent<Object>().CanMerge = false;
                Vector3 spawnPosition = (transform.position + collision.transform.position) / 2f;

               /* if (spawnPosition.y > 0)
                {
                    spawnPosition.y = spawnPosition.y * 0.95f;
                }
                else
                {
                    spawnPosition.y = spawnPosition.y * 1.05f;
                }*/
                //GameManager.instance.replaceObjects(collision.gameObject, gameObject, collision.transform.position);
                GameManager.instance.replaceObjects(collision.gameObject, gameObject, spawnPosition);
            }

        }

    }
     //Game over trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Game Over")
        { 
           timer += Time.deltaTime;
            //gameObject.GetComponent<Rigidbody2D>().WakeUp();

            if (timer > GameManager.instance.TimeTillGameOver)
            {
                GameManager.instance.GameOver();

            }

           // Debug.Log(timer);

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Game Over")
        {
            timer = 0f;
        }
    }

}
