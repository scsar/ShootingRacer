using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if(!GameManager.GM.GameOver)
        {
            //���� ������ �ƴҶ�, �÷��̾�� ī�޶��� �Ÿ����� ���.
            offset=transform.position-player.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(!GameManager.GM.GameOver)
        {
        //ī�޶��� ��ġ�� �÷��̾�� �����Ÿ��� �����ϸ� ���������.
        transform.position=player.transform.position + offset;
        }
    }
}
