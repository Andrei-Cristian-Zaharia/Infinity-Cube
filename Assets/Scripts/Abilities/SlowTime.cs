using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : Ability
{
    public override void Use()
    {
        TrapScript[] objs = FindObjectsOfType<TrapScript>();

        foreach (TrapScript obj in objs)
        {
            obj.SlowMotionTime = Time.timeScale / 3;
        }

        StartCoroutine(DisableSlowMotion());
    }

    IEnumerator DisableSlowMotion()
    {
        yield return new WaitForSeconds(cooldown);

        TrapScript[] objs = FindObjectsOfType<TrapScript>();

        foreach (TrapScript obj in objs)
        {
            obj.SlowMotionTime = Time.timeScale;
        }
    }
}
