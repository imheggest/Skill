using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class ToggleItem : Toggle
{
 

    Func<bool> OnFunc, OffFunc;
    UnityAction TrueOnAction;
    UnityAction  FalseOnAction, TrueOffAction, FalseOffAction;
    public void ToggleInit(Func<bool> onFunc, Func<bool> offFunc, UnityAction trueOnAction, UnityAction falseOnAction, UnityAction trueOffAction, UnityAction falseOffAction) {
        OnFunc = null;
        OffFunc = null;
        TrueOnAction = null;
        FalseOnAction = null;
        TrueOffAction = null;
        FalseOffAction = null;
        this. OnFunc = onFunc;
        this.OffFunc = offFunc;
        this.TrueOnAction = trueOnAction;
       
        this.FalseOnAction=falseOnAction;
        this.TrueOffAction = trueOffAction;
     
        this.FalseOffAction = falseOffAction;
    }
   
  
    public override void OnSubmit(BaseEventData eventData)
    {
      
        if (isOn)
        {
            var off = OffFunc?.Invoke();
            if (off == null)
            {
                return;
            }
            if ((bool)off)
            {

                TrueOffAction?.Invoke();
                AnimalScale();
                base.OnSubmit(eventData);



            }
            else
            {
                FalseOffAction?.Invoke();
            }
           
            
        
        
        }
        else
        {
            var on = OnFunc?.Invoke();
            if (on == null)
            {
                return;
            }
            if ((bool)on)
            {
                
                 TrueOnAction?.Invoke();
                AnimalScale();
                base.OnSubmit(eventData);



            }
            else
            {
                FalseOnAction?.Invoke();
            }

        }
     
      
    }

    private void AnimalScale()
    {
        transform.DOKill();
        transform.localScale = new Vector3(1,1,1);
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.5f,1,1);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
       
        if (isOn)
        {
            var off = OffFunc?.Invoke();
            if (off == null)
            {
                return;
            }
            if ((bool)off)
            {

                TrueOffAction?.Invoke();
                AnimalScale();
                base.OnPointerClick(eventData);



            }
            else
            {
                FalseOffAction?.Invoke();
            }




        }
        else
        {
            var on = OnFunc?.Invoke();
            if (on == null)
            {
                return;
            }
            if ((bool)on)
            {
               TrueOnAction?.Invoke();


                AnimalScale();
                base.OnPointerClick(eventData);



            }
            else
            {
                FalseOnAction?.Invoke();
            }

        }
        //var a = OffToOn?.Invoke();
        //if (a == null)
        //{
        //    return;
        //}

        //if ((bool)a)
        //{
        //    if (isOn)
        //    {
        //        OnToOffTrueAction?.Invoke();
        //    }
        //    else
        //    {
        //        OffToOnTrueAction?.Invoke();
        //    }
        //    base.OnPointerClick(eventData);
        //}
        //else
        //{
        //    if (isOn)
        //    {
        //        OffToOnFalseAction?.Invoke();
        //    }
        //    else
        //    {
        //        OffToOnFalseAction?.Invoke();
        //    }

        //}
    }
}
