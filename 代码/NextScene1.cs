using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NextScene1 : BaseRobot1
{
    // Start is called before the first frame update
    public static GameObject Obj;

    void Start()
    {
        
        Obj = GameObject.Find("Button_2");
        this.GetComponent<Button>().onClick.AddListener(OnClick);
        Obj.SetActive(false);

    }
    void OnClick()
    {
       // Obj.SetActive(true);
       if(hp <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.Log("failed");
        }

    }
}
