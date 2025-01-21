using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillPrizeIcon : MonoBehaviour
{
    
    public List<RandomSkillToggle> randomSkillToggles = new List<RandomSkillToggle>();
    public GameObject prefab;
    public Sprite[] sprite;
    public VerticalLayoutGroup verticalLayoutGroup;
    public void InitSkillPrize(Sprite[] sprites,float space,Vector2 size) {
        sprite = sprites;
        verticalLayoutGroup.spacing = space;
        if (randomSkillToggles.Count == 0)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                var randomSkillToggle = Instantiate(prefab, this.transform).GetComponent<RandomSkillToggle>();
                Debug.Log(size);
                randomSkillToggle.GetComponent<RectTransform>().sizeDelta = size;
                randomSkillToggles.Add(randomSkillToggle);
               
            }

        }
       
       
    }
 public   List<int> iconIndex = new List<int>();
    public void ChangeIcon() {

     
        iconIndex.Clear();
        for (int i = 0; i < sprite.Length; i++)
        {
            iconIndex.Add(i);
        }
        int n = iconIndex.Count;
        for (int i = 0; i < n; i++)
        {
            int rand = UnityEngine.Random.Range(i, n );
            int a = iconIndex[i];
            iconIndex[i] = iconIndex[rand];
            iconIndex[rand] = a;

        }
        for (int i = 0; i < randomSkillToggles.Count; i++)
        {
            randomSkillToggles[i].backGround.sprite = sprite[iconIndex[i]];
        }
    }
}
