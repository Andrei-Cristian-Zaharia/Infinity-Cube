using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBullet : MonoBehaviour
{
    public GameObject cannon;
    public float bulletPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Hammer") || collision.CompareTag("MovingPlatform")) Destroy(gameObject);
        if (collision.CompareTag("Player"))
        {
            if (cannon.GetComponent<LineCannonScript>().shootRight)
                PlayerController.instance.Knockback(Vector2.right
                    * cannon.GetComponent<LineCannonScript>().bulletSpeed
                    * bulletPower);
            else
                PlayerController.instance.Knockback(Vector2.left
                    * cannon.GetComponent<LineCannonScript>().bulletSpeed
                    * bulletPower);

            Destroy(gameObject);

            /*
            Vector2 difference = (cannon.transform.position - collision.transform.position).normalized;

            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-difference.x, difference.y)
                * cannon.GetComponent<LineCannonScript>().bulletSpeed
                * bulletPower, ForceMode2D.Impulse);
             */
        }
    }
}
