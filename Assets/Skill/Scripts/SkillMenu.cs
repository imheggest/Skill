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
    [Tooltip("随机技能抽奖Transform")]
    public Transform randomSkillTF;
    [Tooltip("获得技能的序号")]
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
    [Tooltip("会消耗的技能")]
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
    [Tooltip("随机技能的栏位")]
    public ToggleSkill toggleRandomSkill;
    [Tooltip("技能介绍")]
    public SkillInfoItem[] skillInfoItems;
    [Tooltip("配置技能")]
    [SerializeField] public RandomSkill[] randomSkills;
    
    public GameObject skillIconPrefab;
    public Transform skillIconsTF;
    //几组奖品图片
    public int iconGroups;
    //抽奖图片的大小
    public Vector2 iconSize;
    //奖品间距
    public float space;
   
    //道具面板
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
            //选择技能
            int randomIndex = Random.Range(0, randomSkills.Length);

            randomSkillIndex = randomIndex;
            Debug.Log("技能：" + randomIndex);
            Debug.Log("组：" + randomGroup);

            //转到转盘
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
                   Debug.Log("点击开启");
                   SetSkillInfo(3,randomSkills[randomSkillIndex].skillInfo );
                   return true;
               },
               () =>
               {
                   Debug.Log("点击关闭");
                   SetSkillInfo(3, randomSkills[randomSkillIndex].skillInfo);
                   return true;
               },
               () =>
               {
                   Debug.Log("技能1关=>开成功");
                   //toggleRandomSkill.OnTF.gameObject.SetActive(true);
                  // toggleRandomSkill.offTF.gameObject.SetActive(false);

                  

               },
               () =>
               {
                   Debug.Log("技能1关=>开失败");
                  

               },
               () =>
               {
                   Debug.Log("技能1开=>关成功");
                   //toggleRandomSkill.OnTF.gameObject.SetActive(false);
                  // toggleRandomSkill.offTF.gameObject.SetActive(true);

                  
               },
               () =>
               {
                   Debug.Log("技能1开=>关失败");
               }
               );


    }

  
    /// <summary>
    /// 特定条件将技能解锁
    /// </summary>
    private void SetSkillLock() {
        skills[0].isUnlock = null;
        skills[0].isUnlock = () => { return true; };
        skills[1].isUnlock = null;
        skills[1].isUnlock = () => { return true; };
        skills[2].isUnlock = null;
        skills[2].isUnlock = () => { return true; };
    }
    //购买技能
    private void SetSkillBuy()
    {
        skills[0].buyChooseSkillItem.BuyChooseSkillItemInit(()=> {
            
            Debug.Log("购买技能1");
            if (true)
            {
                //code 修改技能的数量
               
                skills[0].buyChooseSkillItem.Close();
            }

            SkillOpen();
        });
        skills[1].buyChooseSkillItem.BuyChooseSkillItemInit(() => {

            Debug.Log("购买技能2");
            if (true)
            {
                //code 修改技能的数量
                skills[1].buyChooseSkillItem.Close();
            }
            SkillOpen();
        });
        skills[2].buyChooseSkillItem.BuyChooseSkillItemInit(() => {

            Debug.Log("购买技能3");
            if (true)
            {
                //code 修改技能的数量
                skills[2].buyChooseSkillItem.Close();
            }
            SkillOpen();
        });
    }

    /// <summary>
    /// 使用技能
    /// </summary>
    private void SetUseSkill()
    {
        skills[0].toggleSkill.InitUseSkill(() => {
            if (!(bool)skills[0].isUnlock?.Invoke())
            {
                return;
             
            }
            Debug.Log("使用技能1");
            //code 技能

            //example HuoJian();
            //code 修改技能的数量

        });
        skills[1].toggleSkill.InitUseSkill(() => {
            if (!(bool)skills[1].isUnlock?.Invoke())
            {
                return;

            }
         
            Debug.Log("使用技能2");
            //code 技能

            //example Tip(skills[1].toggleSkill.transform.position);

            //code 修改技能的数量
        });
        skills[2].toggleSkill.InitUseSkill(() => {
            if (!(bool)skills[2].isUnlock?.Invoke())
            {
                return;

            }

            Debug.Log("使用技能3");
            //code 技能

           //example  IncreaseTime(skills[1].toggleSkill.transform.position);

            //code 修改技能的数量

        });
    }
    #region Skill
   
   
    #endregion

    /// <summary>
    /// 使用抽奖的技能
    /// </summary>
    private void SetRandomSkill()
    {
        randomSkills[0].InitSkillAction(() => { 
            Debug.Log("使用抽奖技能1火箭");
            //code 技能
           
        });
        randomSkills[1].InitSkillAction(() => { 
            Debug.Log("使用抽奖技能2提示"); 
            //code 技能
        });
            randomSkills[2].InitSkillAction(() => {
                
                Debug.Log("使用抽奖技能3时间");
                //code 技能
        });
     
    }
    /// <summary>
    /// 设置技能UI toggle逻辑
    /// </summary>
    private void SetSkillToggle()
    {
        skills[0].toggleSkill.InitToggle(
            () =>
            {
                Debug.Log("尝试开启");
                SetSkillInfo(0, skills[0].skillInfo);
                //code 返回  技能可以开启的条件
                return true;
            },
            () =>
            {

                Debug.Log("尝试关闭");
                SetSkillInfo(0, skills[0].skillInfo);
                //code 返回 技能可关闭的条件
                return true;
            },
            () =>
            {
                Debug.Log("技能1关=>开成功");
                skills[0].toggleSkill.OnTF.gameObject.SetActive(true);
                skills[0].toggleSkill.offTF.gameObject.SetActive(false);

                SaveSkillIsOn(skills[0].skillName,1);

            },
            () =>
            {
                Debug.Log("技能1关=>开失败");
                if (skills[0].buyChooseSkillItem!=null)
                {
                    skills[0].buyChooseSkillItem.Open();
                }
               
            },
            () =>
            {
                Debug.Log("技能1开=>关成功");
                skills[0].toggleSkill.OnTF.gameObject.SetActive(false);
                skills[0].toggleSkill.offTF.gameObject.SetActive(true);
             
                SaveSkillIsOn(skills[0].skillName, 0);
            },
            () =>
            {
                Debug.Log("技能1开=>关失败");
            }
            );



       
        skills[1].toggleSkill.InitToggle(
            () =>
            {
                Debug.Log("尝试开启");
                SetSkillInfo(1, skills[1].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("尝试关闭");
                SetSkillInfo(1, skills[1].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("技能2关=>开成功");
                skills[1].toggleSkill.OnTF.gameObject.SetActive(true);
                skills[1].toggleSkill.offTF.gameObject.SetActive(false);
                SaveSkillIsOn(skills[1].skillName, 1);


            },
            () => {
                Debug.Log("技能2关=>开失败");
                if (skills[1].buyChooseSkillItem != null)
                {
                    skills[1].buyChooseSkillItem.Open();
                }
            },
            () =>
            {
                Debug.Log("技能2开=>关成功");
                skills[1].toggleSkill.OnTF.gameObject.SetActive(false);
                skills[1].toggleSkill.offTF.gameObject.SetActive(true);
             
                SaveSkillIsOn(skills[1].skillName, 0);
            },
            () => { Debug.Log("技能2开=>关失败"); }
            );
      
        skills[2].toggleSkill.InitToggle(
            () =>
            {
                Debug.Log("尝试开启");
                SetSkillInfo(2, skills[2].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("尝试关闭");
                SetSkillInfo(2, skills[2].skillInfo);
                return true;
            },
            () =>
            {
                Debug.Log("技能3关=>开成功");
                skills[2].toggleSkill.OnTF.gameObject.SetActive(true);
                skills[2].toggleSkill.offTF.gameObject.SetActive(false);
                SaveSkillIsOn(skills[2].skillName, 1);
            },
            () => { 
                Debug.Log("技能3关=>开失败");
                if (skills[2].buyChooseSkillItem != null)
                {
                    skills[2].buyChooseSkillItem.Open();
                }
            },
            () =>
            {
                Debug.Log("技能3开=>关成功");
                skills[2].toggleSkill.OnTF.gameObject.SetActive(false);
                skills[2].toggleSkill.offTF.gameObject.SetActive(true);
              
                SaveSkillIsOn(skills[2].skillName, 0);
            },
            () => { Debug.Log("技能3开=>关失败"); }
           );
    }

    /// <summary>
    /// 存储技能默认是否开启
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isOn"></param>
    private void SaveSkillIsOn(string name,int isOn)
    {
        PlayerPrefs.SetInt(name, isOn);
       
        PlayerPrefs.Save();
    }
    /// <summary>
    /// 抽奖
    /// </summary>
    /// <param name="randomGroup">第几组图片</param>
    /// <param name="randomIndex">第几个技能</param>
    private void RollPrize(int randomGroup, int randomIndex)
    {
        //定位转盘停止的位置
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
    /// 更换抽奖的图片顺序
    /// </summary>
    private void ChangeIcon()
    {
  
        for (int i = 0; i < skillPrizeIcons.Length; i++)
        {
            skillPrizeIcons[i].ChangeIcon();
           
        }
      
    }
    /// <summary>
    /// 设置技能打开界面时的Ison
    /// 
    /// </summary>
    private void SkillOpen()
    {
        //code 技能的数量
        //example   skills[0].toggleSkill.OpenSet(skills[0].skillName,tipCount);
        skills[0].toggleSkill.OpenSet(skills[0].skillName,1);
        skills[1].toggleSkill.OpenSet(skills[1].skillName, 1);
        skills[2].toggleSkill.OpenSet(skills[2].skillName,1);
    }
    /// <summary>
    /// SkillMenuEditor更新奖品
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
        if (GUILayout.Button("更新转盘"))
        {
        GameObject.Find("SkillMenu").GetComponent<SkillMenu>().   UpdataPrize();
        }
    }

}
