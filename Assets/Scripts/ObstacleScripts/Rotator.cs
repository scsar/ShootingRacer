using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 45f;

    void Update()
    {
        //오브젝트에 해당 String을 기입하여, 원하는 방향으로 회전할수있게함
        if (gameObject.name.Contains("x"))
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
        else if (gameObject.name.Contains("y"))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}
