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
    private float timer = 0f;
    void Start()
    {
      if (transform.position.y < Movement.CloudPos.y)
        {
            Using = false;
            //Debug.Log("Enterred");
            HasEntered = true;
            GetComponent<Rigidbody2D>().gravityScale = 1f; //����� ������� ����� ���� � ����������� ���������� ����� 2 ���� �� ��������� ���
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Using)
        {
            GetComponent<Transform>().position = Movement.CloudPos;

            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                Using = false;
            }

        }

       
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Cloud"))
            {
            //Debug.Log("cloud");
        }
        if ((collision.gameObject.tag != "Cloud")&&(HasEntered == false))
        {
            //Debug.Log("dsadasd");
            HasEntered = true;
            Movement.Spawned = false; 
        }

        if (collision.gameObject.tag == gameObject.tag )
        {
            //Debug.Log("dsadasd");

            if ((collision.gameObject.GetComponent<Object>().id == id) && (collision.gameObject.GetComponent<Object>().CanMerge) && (CanMerge))
            {
                CanMerge = false;
                collision.gameObject.GetComponent<Object>().CanMerge = false;
                GameManager.instance.replaceObjects(collision.gameObject, gameObject, collision.transform.position);
                // GameManager.instance.replaceObjects(collision.gameObject, gameObject, ((transform.position + collision.transform.position) / 2f ));
            }

        }

    }
     //������� ��� ���� �����
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
