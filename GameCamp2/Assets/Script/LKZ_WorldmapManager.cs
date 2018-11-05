using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_WorldmapManager : MonoBehaviour
{
    //카메라가 움직일수 있는 최대 위치
    [SerializeField] float max_x;
    [SerializeField] float min_x;
    [SerializeField] float max_y;
    [SerializeField] float min_y;

    //처음 터치한 위치
    public Vector3 startMousePos = new Vector3();
    //계속 터치하고 있는 위치
    public Vector3 currentMousePos = new Vector3();
    //카메라 위치
    public Vector3 cameraPosition = new Vector3();

    // Use this for initialization
    void Awake()
    {
        cameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = Camera.main.transform.position;
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            cameraPosition -= new Vector3(currentMousePos.x - startMousePos.x, currentMousePos.y - startMousePos.y, 0.0f);

            if (cameraPosition.x > max_x)
            {
                cameraPosition.x = max_x;
            }
            else if (cameraPosition.x < min_x)
            {
                cameraPosition.x = min_x;
            }

            if (cameraPosition.y > max_y)
            {
                cameraPosition.y = max_y;
            }
            else if (cameraPosition.y < min_y)
            {
                cameraPosition.y = min_y;
            }

            Camera.main.transform.position = cameraPosition;
        }

    }
}
