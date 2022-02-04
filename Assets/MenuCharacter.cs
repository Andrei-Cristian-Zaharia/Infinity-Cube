using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    public void changeCharacterSkin(GameObject player)
    {
        this.GetComponent<SpriteRenderer>().sprite = player.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        transform.Rotate(0, 0, .75f);
    }
}
