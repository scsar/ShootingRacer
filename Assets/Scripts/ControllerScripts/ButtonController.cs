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
        //버튼을 눌렀을때, 해당 버튼의 오브젝트 명을 가져와 반환한다.
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        scene.Button=buttonName;
    
    }
}
