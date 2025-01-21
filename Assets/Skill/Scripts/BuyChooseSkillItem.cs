using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuyChooseSkillItem : MonoBehaviour
{
    public Button buy,close;
    public Transform scaleTF;
    public void Open() {
        gameObject.SetActive(true);
        scaleTF.localScale = new Vector3(0.5f,0.5f,0.5f);
        scaleTF.DOScale(new Vector3(1,1,1),0.2f);
    
    }
    public void Close()
    {
        gameObject.SetActive(false);


    }
    public void BuyChooseSkillItemInit(UnityAction buyAction)
    {
        close.onClick.AddListener(Close);
        AddUIAudio();
        buy.onClick.AddListener(buyAction);
    }

    private void AddUIAudio()
    {
        Button[] buttons = transform.GetComponentsInChildren<Button>(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() =>
            {
                //code µã»÷UIµÄÒôÐ§
               // example GameManager.Instance.soundManager.PlaySound(16);

            });
        }
    }
}
