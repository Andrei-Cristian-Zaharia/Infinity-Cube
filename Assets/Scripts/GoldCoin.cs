using System.Collections;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public int amount;
    public AudioClip audioClip;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            GameManager.AddGold(amount);

            if (audioManager.GetComponent<AudioSource>())
            StartCoroutine(DestroyAfterAudio());

            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyAfterAudio()
    {
        GameObject coinAudio = Instantiate(new GameObject("coinAudio"), this.transform.position, Quaternion.identity);
        AudioSource coinAudioSource = coinAudio.AddComponent<AudioSource>();
        coinAudioSource.volume = 0.1f;
        coinAudioSource.spatialBlend = 1f;
        coinAudioSource.bypassEffects = true;
        coinAudioSource.bypassListenerEffects = true;
        coinAudioSource.clip = audioClip;

        coinAudioSource.Play();

        yield return new  WaitForSeconds(coinAudioSource.clip.length);

        Destroy(coinAudio.gameObject);
    }
}
