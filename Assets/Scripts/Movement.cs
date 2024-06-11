using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputLegacyModule;


public class Movement : MonoBehaviour
{

    public Vector2 spawnPoint;
    static public float CD = 0;
    static public Vector2 CloudPos;
    static public bool Spawned;
    GameObject holdedBall;
    GameObject holdedBall2;



    void Start()
    {
        spawnPoint = transform.position;
        Spawned = true;
        CreateNext();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        bool needToDrop = false;
        Vector3 inputPos = Input.mousePosition;

        if (Input.touchCount > 0)
        {
            Debug.Log("Touched");
            Touch touch = Input.GetTouch(0);
            inputPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector2 position = new Vector2(inputPos.x, transform.position.y);
                GetComponent<Rigidbody2D>().MovePosition(position);
                CloudPos = transform.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                needToDrop = true;
            }
        }
        else // Check for mouse/keyboard input
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            inputPos = mousePos;

            if (mousePos.x != transform.position.x)
            {
                Vector2 position = new Vector2(inputPos.x, transform.position.y);
                GetComponent<Rigidbody2D>().MovePosition(position);
            }

            CloudPos = transform.position;

            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                needToDrop = true;
            }
        }

        CloudPos = transform.position;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1, // ”казатель мыши
            };

            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "PauseButton")
                {
                    Debug.Log("Pointer over UI element: " + result.gameObject.name);
                    needToDrop = false;
                    return; // ѕропуск логики ввода, если указатель над UI
                }
            }
        }


        if (needToDrop)
        {
            holdedBall2.GetComponent<Object>().Drop();
        }

        if (!Spawned)
        {
            CreateNext();
            Spawned = true;

        }

        //if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
       // {
       //     holdedBall2.GetComponent<Object>().Drop();
       // }


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
            //не знаю почему координата y вообще мен€етс€ при плоском столкновении со стеной. ѕока что костыль.
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
        holdedBall2 = Instantiate(GameManager.instance.massive[Random.Range(0, 2)], transform.position, Quaternion.identity);
    }
    static void CreateById()
    {

    }

}
