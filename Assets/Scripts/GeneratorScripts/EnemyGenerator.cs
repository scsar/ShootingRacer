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

            //�����ð��� ����ϸ�
            if(time>=SpawnTime)
            {
                Debug.Log("���� ����");
                time=0f;
                SpawnTime=0f;

                // ������ ���� ����
                float angle = Random.Range(0f, 360f);
                // ������ �������� ��ȯ
                float radian = angle * Mathf.Deg2Rad;

                // X, Y, Z ��ǥ ���
                float xPos = Player.transform.position.x + 30f; // X ��ǥ�� ���� ��(30)���� ����
                float yPos = Mathf.Sin(radian) * CircleRadius; // ���� ��迡�� ������ Y ��ǥ
                float zPos = Mathf.Cos(radian) * CircleRadius; // ���� ��迡�� ������ Z ��ǥ

                MobPosition = new Vector3(xPos, Player.transform.position.y + yPos, Player.transform.position.z + zPos);

                // ���� ��ġ�� �÷��̾���� �Ÿ� ���
                float distance = Vector3.Distance(Player.transform.position, MobPosition);

                // ���� �Ÿ��� ���� �������� �Ÿ�(30) ���� ���� ���������� �۴ٸ� ���� ����
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
