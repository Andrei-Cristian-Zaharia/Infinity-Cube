﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().PlayerDied();
    }
}
