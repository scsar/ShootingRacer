using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float time;
    public float speed=30f;
    public float Decrespeed=-20f;
    public int Hp;
    int RandValue;

    GameObject Player;
    Animator animator;
    PlayerController Playerex;

    new AudioSource audio;
    public GameObject hitparticle;

    //아이템 프리펩
    public GameObject PlayerShieldPreFab;
    public GameObject IncreaseShieldPreFab;
    public GameObject PowerUpPreFab;
    public GameObject TracePreFab;
    
    //오브젝트가 활성화될때
    void OnEnable()
    {
        Hp=3;
    }

    //오브젝트가 비활성화 될때
    void OnDisable()
    {
        time=0f;
    }

    void Awake()
    {
        Player=GameObject.Find("Player");
        Playerex= GameObject.Find("Player").GetComponent<PlayerController>(); ;
    }
    void Start()
    {
        audio=GetComponent<AudioSource>();
        animator=GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.GM.GameOver)
        {
            //오브젝트가 활성화 되면 시간을 흐르게 하며 플레이어 위치정보를 받아옴.
            animator.SetTrigger("Idle");
            time+=Time.deltaTime;
            Vector3 offset= Player.transform.position;
            //플레이어 와 에너미 간의 거리가 일정거리보다 멀때
            if(transform.position.x-offset.x>=15)
            {
                //천천히 접근하게 한다.
                transform.Translate(-1f*0.01f,0,0);
            }
            else
            {
                //지정된 시간이 흐르면, 에너미를 플레이어 쪽으로 접근시킨다.
                if(time>=10)
                {
                    transform.Translate(Decrespeed*Time.deltaTime,0,0);
                }
                else
                {
                //아닌경우 일정거리를 유지하며, 이동한다.
                    transform.Translate(speed*Time.deltaTime,0f,0f);
                }

        
                //접근중 플레이어와 플레이어의 x좌표값보다 작아지게 된다면,
                if(transform.position.x<=offset.x-10)
                {
                    ReturnToPool();     //오브젝트풀 반환 메소드
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Bullet충돌
        if(other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hitparticle,transform.position,Quaternion.identity);    //파티클 생성
            audio.Play();
            animator.SetTrigger("Hit");    //에너미의 애니메이션을 Hit로 변경

            //2개의 발사 포탑에 탄환을 번갈아 가면서 반환하는 로직.
            if(TowerManager.Tower.replace)
            {
                
                TowerManager.Tower.replace=false;
            }
            else
            {
                TowerManager.Tower.replace=true;
            }
            TowerManager.Tower.manager.BulletObjectPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
            Hp-=TowerManager.Tower.Damage;
        }

        //플레이어와 충돌하였을때.
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");      //에너미의 애니메이션을 Attack으로 변경.
            Playerex.DecreaseShield();          //실드게이지 감소 메소드
            ReturnToPool();
        }

            if(Hp<=0)       //Hp가 0이하
            {   
                animator.SetTrigger("Die");     //애니메이션을 Die로 설정.
                RandValue=Random.Range(0,100);  //랜덤한 확률로 4개의 아이템을 Instantiate
                if(RandValue<10)
                {
                    Instantiate(PlayerShieldPreFab,transform.position,Quaternion.identity);
                }
                else if(RandValue < 20)
                {
                    Instantiate(IncreaseShieldPreFab,transform.position,Quaternion.identity);
                }
                else if(RandValue < 30)
                {
                    Instantiate(PowerUpPreFab,transform.position,Quaternion.identity);
                }
                else if(RandValue < 40)
                {
                    Instantiate(TracePreFab,transform.position,Quaternion.identity);
                }
                ReturnToPool();     //에너미 오브젝트풀에 반환
            }
    }

    void ReturnToPool()
    {   
        //제네레이터에 접근해 오브젝트 삽입후 비활성화.
        EnemyGenerator manager = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        manager.EnemyObjectPool.Add(gameObject);
        gameObject.SetActive(false);
    }
}
