using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NextScene : BaseRobot
{
    // Start is called before the first frame update
    public static GameObject Obj;

    void Start()
    {
        
        Obj = GameObject.Find("Button");
        this.GetComponent<Button>().onClick.AddListener(OnClick);
        Obj.SetActive(false);

    }
    void OnClick()
    {
       // Obj.SetActive(true);
       if(hp <= 0)
        {
            SceneManager.LoadScene("BattleScene2");
        }
        else
        {
            Debug.Log("failed");
        }

    }
}
