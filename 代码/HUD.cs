using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    private static HUD instance;
    public static HUD GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    public Image weaponIcon;    //武器图标
    public Text bulletNum;      //子弹数量
    public Text hpNum;            //血量
    // Start is called before the first frame update
    //更新武器
    public void UpdateWeaponUI(Sprite icon,int bullet_num)
    {
        weaponIcon.sprite = icon;
        bulletNum.text = bullet_num.ToString();
    }
    //更新血量
    public void UpdateHpUI(int hp_num)
    {
        hpNum.text = hp_num.ToString();
    }
}
