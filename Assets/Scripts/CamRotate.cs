using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public GameObject target;
    private float rotateSpeed = 3.0f;
    private Vector3 point;

    void Start()
    {
        point = target.transform.position;
        transform.LookAt(point);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed);
            //transform.Translate(Vector3.left * Time.deltaTime * rotateSpeed);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), -20 * Time.deltaTime * rotateSpeed);
            //transform.Translate(Vector3.right * Time.deltaTime * rotateSpeed);
        }

        //transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed);
    }
}
