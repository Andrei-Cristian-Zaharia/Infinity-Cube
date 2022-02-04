using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawScript : TrapScript
{
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;

    private GameObject player;

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (pathCreator)
        {
            if (Mathf.Abs(this.transform.position.y - player.transform.position.y) < 20f)
            {
                distanceTravelled += speed * Time.deltaTime * SlowMotionTime;
                this.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        { 
            collision.GetComponent<PlayerController>().doubleJump = false;
            collision.GetComponent<PlayerController>().canMove = false;
        }
    }
}
