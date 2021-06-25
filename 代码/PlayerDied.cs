using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerDied : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject Obj;
  
    void Start()//start()一开始进入游戏就进行调用
    {
        Obj = GameObject.Find("Image");//找到Button对象
        this.GetComponent<Button>().onClick.AddListener(OnClick);//设置监听
        Obj.SetActive(false);//将其隐藏


    }
    void OnClick()
    {
        Obj.SetActive(true);//将其显现
       
    }
}
