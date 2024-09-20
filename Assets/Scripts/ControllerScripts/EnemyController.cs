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

    //������ ������
    public GameObject PlayerShieldPreFab;
    public GameObject IncreaseShieldPreFab;
    public GameObject PowerUpPreFab;
    public GameObject TracePreFab;
    
    //������Ʈ�� Ȱ��ȭ�ɶ�
    void OnEnable()
    {
        Hp=3;
    }

    //������Ʈ�� ��Ȱ��ȭ �ɶ�
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
            //������Ʈ�� Ȱ��ȭ �Ǹ� �ð��� �帣�� �ϸ� �÷��̾� ��ġ������ �޾ƿ�.
            animator.SetTrigger("Idle");
            time+=Time.deltaTime;
            Vector3 offset= Player.transform.position;
            //�÷��̾� �� ���ʹ� ���� �Ÿ��� �����Ÿ����� �ֶ�
            if(transform.position.x-offset.x>=15)
            {
                //õõ�� �����ϰ� �Ѵ�.
                transform.Translate(-1f*0.01f,0,0);
            }
            else
            {
                //������ �ð��� �帣��, ���ʹ̸� �÷��̾� ������ ���ٽ�Ų��.
                if(time>=10)
                {
                    transform.Translate(Decrespeed*Time.deltaTime,0,0);
                }
                else
                {
                //�ƴѰ�� �����Ÿ��� �����ϸ�, �̵��Ѵ�.
                    transform.Translate(speed*Time.deltaTime,0f,0f);
                }

        
                //������ �÷��̾�� �÷��̾��� x��ǥ������ �۾����� �ȴٸ�,
                if(transform.position.x<=offset.x-10)
                {
                    ReturnToPool();     //������ƮǮ ��ȯ �޼ҵ�
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Bullet�浹
        if(other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hitparticle,transform.position,Quaternion.identity);    //��ƼŬ ����
            audio.Play();
            animator.SetTrigger("Hit");    //���ʹ��� �ִϸ��̼��� Hit�� ����

            //2���� �߻� ��ž�� źȯ�� ������ ���鼭 ��ȯ�ϴ� ����.
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

        //�÷��̾�� �浹�Ͽ�����.
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");      //���ʹ��� �ִϸ��̼��� Attack���� ����.
            Playerex.DecreaseShield();          //�ǵ������ ���� �޼ҵ�
            ReturnToPool();
        }

            if(Hp<=0)       //Hp�� 0����
            {   
                animator.SetTrigger("Die");     //�ִϸ��̼��� Die�� ����.
                RandValue=Random.Range(0,100);  //������ Ȯ���� 4���� �������� Instantiate
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
                ReturnToPool();     //���ʹ� ������ƮǮ�� ��ȯ
            }
    }

    void ReturnToPool()
    {   
        //���׷����Ϳ� ������ ������Ʈ ������ ��Ȱ��ȭ.
        EnemyGenerator manager = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        manager.EnemyObjectPool.Add(gameObject);
        gameObject.SetActive(false);
    }
}
