using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 45f;

    void Update()
    {
        //������Ʈ�� �ش� String�� �����Ͽ�, ���ϴ� �������� ȸ���Ҽ��ְ���
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
