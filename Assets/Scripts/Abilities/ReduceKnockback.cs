using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceKnockback : Ability
{
    public GameObject bulletLineCannon;
    public GameObject bulletCannon;

    public override void Use()
    {
        bulletLineCannon.GetComponent<LineBullet>().bulletPower = 100;
        bulletCannon.GetComponent<Rigidbody2D>().mass = 12;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);

        bulletLineCannon.GetComponent<LineBullet>().bulletPower = 200;
        bulletCannon.GetComponent<Rigidbody2D>().mass = 9;
    }
}
