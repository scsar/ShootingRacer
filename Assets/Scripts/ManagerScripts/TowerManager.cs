using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<GameObject> TowerObjects;
    public GameObject ShieldImage;
    GameObject Player;

    public GameObject FirePosition;
    public GameObject FirePosition2;


    bool shield=false;
    int currentDamage;
    float powerUpTime;
    float traceTime;


    bool TraceActived;
    bool isReplace;
    public BulletGenerator manager;
    public bool replace
    {
        get
        {
            return isReplace;
        }
        set
        {
            //źȯ�� Tower2���� ���������� �й��Ͽ� ����ִ´�.
            if(!GameManager.GM.GameOver)
            {
                isReplace=value;
                if(isReplace)
                {
                    manager=FirePosition.GetComponent<BulletGenerator>();
                }
                else
                {
                    manager=FirePosition2.GetComponent<BulletGenerator>();
                }
            }

        }
    }

    public int Damage
    {
        get
        {
            return currentDamage;
        }
        set
        {
            currentDamage=value;
        }
    }
    public bool ObstacleShield
    {
        get
        {
            return shield;
        }
        set
        {
            shield=value;
            ShieldImage.SetActive(shield);
        }
    }
    public bool TraceActive
    {
        get
        {
            return TraceActived;
        }
        set
        {
            TraceActived=value;
            foreach (var obj in TowerObjects)
            {
            obj.GetComponentInChildren<BulletGenerator>().TraceCheck(value);
            }
        }
    }
  

    public static TowerManager Tower=null;

    void Awake()
    {
        if(Tower==null)
        {
            Tower=this;
        }
        Player=GameObject.Find("Player");
    }

    void Start()
    {

        Damage=1;
        ObstacleShield=false;
    }

    void FixedUpdate()
    {
        //Powerup�����۰� Trace�������� �ð��� üũ�ϱ����� �޼ҵ�
        UpdateItemTimers();
    }

    void UpdateItemTimers()
    {
        //PowerUp������
        if (IsPowerUpActive())
        {
            PowerUpTimer();
        }

        //Trace������
        if (IsTraceActive())
        {
            TraceTimer();
        }
    }

    bool IsPowerUpActive()
    {
        return powerUpTime > 0;
    }

    //PowerUp������ ���ӱⰣ���� Ÿ�̸Ӱ� �����Ѵ�
    void PowerUpTimer()
    {
        powerUpTime -= Time.deltaTime;

        if (powerUpTime <= 0)
        {
            //�ð� ������ EndPowerUp�޼ҵ� ȣ��
            EndPowerUp();
        }
    }

    void EndPowerUp()
    {
        Debug.Log("�Ŀ��� ����");
        Damage = 1;
    }

    bool IsTraceActive()
    {
        return traceTime > 0;
    }

    void TraceTimer()
    {
        traceTime -= Time.deltaTime;

        if (traceTime <= 0)
        {
            EndTrace();
        }
    }

    void EndTrace()
    {
        Debug.Log("Ʈ���̽� ����");
        TraceActive = false;
    }

    //PowerUp�������� �Ծ����������ϸ�, PowerUpŸ�̸Ӹ� �����Ͽ� ItemTimer�� ���������� �ߵ��ɼ��ְ��Ѵ�.
    public void PowerUp()      //��ź�� ������ ��ȭ�� ��ž���� ��ȭ
    {   
        Debug.Log("Power up ����");
        powerUpTime=15f;
        traceTime=0;
        TraceActive=false;
        Tower.Damage=2;
    }

    //PowerUp�޼ҵ�� ����
    public void TraceBullet()  //��Ż�� �����ϴ� źȯ�� ���� ��ž���� ��ȭ
    {
        Debug.Log("Trace ����");
        powerUpTime=0;
        traceTime=10f;
        Tower.Damage=1;
        TraceActive=true;

    }
    public void ShieldGauage() //enemy�� �ε������� ���ҵǴ� HP�� ä���ִ� ������
    {
        Player.GetComponent<PlayerController>().IncreaseShield();
    }

}
