using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject particle;
    new AudioSource audio;
    Slider ShieldGauage;   //HP 게이지
    private bool isRotating;
    public float rotationSpeed=100f;
    public float speed = 30f;


    void Start()
    {
        audio=GetComponent<AudioSource>();
        ShieldGauage=GameObject.Find("ShieldGauage").GetComponent<Slider>();
    }

    void Update()
    {   
        transform.Translate(speed*Time.deltaTime,0f,0f);    //플레이어 이동코드 플레이어는 기본적으로 앞으로만 이동한다.

        if(ShieldGauage.value<=0.2f)  //빈사 상태일떄.
        {
            Instantiate(particle,transform.position,Quaternion.identity);
        }

        //마우스를 누를때
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        //isRotating 이 활성화 되었을때
        if (isRotating)
        {   
            //마우스 클릭(터치) 위치에 따라서 화면넓이를 반으로 나누어 클때는 왼쪽, 작을때는 오른쪽으로 회전하게 한다.
            float direction = Input.mousePosition.x > Screen.width / 2 ? -1.0f : 1.0f;

            // 방향에 따라 회전
            transform.Rotate( direction * rotationSpeed * Time.deltaTime,0,0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //장애물과 충돌하였고, 실드아이템을 보유하고있지않은 경우
        if(other.gameObject.CompareTag("Obstacle") && ! TowerManager.Tower.ObstacleShield )
        {
            GameManager.GM.GameOver=true;
            Destroy(transform.root.gameObject); 
        }
        //장애물과 충돌하였지만, 실드아이템을 보유하고있는경우
        else if(other.gameObject.CompareTag("Obstacle") && TowerManager.Tower.ObstacleShield)
        {
            TowerManager.Tower.ObstacleShield=false;
        }

        //끝에 도달하였을때
        if(other.gameObject.CompareTag("Finish"))
        {
            GameManager.GM.Clear=true;
        }
    }


    //실드 감소 메소드
    public void DecreaseShield()
    {
        audio.Play();
        ShieldGauage.value-=0.1f;
        //실드게이지가 모두 소진되었을떄 오브젝트 파괴.
        if(ShieldGauage.value==0)
        {
            Destroy(gameObject);
        }
    }

    //실드 증가 메소드
    public void IncreaseShield()
    {
       ShieldGauage.value+=0.1f;
    }

}
