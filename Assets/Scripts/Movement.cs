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
            Touch touch = Input.GetTouch(0);
            inputPos = Camera.main.ScreenToWorldPoint(touch.position);

            // Проверка, находится ли указатель касания над элементом UI
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    pointerId = touch.fingerId,
                };

                pointerData.position = touch.position;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.name == "PauseButton")
                    {
                        //Debug.Log("Pointer over UI element: " + result.gameObject.name);
                        needToDrop = false;
                        return; // Пропуск логики ввода, если указатель над UI
                    }
                }
            }

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

            // Проверка, находится ли указатель мыши над элементом UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    pointerId = -1, // Указатель мыши
                };

                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.name == "PauseButton")
                    {
                        // Debug.Log("Pointer over UI element: " + result.gameObject.name);
                        needToDrop = false;
                        return; // Пропуск логики ввода, если указатель над UI
                    }
                }
            }

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

        if (needToDrop)
        {
            holdedBall.GetComponent<Object>().Drop();
        }

        if (!Spawned)
        {
            CreateNext();
            Spawned = true;
        }
    }
    void CreateNext()
    {
        holdedBall = Instantiate(GameManager.instance.massive[Random.Range(0, 2)], transform.position, Quaternion.identity);
    }
    static void CreateById()
    {

    }

}