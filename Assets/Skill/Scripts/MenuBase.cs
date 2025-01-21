using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����UI�˵��Ļ���
/// </summary>
public class MenuBase : MonoBehaviour
{

    /// <summary>
    /// ��ʼ������text�������������
    /// </summary>
    public virtual void InitItem() { }
    /// <summary>
    /// ��UI
    /// </summary>
    public virtual void OpenMenu()
    {

        this.transform.gameObject.SetActive(true);
    }
    /// <summary>
    /// ����
    /// </summary>
    public virtual void UpdataMenu()
    {

    }
    /// <summary>
    /// �ر�UI
    /// </summary>
    public virtual void CloseMenu()
    {
        this.transform.gameObject.SetActive(false);

    }
    
}
