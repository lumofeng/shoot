using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int power = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<RobotPlayer>().GetDamage(power);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<RobotEnemy>().GetDamage(power);
        }
        else if (other.gameObject.tag == "Enemy_1")
        {
            other.GetComponent<RobotEnemy1>().GetDamage(power);
        }

        Destroy(this.gameObject);
    }
}
