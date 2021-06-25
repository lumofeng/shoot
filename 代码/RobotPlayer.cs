using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPlayer : BaseRobot
{
    static RobotPlayer instance;
    private AudioSource source;
    public Rigidbody rg;
    //---动画组件应用
    private Animator animt;
     //---跳跃动画名,可以是一个或者多个[]
  // public string Jump;
    public static RobotPlayer GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    //
    public List<WeaponBase> Weapons;//武器背包
    public Transform Hand;		//挂接武器的地方
    public float WalkSpeed = 10;	//走路的速度
    //
    private WeaponBase CurWeapon; //当年手中的武器
    private int CurWeaponIdx = 0;
    //
    private Animator animator;
    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        //获取组件
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        CurWeapon = Weapons[CurWeaponIdx];
        //---查找动画组件赋值
        animt = this.GetComponent<Animator>();
        //寻找刚体组件
      /*  rg = this.GetComponent<Rigidbody>();
        rg.AddForce(Vector3.down);*/
    }

    // Update is called once per frame
    void Update()
    {
        var trans = Camera.main.transform;
        var forward = Vector3.ProjectOnPlane(trans.forward, Vector3.up);//向前的向量
        var right = Vector3.ProjectOnPlane(trans.right, Vector3.up);	//向右的向量
        //jump
        //---判断是否按下空格
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //---播放动画跳跃
            animt.SetBool("Jump", true);
        }
            //movement
            float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var moveDirection = v * forward + h * right;
        cc.Move(moveDirection.normalized * WalkSpeed * Time.deltaTime);	//对角色进行移动
        animator.SetFloat("Speed", cc.velocity.magnitude / WalkSpeed);	//过渡到跑步状态
        //rotate
        var r = GetAimPoint();	//获得目标点
        RotateToTarget(r);
        //shoot
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        //switch weapon
        float f = Input.GetAxis("Mouse ScrollWheel");
        if (f > 0) { NextWeapon(1); }
        else if (f < 0) { NextWeapon(-1); }
        //ui
        HUD.GetInstance().UpdateHpUI(hp);
        HUD.GetInstance().UpdateWeaponUI(CurWeapon.icon, CurWeapon.bulletNum);

    }
    //下一把武器
    public void NextWeapon(int step)
    {
        var idx = (CurWeaponIdx + step + Weapons.Count) % Weapons.Count;
        CurWeapon.gameObject.SetActive(false);
        CurWeapon = Weapons[idx];
        CurWeapon.gameObject.SetActive(true);
        CurWeaponIdx = idx;
    }
    //添加武器
    public void AddWeapon(GameObject weapon)
    {
        for (int i = 0;i<Weapons.Count;i++)
        {
            if (Weapons[i].gameObject.name == weapon.name)
            {
                Weapons[i].bulletNum += 15;
                return;
            }
        }
        var new_weapon = GameObject.Instantiate(weapon, Hand);
        new_weapon.name = weapon.name;
        new_weapon.transform.localRotation = CurWeapon.transform.localRotation;
        Weapons.Add(new_weapon.GetComponent<WeaponBase>());
        NextWeapon(Weapons.Count - 1 - CurWeaponIdx);
    }

    public Vector3 GetAimPoint()
    {

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay,out floorHit,100.0f,LayerMask.GetMask("Floor")))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            return playerToMouse;
        }

        return Vector3.zero;
    }

    //朝向鼠标
    public void RotateToTarget(Vector3 rot)
    {
        transform.LookAt(rot + transform.position);
    }
    //开火函数
    public void Shoot()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("idle"))
        {
            animator.SetBool("Shoot", true);
        }
    }
    //开枪虚函数
    public override void OpenFire()
    {
        base.OpenFire();
        //
        Debug.Log("OpenFire Shoot Bullet!!");
        //
        CurWeapon.OpenFire(transform.forward);
    }
}
