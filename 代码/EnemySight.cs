using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float fov = 110f;    //视野范围
    public bool playerInSight;  //视野记录
    public Vector3 personalLastSighting;    //记录最后一次看到玩家的位置
    public static Vector3 resetPos = Vector3.back;      //视野球的碰撞体
    private GameObject player;  
    private SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        personalLastSighting = resetPos;
    }

    private void OnTriggerStay(Collider other)
    {
        //当玩家进入视野球后，玩家与当前视野球所在的敌人位置连线表示方向向量
        if (other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction,transform.forward);
            //如果没有超过视野范围
            if (angle < fov * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up,direction.normalized,out hit,col.radius))
                {
                    //如果没有障碍物
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        personalLastSighting = player.transform.position;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            //如果玩家离开视野球
            playerInSight = false;
        }
    }
}