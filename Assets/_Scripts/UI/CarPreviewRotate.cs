using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarPreviewRotate : MonoBehaviour
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    new Camera camera;
    float rotateSpeed = 1f;
    void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            
            if(Vector3.Dot(transform.up, Vector3.up)<= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, camera.transform.right), Space.World);

            }
            else{
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, camera.transform.right), Space.World);
            }
           
        }
        else{
           
           transform.Rotate(Vector3.up* rotateSpeed);
           
        }
        mPrevPos = Input.mousePosition;
    }
}
