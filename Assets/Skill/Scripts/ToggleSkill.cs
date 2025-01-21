using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleSkill : MonoBehaviour
{

    public Transform OnTF, offTF;
    public TextMeshProUGUI count;
    UnityAction useSkill;
    public ToggleItem toggleItem;
 
    public void InitUseSkill(UnityAction a)
    {
      
      
           useSkill = a;
       
    }
    
    Func<bool> canOn;
    /// <summary>
    /// ��UIʱ����
    /// </summary>
    /// <param name="name">�洢����Ĭ�ϴ򿪹ر��õ�string</param>
    /// <param name="skillCount">����ӵ�е�����</param>
    public void OpenSet(string name,int skillCount) {
        if (PlayerPrefs.HasKey(name))
        {
           
            int a=  PlayerPrefs.GetInt(name);
            Debug.Log("a"+ a);
            if (a == 1&& (bool)canOn?.Invoke())
            {
                OnTF.gameObject.SetActive(true);
                offTF.gameObject.SetActive(false);
                this.count.text = skillCount.ToString();
                toggleItem.isOn = true;
            }
            else
            {
                OnTF.gameObject.SetActive(false);
                offTF.gameObject.SetActive(true);
                this.count.text = skillCount.ToString();
                toggleItem.isOn = false;
            }
        }
        else
        {
          var c=  canOn?.Invoke();
            if (c!=null)
            {
                if ((bool)c)
                {
                    PlayerPrefs.SetInt(name,1);
                }
                else
                {
                    PlayerPrefs.SetInt(name, 0);
                }
                PlayerPrefs.Save();
                int a = PlayerPrefs.GetInt(name);
                if (a == 1)
                {
                    OnTF.gameObject.SetActive(true);
                    offTF.gameObject.SetActive(false);
                    this.count.text = skillCount.ToString();
                    toggleItem.isOn = true;
                }
                else
                {
                    OnTF.gameObject.SetActive(false);
                    offTF.gameObject.SetActive(true);
                    this.count.text = skillCount.ToString();
                    toggleItem.isOn = false;
                }
            }
           
        } 
       
    
    }
  
    /// <summary>
    /// /
    /// </summary>
    /// <param name="onFunc">������</param>
    /// <param name="offFunc">������</param>
    /// <param name="trueOnAction">���ɹ�����</param>
    /// <param name="falseOnAction">��ʧ�ܴ���</param>
    /// <param name="trueOffAction">�سɹ�����</param>
    /// <param name="falseOffAction">��ʧ�ܴ���</param>
    public void InitToggle(Func<bool> onFunc, Func<bool> offFunc, UnityAction trueOnAction, UnityAction falseOnAction, UnityAction trueOffAction, UnityAction falseOffAction) {
        if (toggleItem != null)
        {
            canOn = onFunc;
            toggleItem.ToggleInit(onFunc, offFunc, trueOnAction, falseOnAction, trueOffAction, falseOffAction);
        }

        }
    public void UseSkill() {
        if (toggleItem.isOn)
        {
            useSkill?.Invoke();
        }
      


    }
   
}
