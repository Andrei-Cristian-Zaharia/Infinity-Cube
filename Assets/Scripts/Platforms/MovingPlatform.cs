using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PlatformScript
{
    public float speed;
    private bool canMove = true;
    public float waitTime;

    public Transform[] moveSpot;
    private int index;
    private void Update()
    {
        if (canMove)
        transform.position = Vector2.MoveTowards(transform.position, moveSpot[index].position, speed * Time.deltaTime * SlowMotionTime);

        if (Vector2.Distance(transform.position, moveSpot[index].position) < 0.2f)
        {
            canMove = false;

            index++;
            if (index == moveSpot.Length) index = 0;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(waitTime);

        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
