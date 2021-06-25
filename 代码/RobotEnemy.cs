using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//敌人行为逻辑代码
public class RobotEnemy : BaseRobot
{
    private NavMeshAgent nav;//保存代理体
    private Animator animator;//动画状态机
    private EnemySight enemySight;//敌人视野
    private EnemySight weaponSight;//武器视野
    public WeaponBase CurWeapon;//当前武器
    private GameObject player;//对人物的引用
    //Patrolling
    public float patrolSpeed = 5;	//巡逻速度
    public int wayPointIndex;	//敌人路径点缩影
    public float patrolWaitTime = 1f;//巡逻等待时间
    public Transform patrolWayPoints;//巡逻路径点
    private float patrolTimer;	//巡逻到路径点后的计时器
    //Chasing
    public float chaseSpeed = 8;//追击速度
    public float chaseWaitTime = 5f;	//追击的等待时间
    private float chaseTimer;		//追击等待计时器
    private void Awake()
    {
        //获取组件，物体
        enemySight = transform.Find("EnemySight").GetComponent<EnemySight>();
        weaponSight = transform.Find("WeaponSight").GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //开枪函数
    public void Shooting()
    {
        nav.isStopped = true;
        nav.speed = 0;
        //
        Vector3 lookPos = player.transform.position;
        lookPos.y = transform.position.y;
        Vector3 targetDir = lookPos - transform.position;
        float step = 5 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward,targetDir,step,0);
        transform.rotation = Quaternion.LookRotation(newDir);
        //
        animator.SetBool("Shoot", true);
    }

    public override void OpenFire()
    {
        base.OpenFire();
        CurWeapon.OpenFire(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {//判断玩家是否死亡
        if (!RobotPlayer.GetInstance().IsAlive()) return;

        if (weaponSight.playerInSight)
        {
            //shoot
            Shooting();
        }
        else if (enemySight.playerInSight)
        {
            //chase
            Chasing();
        }
        else
        {
            //patrol
            Patrolling();
        }
        animator.SetFloat("Speed",nav.speed / chaseSpeed);
    }
    //追击人物函数
    public void Chasing()
    {
        nav.isStopped = false;
        nav.speed = chaseSpeed;
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4)
        {
            nav.destination = enemySight.personalLastSighting;
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                chaseTimer += Time.deltaTime;
                if (chaseTimer >= chaseWaitTime)
                {
                    chaseTimer = 0;
                    nav.speed = 0;
                }
            }
            else
            {
                chaseTimer = 0;
            }
        }
    }
    //巡逻函数
    public void Patrolling()
    {
        nav.isStopped = false;
        nav.speed = patrolSpeed;
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.childCount - 1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0;
            }
        }
        else
        {
            patrolTimer = 0;
        }
        nav.destination = patrolWayPoints.GetChild(wayPointIndex).position;
    }
}
