using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : Ability
{
    public override void Use()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);
    }
}
