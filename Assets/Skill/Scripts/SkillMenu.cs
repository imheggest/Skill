using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillMenu : MenuBase
{
    public Button playButton,free;
    [Tooltip("������ܳ齱Transform")]
    public Transform randomSkillTF;
    [Tooltip("��ü��ܵ����")]
    [SerializeField] private int randomSkillIndex;
    [Serializable]
    public class Skill {
        public string skillName;
        public string skillInfo;
        public ToggleSkill toggleSkill;
        public BuyChooseSkillItem buyChooseSkillItem;
        public Func<bool> isUnlock;

    }
    [SerializeField]
    [Tooltip("�����ĵļ���")]
    public Skill[] skills;
    [Serializable]public class RandomSkill
    {
        public string skillName;
        public string skillInfo;
        public UnityAction skillAction;
        public Sprite icon;
        public void InitSkillAction(UnityAction action)
        {

            skillAction = action;
        }
    }
    [Tooltip("������ܵ���λ")]
    public ToggleSkill toggleRandomSkill;
    [Tooltip("���ܽ���")]
    public SkillInfoItem[] skillInfoItems;
    [Tooltip("���ü���")]
    [SerializeField] public RandomSkill[] randomSkills;
    
    public GameObject skillIconPrefab;
    public Transform skillIconsTF;
    //���齱ƷͼƬ
    public int iconGroups;
    //�齱ͼƬ�Ĵ�С
    public Vector2 iconSize;
    //��Ʒ���
    public float space;
   
    //�������
    [HideInInspector]
    public  SkillPrizeIcon[] skillPrizeIcons;
    #region Menu
    public override void InitItem()
    {
        base.InitItem();
        skillPrizeIcons = skillIconsTF.GetComponentsInChildren<SkillPrizeIcon>();
        ChangeIcon();


        SetUseSkill();
        //InitOffToOn
        SetSkillToggle();
        SetRandomSkillToggle();
        //
        SetSkillBuy();
        SetSkillLock();
        playButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < skills.Length; i++)
            {

                skills[i].toggleSkill.UseSkill();
            }
            if (randomSkillIndex != -1)
            {
                randomSkills[randomSkillIndex].skillAction?.Invoke();
            }

            CloseMenu();
        });
        SetRandomSkill();
        free.onClick.AddListener(() =>
        {
            randomSkillTF.transform.gameObject.SetActive(true);

            int randomGroup = Random.Range(1, iconGroups);
            //ѡ����
            int randomIndex = Random.Range(0, randomSkills.Length);

            randomSkillIndex = randomIndex;
            Debug.Log("���ܣ�" + randomIndex);
            Debug.Log("�飺" + randomGroup);

            //ת��ת��
            RollPrize(randomGroup, randomIndex);
            free.transform.gameObject.SetActive(false);
        });
    }
    public override void OpenMenu()
    {
        base.OpenMenu();
        randomSkillTF.transform.gameObject.SetActive(false);
        skillIconsTF.DOLocalMoveY(0, 0.01f);

        free.transform.gameObject.SetActive(true);

        ChangeIcon();
        SkillOpen();
        for (int i = 0; i < skills.Length; i++)
        {
            bool locked = (bool)skills[i].isUnlock?.Invoke();
            skills[i].toggleSkill.gameObject.SetActive(locked);

        }
        randomSkillIndex = -1;


    }
    public override void CloseMenu()
    {
        base.CloseMenu();
        randomSkillIndex = -1;

    }
    #endregion

    int openSkillInfo=-1;
    public void SetSkillInfo(int index,string content) {
        if (index== openSkillInfo)
        {
            return;
        }
        openSkillInfo = index;
        for (int i = 0; i < skillInfoItems.Length; i++)
        {
            if (i== openSkillInfo)
            {
                skillInfoItems[i].gameObject.SetActive(true);
            }
            else
            {
                skillInfoItems[i].gameObject.SetActive(false);
            }
           
        }
       
        skillInfoItems[openSkillInfo].content.text = content;
    }
    private void SetRandomSkillToggle()
    {
        toggleRandomSkill.InitToggle(
               () =>
               {
                   Debug.Log("�������");
                   SetSkillInfo(3,randomSkills[randomSkillIndex].skillInfo );
                   return true;
               },
               () =>
               {
                   Debug.Log("����ر�");
                   SetSkillInfo(3, randomSkills[randomSkillIndex].skillInfo);
                   return true;
               },
               () =>
               {
                   Debug.Log("����1��=>���ɹ�");
                   //toggleRandomSkill.OnTF.gameObject.SetActive(true);
                  // toggleRandomSkill.offTF.gameObject.SetActive(false);

                  

               },
               () =>
               {
                   Debug.Log("����1��=>��ʧ��");
                  

               },
               () =>
               {
                   Debug.Log("����1��=>�سɹ�");
                   //toggleRandomSkill.OnTF.gameObject.SetActive(false);
                  // toggleRandomSkill.offTF.gameObject.SetActive(true);

                  
               },
               () =>
               {
                   Debug.Log("����1��=>��ʧ��");
               }
               );


    }

  
    /// <summary>
    /// �ض����������ܽ���
    /// </summary>
    private void SetSkillLock() {
        skills[0].isUnlock = null;
        skills[0].isUnlock = () => { return true; };
        skills[1].isUnlock = null;
        skills[1].isUnlock = () => { return true; };
        skills[2].isUnlock = null;
        skills[2].isUnlock = () => { return true; };
    }
    //������
    private void SetSkillBuy()
    {
        skills[0].buyChooseSkillItem.BuyChooseSkillItemInit(()=> {
            
            Debug.Log("������1");
            if (true)
            {
                //code �޸ļ��ܵ�����
               
                skills[0].buyChooseSkillItem.Close();
            }

            SkillOpen();
        });
        skills[1].buyChooseSkillItem.BuyChooseSkillItemInit(() => {

            Debug.Log("������2");
            if (true)
            {
                //code �޸ļ��ܵ�����
                skills[1].buyChooseSkillItem.Close();
            }
            SkillOpen();
        });
        skills[2].buyChooseSkillItem.BuyChooseSkillItemInit(() => {

            Debug.Log("������3");
            if (true)
            {
                //code �޸ļ��ܵ�����
                skills[2].buyChooseSkillItem.Close();
            }
            SkillOpen();
        });
    }

    /// <summary>
    /// ʹ�ü���
    /// </summary>
    private void SetUseSkill()
    {
        skills[0].toggleSkill.InitUseSkill(() => {
            if (!(bool)skills[0].isUnlock?.Invoke())
            {
                return;
             
            }
            Debug.Log("ʹ�ü���1");
            //code ����

            //example HuoJian();
            //code �޸ļ��ܵ�����

        });
        skills[1].toggleSkill.InitUseSkill(() => {
            if (!(bool)skills[1].isUnlock?.Invoke())
            {
                return;

            }
         
            Debug.Log("ʹ�ü���2");
            //code ����

            //example Tip(skills[1].toggleSkill.transform.position);

            //code �޸ļ��ܵ�����
        });
        skills[2].toggleSkill.InitUseSkill(() => {
            if (!(bool)skills[2].isUnlock?.Invoke())
            {
                return;

            }

            Debug.Log("ʹ�ü���3");
            //code ����

           //example  IncreaseTime(skills[1].toggleSkill.transform.position);

            //code �޸ļ��ܵ�����

        });
    }
    #region Skill
   
   
    #endregion

    /// <summary>
    /// ʹ�ó齱�ļ���
    /// </summary>
    private void SetRandomSkill()
    {
        randomSkills[0].InitSkillAction(() => { 
            Debug.Log("ʹ�ó齱����1���");
            //code ����
           
        });
        randomSkills[1].InitSkillAction(() => { 
            Debug.Log("ʹ�ó齱����2��ʾ"); 
            //code ����
        });
            randomSkills[2].InitSkillAction(() => {
                
                Debug.Log("ʹ�ó齱����3ʱ��");
                //code ����
        });
     
    }
    /// <summary>
    /// ���ü���UI toggle�߼�
    /// </summary>
    private void SetSkillToggle()
    {
        skills[0].toggleSkill.InitToggle(
            () =>
            {
                Debug.Log("���Կ���");
                SetSkillInfo(0, skills[0].skillInfo);
                //code ����  ���ܿ��Կ���������
                return true;
            },
            () =>
            {

                Debug.Log("���Թر�");
                SetSkillInfo(0, skills[0].skillInfo);
                //code ���� ���ܿɹرյ�����
                return true;
            },
            () =>
            {
                Debug.Log("����1��=>���ɹ�");
                skills[0].toggleSkill.OnTF.gameObject.SetActive(true);
                skills[0].toggleSkill.offTF.gameObject.SetActive(false);

                SaveSkillIsOn(skills[0].skillName,1);

            },
            () =>
            {
                Debug.Log("����1��=>��ʧ��");
                if (skills[0].buyChooseSkillItem!=null)
                {
                    skills[0].buyChooseSkillItem.Open();
                }
               
            },
            () =>
            {
                Debug.Log("����1��=>�سɹ�");
                skills[0].toggleSkill.OnTF.gameObject.SetActive(false);
                skills[0].toggleSkill.offTF.gameObject.SetActive(true);
             
                SaveSkillIsOn(skills[0].skillName, 0);
            },
            () =>
            {
                Debug.Log("����1��=>��ʧ��");
            }
            );



       
        skills[1].toggleSkill.InitToggle(
            () =>
            {
                Debug.Log("���Կ���");
                SetSkillInfo(1, skills[1].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("���Թر�");
                SetSkillInfo(1, skills[1].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("����2��=>���ɹ�");
                skills[1].toggleSkill.OnTF.gameObject.SetActive(true);
                skills[1].toggleSkill.offTF.gameObject.SetActive(false);
                SaveSkillIsOn(skills[1].skillName, 1);


            },
            () => {
                Debug.Log("����2��=>��ʧ��");
                if (skills[1].buyChooseSkillItem != null)
                {
                    skills[1].buyChooseSkillItem.Open();
                }
            },
            () =>
            {
                Debug.Log("����2��=>�سɹ�");
                skills[1].toggleSkill.OnTF.gameObject.SetActive(false);
                skills[1].toggleSkill.offTF.gameObject.SetActive(true);
             
                SaveSkillIsOn(skills[1].skillName, 0);
            },
            () => { Debug.Log("����2��=>��ʧ��"); }
            );
      
        skills[2].toggleSkill.InitToggle(
            () =>
            {
                Debug.Log("���Կ���");
                SetSkillInfo(2, skills[2].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("���Թر�");
                SetSkillInfo(2, skills[2].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("����3��=>���ɹ�");
                skills[2].toggleSkill.OnTF.gameObject.SetActive(true);
                skills[2].toggleSkill.offTF.gameObject.SetActive(false);
                SaveSkillIsOn(skills[2].skillName, 1);
            },
            () => { 
                Debug.Log("����3��=>��ʧ��");
                if (skills[2].buyChooseSkillItem != null)
                {
                    skills[2].buyChooseSkillItem.Open();
                }
            },
            () =>
            {
                Debug.Log("����3��=>�سɹ�");
                skills[2].toggleSkill.OnTF.gameObject.SetActive(false);
                skills[2].toggleSkill.offTF.gameObject.SetActive(true);
              
                SaveSkillIsOn(skills[2].skillName, 0);
            },
            () => { Debug.Log("����3��=>��ʧ��"); }
           );
    }

    /// <summary>
    /// �洢����Ĭ���Ƿ���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isOn"></param>
    private void SaveSkillIsOn(string name,int isOn)
    {
        PlayerPrefs.SetInt(name, isOn);
       
        PlayerPrefs.Save();
    }
    /// <summary>
    /// �齱
    /// </summary>
    /// <param name="randomGroup">�ڼ���ͼƬ</param>
    /// <param name="randomIndex">�ڼ�������</param>
    private void RollPrize(int randomGroup, int randomIndex)
    {
        //��λת��ֹͣ��λ��
        int prizePos = 0;
        for (int i = 0; i < skillPrizeIcons[randomGroup].iconIndex.Count; i++)
        {
            if ((randomIndex) == skillPrizeIcons[randomGroup].iconIndex[i])
            {
                prizePos = i;
                Debug.Log(i + "index");
                break;
            }
        }



        skillIconsTF.DOLocalMoveY(randomGroup * randomSkills.Length * (iconSize.y + space) + prizePos * (iconSize.y + space), 2.1f);
    }
    /// <summary>
    /// �����齱��ͼƬ˳��
    /// </summary>
    private void ChangeIcon()
    {
  
        for (int i = 0; i < skillPrizeIcons.Length; i++)
        {
            skillPrizeIcons[i].ChangeIcon();
           
        }
      
    }
    /// <summary>
    /// ���ü��ܴ򿪽���ʱ��Ison
    /// 
    /// </summary>
    private void SkillOpen()
    {
        //code ���ܵ�����
        //example   skills[0].toggleSkill.OpenSet(skills[0].skillName,tipCount);
        skills[0].toggleSkill.OpenSet(skills[0].skillName,1);
        skills[1].toggleSkill.OpenSet(skills[1].skillName, 1);
        skills[2].toggleSkill.OpenSet(skills[2].skillName,1);
    }
    /// <summary>
    /// SkillMenuEditor���½�Ʒ
    /// </summary>
    public void UpdataPrize()
    {
        for (int i = skillIconsTF.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(skillIconsTF.GetChild(i).gameObject);
        }
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < randomSkills.Length; i++)
        {
            sprites.Add(randomSkills[i].icon);
        }
        for (int i = 0; i < iconGroups; i++)
        {

            SkillPrizeIcon skillPrizeIcon = Instantiate(skillIconPrefab, skillIconsTF).GetComponent<SkillPrizeIcon>();

            skillPrizeIcon.InitSkillPrize(sprites.ToArray(), space,iconSize);






        }
        Debug.Log(iconSize.y + space);
    }
}
[CustomEditor(typeof(SkillMenu))]
public class SkillMenuEditor : Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        if (GUILayout.Button("����ת��"))
        {
        GameObject.Find("SkillMenu").GetComponent<SkillMenu>().   UpdataPrize();
        }
    }

}
