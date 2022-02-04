using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Ability ability;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ability.canUse)
        { ability.Use(); Debug.Log("Ability used !!!"); ability.canUse = false; }
    }

    public void UseAbility()
    {
        if (ability.canUse)
        { ability.Use(); Debug.Log("Ability used !!!"); ability.canUse = false; }
    }
}
