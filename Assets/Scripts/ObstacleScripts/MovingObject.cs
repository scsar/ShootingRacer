using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveSpeed=30f;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        //������Ʈ�� �ش� String�� �����Ͽ�, ���ϴ� �������� �̵��Ҽ��ְ���.
        if(name.Contains("side"))
        {
            dir=new Vector3(0,-1,-1);
        }
        else if(name.Contains("forward"))
        {
            dir=Vector3.forward;
        }
        else if(name.Contains("back"))
        {
            dir=Vector3.back;
        }
        else if(name.Contains("up"))
        {
            dir=Vector3.up;
        }
        else if(name.Contains("down"))
        {
            dir=Vector3.down;
        }
        else if(name.Contains("right"))
        {
            dir=Vector3.right;
        }
        else if(name.Contains("left"))
        {
            dir=Vector3.left;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*moveSpeed*Time.deltaTime);
    }
}
