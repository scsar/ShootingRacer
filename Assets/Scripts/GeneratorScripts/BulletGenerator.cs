using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletGenerator : MonoBehaviour
{
    new AudioSource audio;

    float time;
    float SpawnTime=0.7f;
    public GameObject BulletpreFab;
    bool Trace;
    EnemyController enemyController;

    public List<GameObject> BulletObjectPool;
    int poolSize=10;

    void Start()
    {
        audio=GetComponent<AudioSource>();
        Trace=false;
        //오브젝트풀 생성.
        for(int i=0;i<poolSize;i++)
        {
            GameObject bullet= Instantiate(BulletpreFab);
            BulletObjectPool.Add(bullet);
            bullet.SetActive(false);
        }
    }

    void Update()
    {
        
        time+=Time.deltaTime;
        //일정시간이 지나면 Bullet을 생성.
        if(SpawnTime<time)
        {
            time=0f;
            if(BulletObjectPool.Count>0)
            {
                GameObject bullet = BulletObjectPool[0];
                BulletObjectPool.Remove(bullet);
                bullet.transform.position=transform.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.GetComponent<Rigidbody>().velocity = Vector3.zero; 
                //트레이스 아이템 획득시 발동되는 조건문
                if(Trace)
                {
                    enemyController = FindObjectOfType<EnemyController>();
                    //EnemyController 를 가지고있는 enemy를 찾는다.
                    if(enemyController != null)
                    {   
                        //발견하면, 해당오브젝트의 위치를 SetTarget메소드로 전달한다.
                        bullet.GetComponent<BulletController>().SetTarget(enemyController.transform);
                    }
                    else
                    {
                        //현재 enemy가 존재하지 않는경우, null을 반환한다.
                        bullet.GetComponent<BulletController>().SetTarget(null);
                    }
                }
                bullet.SetActive(true);
                if(audio!=null)
                {
                    audio.Play();
                }
            }

        }
    }

    //트레이스 인자를 전달받기위한 메소드
    public void TraceCheck(bool tf)
    {
        Trace=tf;
    }
}
