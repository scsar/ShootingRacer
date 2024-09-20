using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject particle;
    new AudioSource audio;
    Slider ShieldGauage;   //HP ������
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
        transform.Translate(speed*Time.deltaTime,0f,0f);    //�÷��̾� �̵��ڵ� �÷��̾�� �⺻������ �����θ� �̵��Ѵ�.

        if(ShieldGauage.value<=0.2f)  //��� �����ϋ�.
        {
            Instantiate(particle,transform.position,Quaternion.identity);
        }

        //���콺�� ������
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        //isRotating �� Ȱ��ȭ �Ǿ�����
        if (isRotating)
        {   
            //���콺 Ŭ��(��ġ) ��ġ�� ���� ȭ����̸� ������ ������ Ŭ���� ����, �������� ���������� ȸ���ϰ� �Ѵ�.
            float direction = Input.mousePosition.x > Screen.width / 2 ? -1.0f : 1.0f;

            // ���⿡ ���� ȸ��
            transform.Rotate( direction * rotationSpeed * Time.deltaTime,0,0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //��ֹ��� �浹�Ͽ���, �ǵ�������� �����ϰ��������� ���
        if(other.gameObject.CompareTag("Obstacle") && ! TowerManager.Tower.ObstacleShield )
        {
            GameManager.GM.GameOver=true;
            Destroy(transform.root.gameObject); 
        }
        //��ֹ��� �浹�Ͽ�����, �ǵ�������� �����ϰ��ִ°��
        else if(other.gameObject.CompareTag("Obstacle") && TowerManager.Tower.ObstacleShield)
        {
            TowerManager.Tower.ObstacleShield=false;
        }

        //���� �����Ͽ�����
        if(other.gameObject.CompareTag("Finish"))
        {
            GameManager.GM.Clear=true;
        }
    }


    //�ǵ� ���� �޼ҵ�
    public void DecreaseShield()
    {
        audio.Play();
        ShieldGauage.value-=0.1f;
        //�ǵ�������� ��� �����Ǿ����� ������Ʈ �ı�.
        if(ShieldGauage.value==0)
        {
            Destroy(gameObject);
        }
    }

    //�ǵ� ���� �޼ҵ�
    public void IncreaseShield()
    {
       ShieldGauage.value+=0.1f;
    }

}
