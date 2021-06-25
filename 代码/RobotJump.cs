using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJump: MonoBehaviour
{
    //跳起的力量
    public float JumpGravity = 1000f;
    //刚体
    public Rigidbody rg;
    void Start()
    {
        //寻找刚体组件
        rg = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //判断是否按下空格
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rg.velocity = new Vector3(rg.velocity.x, JumpGravity * Time.deltaTime, rg.velocity.z);

        }
    }
}
