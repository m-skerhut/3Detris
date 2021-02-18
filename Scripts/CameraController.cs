using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float camMoveSpeed;

    float yaw = 0;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            yaw += camMoveSpeed * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        }
    }
}
