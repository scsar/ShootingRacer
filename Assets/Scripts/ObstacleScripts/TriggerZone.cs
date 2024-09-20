using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public List<GameObject> obstacles;
    // Start is called before the first frame update
    void Start()
    {
        //obstacles 리스트에 존재하는 모든오브젝트를 비활성화.
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
        //트리거존과 플레이어가 충돌하면, 모든 오브젝트 활성화.
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(GameObject obj in obstacles)
            {
                obj.SetActive(true);
            }            
        }
    }
}
