using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int id = 0;
    private bool Using = false;
    public bool CanMerge = false;
    public bool HasEntered = false;
    private float timer = 0f;
    void Start()
    {
        //Debug.Log("Checking");

        if (transform.position.y < Movement.CloudPos.y)
        {
            CanMerge = true;
            HasEntered = true;
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
     // else {
            //Debug.Log(this.gameObject.transform.position.y + this.gameObject.name + Movement.CloudPos.y) ;
           //  }
    }

    void Update()
    {
        //if (CanMerge)
        //{ Debug.Log(this.gameObject.name); }
        if (Using)
        {
            GetComponent<Transform>().position = Movement.CloudPos;
        }
 
    }
    public void Drop()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        Using = false;
        CanMerge = true;
    }
    public void setUsing()
    {
        Using = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }
    public void setMerge()
    {
        HasEntered = true;
    }
    public void setEntered()
    {
        CanMerge = true;
    }
    public void setGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ((collision.gameObject.tag != "Cloud")&&(HasEntered == false) && CanMerge)
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
