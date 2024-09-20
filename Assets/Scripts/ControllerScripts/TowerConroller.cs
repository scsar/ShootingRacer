using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerConroller : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //타워가 장애물과 충돌하였고 실드아이템을 보유하지않은 경우
        if(other.gameObject.CompareTag("Obstacle") && ! TowerManager.Tower.ObstacleShield )
        {
            GameManager.GM.GameOver=true;
            Destroy(transform.root.gameObject); 
        }
        //타워가 장애물과 충돌하였지만, 실드아이템을 보유하고있는 경우
        else if(other.gameObject.CompareTag("Obstacle") && TowerManager.Tower.ObstacleShield)
        {
            TowerManager.Tower.ObstacleShield=false;
        }
        //타워가 에너미와 충돌하였을경우는 따로 처리하지않음(EnemyController 스크립트에서 처리하기때문에 이중 처리할 필요가없음)
        else if(other.gameObject.CompareTag("Enemy")){}
        //이외의 경우, 아이템과 충돌하였을경우.
        else
        {
            //피격된 오브젝트 이름으로 각 오브젝트가 어떠한 오브젝트인지 구분
            string str=other.gameObject.name;
            if(str.Equals("PowerUp(Clone)"))
            {
                Debug.Log("파워업");
                //PowerUp();
                TowerManager.Tower.PowerUp();   
            }
            else if(str.Equals("TraceBullet(Clone)"))
            {
                Debug.Log("트레이스");
                //TraceBullet();
                TowerManager.Tower.TraceBullet();
            }
            else if(str.Equals("ShieldGauage(Clone)"))
            {
                Debug.Log("실드게이지");
                TowerManager.Tower.ShieldGauage();
            }
            else if(str.Equals("PlayerShield(Clone)"))
            {
                Debug.Log("플레이어실드");
                TowerManager.Tower.ObstacleShield=true;
            }
            //Bullet과 충돌하였을떄(예상치못한경우로 타워와 Bullet이 충돌할경우)
            if(other.gameObject.CompareTag("Bullet"))
            {
                //2개의 Tower에 순서대로 오브젝트 풀에 반환한다.
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
            }
            else
            {
                //앞선 모든경우와 비교하였을떄 해당되지않는경우 오브젝트를 파괴한다.
                Destroy(other.gameObject);
            }

        }
    }



}
