using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public List<GameObject> obstacles;
    // Start is called before the first frame update
    void Start()
    {
        //obstacles ����Ʈ�� �����ϴ� ��������Ʈ�� ��Ȱ��ȭ.
        foreach(GameObject obj in obstacles)
        {
            obj.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Ʈ�������� �÷��̾ �浹�ϸ�, ��� ������Ʈ Ȱ��ȭ.
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(GameObject obj in obstacles)
            {
                obj.SetActive(true);
            }            
        }
    }
}
