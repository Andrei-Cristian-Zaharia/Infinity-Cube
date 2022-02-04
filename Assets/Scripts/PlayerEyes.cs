using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RollDirection { None, Left, Right }

public class PlayerEyes : MonoBehaviour
{
    public GameObject Eyes;

    public PlayerState state;
    public RollDirection direction;

    private Animator anim;
    private RollDirection currentDirection;

    private void Start()
    {
        anim = Eyes.GetComponent<Animator>();
    }

    void EyesRoll()
    {
        if (direction == RollDirection.None)
        {
            anim.SetBool("Rolling", false);
            return;
        }

        if (direction == RollDirection.Left && canStartRolling())
        {
            anim.Play("StartRollingLeft");
            anim.SetBool("Rolling", true);
            currentDirection = direction;
        }
        else if (direction == RollDirection.Right && canStartRolling())
        {
            anim.Play("StartRollingRight");
            anim.SetBool("Rolling", true);
            currentDirection = direction;
        }
    }

    bool canStartRolling()
    {
        if (anim.GetBool("Rolling") && currentDirection != direction)
            return true;

        if (anim.GetBool("Rolling") == false)
            return true;

        return false;
    }

    private void Update()
    {
        if (state == PlayerState.NotGround)
        {
            EyesRoll();
        }

        if (state == PlayerState.Ground)
        {
            if (anim.GetBool("Rolling"))
                anim.SetBool("Rolling", false);
        }
    }
}
