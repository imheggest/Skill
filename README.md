# Skill
GameSkillMenu
在游戏开始时选择技能，通过激励广告随机获得免费的技能

![QQ_1738811347600](https://github.com/user-attachments/assets/d6e8c158-9c41-4391-8d44-0deb4ce7d420)
玩家使用后会消耗数量的技能

![QQ_1738811773402](https://github.com/user-attachments/assets/6503604a-22db-44ff-b328-d9a81059bd92)
包括技能的名字，介绍，技能的单选框组件，技能的购买组件
![QQ_1738811877539](https://github.com/user-attachments/assets/469d9d07-3bf4-41ec-af06-c98771685e2e)
技能的详细介绍组件
![QQ_1738812017164](https://github.com/user-attachments/assets/d2a70be9-713f-452f-9d93-60451d30bc53)
激励广告随机获得免费的技能
![QQ_1738812093215](https://github.com/user-attachments/assets/2976d4e3-334d-4774-8d9f-89b596610f2b)
配置随机获得免费的技能，配置完成后点击更新转盘按钮更新UI
![QQ_1738812147893](https://github.com/user-attachments/assets/12ad89a7-8675-4854-b427-88fbc4054bea)


编写你自己的逻辑代码

条件true表示会显示可消耗技能在UI界面给玩家选择，一般用于特定关卡的技能解锁，如：当关卡大于20关解锁第二个技能
![QQ_1738812702480](https://github.com/user-attachments/assets/7ef7e73e-5439-4188-bcb6-568fff8a0b88)

当消耗技能的数量为0时，点击选择技能会弹出购买技能窗口，点击购买按钮会调用以下的代码
![QQ_1738812910335](https://github.com/user-attachments/assets/cb00cfaf-23d9-4616-82b6-58939f2c34a5)

当技能被选择，玩家开始游戏时会调用以下的代码

![QQ_1738813129767](https://github.com/user-attachments/assets/f002ccec-e6da-4e84-9eae-48f22b55ed86)

观看激励广告后，会抽取一个技能，调用被抽取技能的代码
![QQ_1738813201866](https://github.com/user-attachments/assets/2c0ee847-a26e-4563-ac9a-fa622ffb7a17)


toggle开启，关闭时会调用的代码，成功/失败选择技能，成功/失败取消技能
example：


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

![QQ_1738813346240](https://github.com/user-attachments/assets/49e6a7b9-87e9-4f10-acf8-6f6ac5b002bb)




