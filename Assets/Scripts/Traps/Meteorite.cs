using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : TrapScript
{
    public Transform destination;
    public float distance;

    public float speed;
    public bool arrived = false;

    private void Update()
    {
        distance = Vector2.Distance(transform.position, destination.position);

        if (distance < 0.4f) arrived = true;

        if (distance >= 0.01f)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
                new Vector2(destination.position.x, destination.position.y), speed * Time.deltaTime);
        }
        else Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (arrived && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().doubleJump = false;
            collision.GetComponent<PlayerController>().canMove = false;

            Debug.Log("Player got hit by the asteroid !");
        }
    }
}
