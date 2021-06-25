using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RobotEnemy1 : BaseRobot1
{
    private NavMeshAgent nav;
    private Animator animator;
    private EnemySight enemySight;
    private EnemySight weaponSight;
    public WeaponBase CurWeapon;
    private GameObject player;
    //Patrolling
    public float patrolSpeed = 5;
    public int wayPointIndex;
    public float patrolWaitTime = 1f;
    public Transform patrolWayPoints;
    private float patrolTimer;
    //Chasing
    public float chaseSpeed = 8;
    public float chaseWaitTime = 5f;
    private float chaseTimer;
    private void Awake()
    {
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
    {
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
