using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Sprite icon;
    public Transform muzzle;
    public GameObject bulletPrefab;
    public int bulletNum;
    public float bulletSpeed = 12;
    //
    public void OpenFire(Vector3 dir)
    {
        if (bulletNum > 0)
        {
            Debug.Log("Weapon Fire!");
            //
            var bullet = GameObject.Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
            bulletNum--;
        }
    }
}
