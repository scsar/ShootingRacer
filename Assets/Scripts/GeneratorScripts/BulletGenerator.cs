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
        //������ƮǮ ����.
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
        //�����ð��� ������ Bullet�� ����.
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
                //Ʈ���̽� ������ ȹ��� �ߵ��Ǵ� ���ǹ�
                if(Trace)
                {
                    enemyController = FindObjectOfType<EnemyController>();
                    //EnemyController �� �������ִ� enemy�� ã�´�.
                    if(enemyController != null)
                    {   
                        //�߰��ϸ�, �ش������Ʈ�� ��ġ�� SetTarget�޼ҵ�� �����Ѵ�.
                        bullet.GetComponent<BulletController>().SetTarget(enemyController.transform);
                    }
                    else
                    {
                        //���� enemy�� �������� �ʴ°��, null�� ��ȯ�Ѵ�.
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

    //Ʈ���̽� ���ڸ� ���޹ޱ����� �޼ҵ�
    public void TraceCheck(bool tf)
    {
        Trace=tf;
    }
}
