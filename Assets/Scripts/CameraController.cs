using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float screenBorder = 30f;
    public float speedOfTheCamera = 10f;
    public float scrollMoveSpeed = 500f;
    public Vector2 cameraXLimits;
    public Vector2 cameraZLimits;
    public Vector2 cameraYLimits;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePositionT = transform.position;
        
        if (Input.mousePosition.y >= Screen.height - screenBorder || Input.GetKey(KeyCode.W))
        {
            mousePositionT.z += speedOfTheCamera * Time.deltaTime;

        }
        if (Input.mousePosition.y <= screenBorder || Input.GetKey(KeyCode.S))
        {
            mousePositionT.z -= speedOfTheCamera * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - screenBorder || Input.GetKey(KeyCode.D))
        {
            mousePositionT.x += speedOfTheCamera * Time.deltaTime;
        }
        if (Input.mousePosition.x <= screenBorder || Input.GetKey(KeyCode.A))
        {
            mousePositionT.x -= speedOfTheCamera * Time.deltaTime;

        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mousePositionT.y -= scroll * scrollMoveSpeed * Time.deltaTime;
        mousePositionT.y = Mathf.Clamp(mousePositionT.y, cameraYLimits.x, cameraYLimits.y);
        mousePositionT.x = Mathf.Clamp(mousePositionT.x, -cameraXLimits.x, cameraXLimits.y);
        mousePositionT.z = Mathf.Clamp(mousePositionT.z, -cameraZLimits.x, cameraZLimits.y);
        transform.position = mousePositionT;
    }
}