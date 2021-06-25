using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BaseRobot : MonoBehaviour
{
    public static GameObject btn;  //当人物死亡后弹出的按钮
   
    public int hp = 100;

    void Start()
    {
        btn = GameObject.Find("Button");        //获取按钮
        this.GetComponent<Button>().onClick.AddListener(OnClick);   监听按钮
        btn.SetActive(false);   //响应按钮事件

       
    }
    //点击按钮事件
    void OnClick()
    {
        // Obj.SetActive(true);
        SceneManager.LoadScene("BattleScene2");

    }
   //判断人物是否存活
    public bool IsAlive()
    {
        return hp > 0;
    }
    //判断人物是否死亡
    public void GetDamage(int dmg)
    {
        hp -= dmg;
        if (!IsAlive())
        {
            Die();
        }
    }
    //死亡函数
    public virtual void Die()
    {
        btn.SetActive(true);
        Debug.Log("!!!!!!");
        //死亡后销毁人物
        Destroy(this.gameObject);
        
    }
    //Shoot Animation Event!
    public virtual void OpenFire()
    {
        
    }
}
