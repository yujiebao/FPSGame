using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float shootRate = 10f;
    [SerializeField] private float shootRange = 20f;
    [SerializeField] private float shootForce  = 150f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private TMP_Text AmmoInfo;
    private int currentAmmo;   //当前弹夹剩余子弹数
    [SerializeField] private int maxAmmo;  //弹夹容量
    [SerializeField] private int magazineAmmo;  //总弹药
    [SerializeField] private float damageAmout = 10;
    // [SerializeField] private AudioSource audioSource;
    // [SerializeField] private AudioClip shootSound;
    // private Camera eye;
    private InputAction shootAction;
    private InputAction ReloadAction;
    private float nextTimeToShoot = 0f;
    [SerializeField]protected Animator animator;
    public bool isReloading = false;
    // public  int a = 1;
    // Start is calletd before the first frame update
    // private void OnEnable()
    // {
    //   //新输入系统绑定按钮
    //     shootAction = new InputAction("Shoot",binding:"<mouse>/leftButton");
    //     shootAction .AddBinding("<Gamepad>/x");
    //     shootAction.Enable(); 
        
    // }
    // private void Start() {
    //   animator = GetComponent<Animator>();
    //   eye = FindAnyObjectByType<Camera>();
    //     currentAmmo = maxAmmo;//初始化弹夹
    // }
    
    private void OnEnable() {
         isReloading = false;
          animator.SetBool("Reload", false);
    }

     protected void Start() {
        // animator = GetComponent<Animator>();
      //新输入系统绑定按钮
        shootAction = new InputAction("Shoot",binding:"<mouse>/leftButton");
        shootAction.AddBinding("<Gamepad>/x");
        shootAction.Enable(); 
        ReloadAction = new InputAction("Reload",binding:"<keyboard>/R");
        ReloadAction.Enable();
        // eye = FindAnyObjectByType<Camera>();
        currentAmmo = maxAmmo;//初始化弹夹  
    }
    // Update is called once per frame
    protected void Update()
    {
        if(ReloadAction.triggered)
        {
          StartCoroutine(Reload());
        }
        AmmoInfo.text = currentAmmo+"/"+magazineAmmo;
        animator.SetBool("Shoot",shootAction.IsPressed());    //此语句必须放在if外面，if（69行）里面shootAction.IsPressed()恒为true  此类语句注意逻辑。
                                                              //且应该放在Reload代码前，避免修改Reload时的射击操作  或者可以将设置射击操作函数放在弹夹为零的else，避免换弹时射击动作
      ///<summary>
      /// 弹夹容量为0时，弹夹容量减去弹夹容量
      /// </summary> 
         if(currentAmmo == 0 )
         {
          animator.SetBool("Shoot",false);
          if(magazineAmmo == 0) 
           {
              return;
           }
           else if(!isReloading)    //相当于加了一把互斥锁
           {
            //  Reload();
            StartCoroutine(Reload());
           }
         }

        ///<summary>  
        ///方法1使用鼠标的float值判断是否按下注意浮点数精度误差使用Mathf.Approximately来比较浮点数
        /// </summary> 
        // bool isShooting = Mathf.Approximately(shootAction.ReadValue<float>(),1f); //----------1
        // animator.SetBool("Shoot",isShooting);     //--------1
        // if(isShooting&&Time.time > nextTimeToShoot)  ---------1   
      if(shootAction.IsPressed()&&Time.time > nextTimeToShoot && !isReloading)
      {
      // animator.SetBool("Shoot",shootAction.IsPressed());  
        // audioSource.PlayOneShot(shootSound);\
         nextTimeToShoot = Time.time + 1f/shootRate;  //shootRate 用于计算时间间隔
            Shoot();
      }  
    } 
    /// <summary>
    /// Reload 只能在idle和run状态使用，Shoot函数运行在Shoot状态
    /// </summary>
    private IEnumerator Reload()
    {
        isReloading = true;
        AudioManager.instance.Play("Reload");  //播放声音
        animator.SetBool("Reload",true); 
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Reload",false);
        // currentAmmo = maxAmmo;
        // magazineAmmo -= maxAmmo;
        if(magazineAmmo >= maxAmmo)
        {
          magazineAmmo = magazineAmmo-maxAmmo+currentAmmo;   //减数是缺少的量，即弹夹量-剩余量
          currentAmmo = maxAmmo;   //设置isReloading之前更改currentAmmo避免一直进行换弹操作
        }
        else
        {
          currentAmmo = magazineAmmo;  //同理更改currentAmmo的值
          magazineAmmo = 0;
        }
        isReloading = false; 
    }
    private void Shoot()
    {
         AudioManager.instance.Play("Shoot");
         muzzleFlash.Play();
         currentAmmo--;
         RaycastHit hitInfo;
         LayerMask playerLayer = LayerMask.GetMask("Player");
        //  if(Physics.Raycast(eye.transform.position,eye.transform.forward,out hitInfo,shootRange))
         if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hitInfo,shootRange,~playerLayer))
          {
            if(hitInfo.rigidbody)
            {
                hitInfo.rigidbody.AddForce(Muzzle.transform.forward * shootForce);  //施加力先获取刚体
            }

            //调用TakeDamage方法
            if(hitInfo.transform.GetComponent<Enemy>())   //击中的物体是否包含Enemy脚本
            {
              hitInfo.transform.GetComponent<Enemy>().TakeDamage(damageAmout);
              return;
            }
            Instantiate(impactEffect,hitInfo.point,Quaternion.LookRotation(hitInfo.normal),hitInfo.transform);   //平面垂直于法线
            // Instantiate(impactEffect,hitInfo.point,Quaternion.LookRotation(hitInfo.normal),hitInfo.transform); 
            // // 使用三个参数的 Instantiate 方法实例化对象
            //  GameObject Hole = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            // // 将实例化对象设置为 hitInfo.transform 的子对象
            // Hole.transform.SetParent(hitInfo.transform);   或者 Hole.transform.parent = hitInfo.transform;
          }     
    }
}
