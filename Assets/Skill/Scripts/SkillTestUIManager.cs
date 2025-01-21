using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTestUIManager : MonoBehaviour
{
    public SkillMenu skillMenu;
    private void Awake()
    {
        skillMenu.InitItem();
    }
    
}
