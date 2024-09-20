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
            //탄환을 Tower2개에 순차적으로 분배하여 집어넣는다.
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
        //Powerup아이템과 Trace아이템의 시간을 체크하기위한 메소드
        UpdateItemTimers();
    }

    void UpdateItemTimers()
    {
        //PowerUp아이템
        if (IsPowerUpActive())
        {
            PowerUpTimer();
        }

        //Trace아이템
        if (IsTraceActive())
        {
            TraceTimer();
        }
    }

    bool IsPowerUpActive()
    {
        return powerUpTime > 0;
    }

    //PowerUp아이템 지속기간동안 타이머가 감소한다
    void PowerUpTimer()
    {
        powerUpTime -= Time.deltaTime;

        if (powerUpTime <= 0)
        {
            //시간 종료후 EndPowerUp메소드 호출
            EndPowerUp();
        }
    }

    void EndPowerUp()
    {
        Debug.Log("파워업 종료");
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
        Debug.Log("트레이스 종료");
        TraceActive = false;
    }

    //PowerUp아이템을 먹었을때실행하며, PowerUp타이머를 설정하여 ItemTimer가 연쇄적으로 발동될수있게한다.
    public void PowerUp()      //포탄의 위력이 강화된 포탑으로 강화
    {   
        Debug.Log("Power up 실행");
        powerUpTime=15f;
        traceTime=0;
        TraceActive=false;
        Tower.Damage=2;
    }

    //PowerUp메소드와 동일
    public void TraceBullet()  //포탈을 추적하는 탄환을 가진 포탑으로 강화
    {
        Debug.Log("Trace 실행");
        powerUpTime=0;
        traceTime=10f;
        Tower.Damage=1;
        TraceActive=true;

    }
    public void ShieldGauage() //enemy와 부딪쳤을떄 감소되는 HP를 채워주는 아이템
    {
        Player.GetComponent<PlayerController>().IncreaseShield();
    }

}
