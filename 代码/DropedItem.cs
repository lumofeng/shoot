using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : MonoBehaviour
{
    public GameObject WeaponPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            RobotPlayer.GetInstance().AddWeapon(WeaponPrefab);
            Destroy(this.gameObject);
        }
    }
}
