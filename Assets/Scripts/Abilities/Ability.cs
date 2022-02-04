using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float cooldown;
    public bool canUse = false;

    public virtual void Use() { Debug.LogError("No ability set yet."); }
}
