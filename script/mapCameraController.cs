using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCameraController : MonoBehaviour
{
    public bool isMap = false ;

    public GameObject MapUI;

    public Vector2 range;//y为最大值，x为最小值
    public float scrollSpeed;
    public Camera c;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            c.orthographicSize -= Input.mouseScrollDelta.y * scrollSpeed;
            c.orthographicSize = Mathf.Clamp(c.orthographicSize, range.x, range.y);
            Vector3 direction;
            if (Input.mouseScrollDelta.y > 0)
            {
                if (c.orthographicSize > range.x)
                {
                    Vector3 mousePos = c.ScreenToWorldPoint(Input.mousePosition);
                    direction = new Vector3(mousePos.x, mousePos.y, 0) - new Vector3(transform.position.x, transform.position.y, 0);
                    c.transform.Translate(direction / c.orthographicSize);
                }
            }
            else if (c.orthographicSize < range.y)
            {
                direction = Vector3.zero - new Vector3(transform.position.x, transform.position.y, 0);
                c.transform.Translate(direction / (54 - c.orthographicSize));
            }
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (isMap)
            {
                ExitMap();
            }
            else
            {
                EnterMap();
            }
        }

        
    }
    public void ExitMap()
    {
        MapUI.SetActive(false);
        Time.timeScale = 1.0f;
        isMap = false;
    }

    public void EnterMap()
    {
        MapUI.SetActive(true);
        Time.timeScale = 0f;
        isMap = true;
    }
}
