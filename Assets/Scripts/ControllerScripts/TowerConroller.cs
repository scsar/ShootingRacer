using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerConroller : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Ÿ���� ��ֹ��� �浹�Ͽ��� �ǵ�������� ������������ ���
        if(other.gameObject.CompareTag("Obstacle") && ! TowerManager.Tower.ObstacleShield )
        {
            GameManager.GM.GameOver=true;
            Destroy(transform.root.gameObject); 
        }
        //Ÿ���� ��ֹ��� �浹�Ͽ�����, �ǵ�������� �����ϰ��ִ� ���
        else if(other.gameObject.CompareTag("Obstacle") && TowerManager.Tower.ObstacleShield)
        {
            TowerManager.Tower.ObstacleShield=false;
        }
        //Ÿ���� ���ʹ̿� �浹�Ͽ������� ���� ó����������(EnemyController ��ũ��Ʈ���� ó���ϱ⶧���� ���� ó���� �ʿ䰡����)
        else if(other.gameObject.CompareTag("Enemy")){}
        //�̿��� ���, �����۰� �浹�Ͽ������.
        else
        {
            //�ǰݵ� ������Ʈ �̸����� �� ������Ʈ�� ��� ������Ʈ���� ����
            string str=other.gameObject.name;
            if(str.Equals("PowerUp(Clone)"))
            {
                Debug.Log("�Ŀ���");
                //PowerUp();
                TowerManager.Tower.PowerUp();   
            }
            else if(str.Equals("TraceBullet(Clone)"))
            {
                Debug.Log("Ʈ���̽�");
                //TraceBullet();
                TowerManager.Tower.TraceBullet();
            }
            else if(str.Equals("ShieldGauage(Clone)"))
            {
                Debug.Log("�ǵ������");
                TowerManager.Tower.ShieldGauage();
            }
            else if(str.Equals("PlayerShield(Clone)"))
            {
                Debug.Log("�÷��̾�ǵ�");
                TowerManager.Tower.ObstacleShield=true;
            }
            //Bullet�� �浹�Ͽ�����(����ġ���Ѱ��� Ÿ���� Bullet�� �浹�Ұ��)
            if(other.gameObject.CompareTag("Bullet"))
            {
                //2���� Tower�� ������� ������Ʈ Ǯ�� ��ȯ�Ѵ�.
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
                //�ռ� ������ ���Ͽ����� �ش�����ʴ°�� ������Ʈ�� �ı��Ѵ�.
                Destroy(other.gameObject);
            }

        }
    }



}
