using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
     SceneUI scene;
    void Start()
    {
        scene=SceneUI.Instance;
    }

    public void OnButtonClick()
    {
        //��ư�� ��������, �ش� ��ư�� ������Ʈ ���� ������ ��ȯ�Ѵ�.
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        scene.Button=buttonName;
    
    }
}
