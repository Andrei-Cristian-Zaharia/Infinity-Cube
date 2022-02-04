using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [Header("Details")]
    public new string name;
    public float SlowMotionTime = 1;

    public AudioSource audioSource;

    public void PlaySoundEffect()
    {
        audioSource.Play();
    }
}
