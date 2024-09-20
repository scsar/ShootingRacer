using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Player;
    public float SpawnTimeMax=4f;
    public float SpawnTimeMin=1.5f;
    float SpawnTime;
    float time;
    public GameObject EnemyPreFab;
    public List<GameObject> EnemyObjectPool;
    int poolSize= 10;

    public float CircleRadius = 4f;

    Vector3 MobPosition;


    void Start()
    {
        time=0f;
        SpawnTime=0f;
        for(int i=0;i<poolSize;i++)
        {
            GameObject enemy=Instantiate(EnemyPreFab);
            EnemyObjectPool.Add(enemy);
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!GameManager.GM.GameOver)
        {
            time+=Time.deltaTime;
            SpawnTime=Random.Range(SpawnTimeMin,SpawnTimeMax);

            //스폰시간이 경과하면
            if(time>=SpawnTime)
            {
                Debug.Log("생성 실행");
                time=0f;
                SpawnTime=0f;

                // 랜덤한 각도 생성
                float angle = Random.Range(0f, 360f);
                // 각도를 라디안으로 변환
                float radian = angle * Mathf.Deg2Rad;

                // X, Y, Z 좌표 계산
                float xPos = Player.transform.position.x + 30f; // X 좌표를 고정 값(30)으로 설정
                float yPos = Mathf.Sin(radian) * CircleRadius; // 원의 경계에서 랜덤한 Y 좌표
                float zPos = Mathf.Cos(radian) * CircleRadius; // 원의 경계에서 랜덤한 Z 좌표

                MobPosition = new Vector3(xPos, Player.transform.position.y + yPos, Player.transform.position.z + zPos);

                // 적의 위치와 플레이어와의 거리 계산
                float distance = Vector3.Distance(Player.transform.position, MobPosition);

                // 만약 거리가 원의 반지름과 거리(30) 합이 원의 반지름보다 작다면 적을 생성
                if (distance <= CircleRadius + 30 && EnemyObjectPool.Count>0)
                {
                    GameObject enemy=EnemyObjectPool[0];
                    EnemyObjectPool.Remove(enemy);
                    enemy.transform.position=MobPosition;
                    enemy.SetActive(true);
                }
            }
        }

    }
}
