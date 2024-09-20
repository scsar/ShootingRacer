using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroller : MonoBehaviour
{
    public List<GameObject> TutorialText;
    int count = 0;

    void Awake()
    {
        SetActiveText();
    }

    void SetActiveText()
    {
        foreach (GameObject obj in TutorialText)
        {
            obj.SetActive(false);
        }
        TutorialText[count].SetActive(true);
    }
    public void OnButtonClick()
    {
        TutorialText[count].SetActive(false);

        count = (count + 1) % TutorialText.Count;

        TutorialText[count].SetActive(true);
    }
}
