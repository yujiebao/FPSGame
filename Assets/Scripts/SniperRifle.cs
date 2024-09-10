using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SniperRifle : Gun
{
   private InputAction scopeAction;
   [SerializeField] private Image scopeOverlayImage;
   private bool isScope = false;
   protected bool scopeReloading = true;
   private void Start() 
   {
      base.Start();
      scopeAction = new InputAction("Scope", binding:"<Mouse>/rightButton");
      scopeAction.Enable();
   }
   private void Update() 
   {
      base.Update();
      if(scopeAction.triggered)
       {
         isScope = !isScope;
         StartCoroutine(OnScope(isScope));
       }
       if(isReloading)
       {
         StartCoroutine(OnScope(false));
       }
   }
   IEnumerator OnScope(bool isScope)
   {   
     animator.SetBool("Scope", isScope);
     if(isScope)
     {
        yield return new WaitForSeconds(0.5f);
        Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << 7);  //位运算 图层0不显示1显示
        Camera.main.fieldOfView = 45;
      //   print(Convert.ToString(1 << 7,2));   //输出二进制
     }
     else
     {
     Camera.main.cullingMask = Camera.main.cullingMask | (1 << 7);   //位运算
     Camera.main.fieldOfView = 60;
     } 
     scopeOverlayImage.gameObject.SetActive(isScope);
   }
   
}
