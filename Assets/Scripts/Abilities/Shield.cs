using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Ability
{
    public GameObject ShieldObj;

    public override void Use()
    {
        ShieldObj.SetActive(true);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(cooldown);

        ShieldObj.SetActive(false);
    }
}
