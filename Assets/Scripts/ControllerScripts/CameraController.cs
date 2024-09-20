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
            //게임 오버가 아닐때, 플레이어와 카메라의 거리차이 계산.
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
        //카메라의 위치를 플레이어와 일정거리를 유지하며 따라오게함.
        transform.position=player.transform.position + offset;
        }
    }
}
