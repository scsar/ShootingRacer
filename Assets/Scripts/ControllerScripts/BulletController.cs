using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 40f;
    float time;
    bool isActive;
    Transform target=null;
    ParticleSystem hitparticle;
    // Start is called before the first frame update

    void Awake()
    {
            hitparticle=GetComponentInChildren<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if (target != null)
            {
                // 트래킹 중인 경우
                RotateTowardsTarget();
            }
            else
            {
                MoveBullet();
            }

            time+=Time.deltaTime;
            if(time>=2)
            {
                time=0;
                if(TowerManager.Tower.replace)
                {
                    TowerManager.Tower.replace=false;
                }
                else
                {
                    TowerManager.Tower.replace=true;
                }
                TowerManager.Tower.manager.BulletObjectPool.Add(gameObject);
                gameObject.SetActive(false);
            }


        }
    }

    void MoveBullet()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void RotateTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        
        target = newTarget;
    }

    void OnDisable()
    {
        time=0;
        target=null;
        isActive=false;
    }

    void OnEnable()
    {
        isActive=true;
    }

    public void Particle()
    {
        if(hitparticle!=null)
        {
            hitparticle.Play();
        }
        else
        {
            Debug.LogError("삽입된 파티클시스템이존재하지않습니다.");
        }
        }

}
