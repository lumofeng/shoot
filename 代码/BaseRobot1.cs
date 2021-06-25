using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BaseRobot1 : MonoBehaviour
{
  
    public static GameObject btn2;  //当人物死亡后弹出的按钮
    public int hp = 100;

    void Start()
    {
        

        //btn2
         btn2= GameObject.Find("Button_2");
        this.GetComponent<Button>().onClick.AddListener(OnClick_2);
        btn2.SetActive(false);
    }

    void OnClick_2()
    {
        // Obj.SetActive(true);
        SceneManager.LoadScene("SampleScene");

    }
    public bool IsAlive()
    {
        return hp > 0;
    }
    public void GetDamage(int dmg)
    {
        hp -= dmg;
        if (!IsAlive())
        {
            Die();
        }
    }
    public virtual void Die()
    {
      
        btn2.SetActive(true);
        Debug.Log("????????");
        Destroy(this.gameObject);
        
    }
    //Shoot Animation Event!
    public virtual void OpenFire()
    {
        
    }
}
