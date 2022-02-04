using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : TrapScript
{
    private GameObject player;
    public float offest;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
            if (player.transform.position.y < this.transform.position.y - offest)
                Physics2D.IgnoreCollision(player.transform.Find("Collider").GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
            else 
                Physics2D.IgnoreCollision(player.transform.Find("Collider").GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), false);
    }
}
