using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public bool audioP = true;
    public bool music = true;

    public Sprite[] musicIcon = new Sprite[2];
    public Button musicButton;

    public Sprite[] audioIcon = new Sprite[2];
    public Button audioButton;

    private AudioSource musicSource;

    public void Start()
    {
        musicSource = this.GetComponent<AudioSource>();
        LoadData();

        UpdateMusic();
        UpdateAudio();
    }

    public void ToogleMusic()
    {
        music = !music;

        UpdateMusic();

        SaveData();
    }

    public void ToogleAudio()
    {
        audioP = !audioP;

        UpdateAudio();

        SaveData();
    }

    private void UpdateMusic()
    {
        if (music) { musicSource.Play(); musicButton.GetComponent<Image>().sprite = musicIcon[1]; }
        else { musicSource.Stop(); musicButton.GetComponent<Image>().sprite = musicIcon[0]; }
    }

    private void UpdateAudio()
    {
        if (audioP) audioButton.GetComponent<Image>().sprite = audioIcon[1]; 
        else audioButton.GetComponent<Image>().sprite = audioIcon[0]; 
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Music", music ? 1 : 0);
        PlayerPrefs.SetInt("Audio", audioP ? 1 : 0);
    }

    public void LoadData()
    {
        music = PlayerPrefs.GetInt("Music") == 1 ? true : false;
        audioP = PlayerPrefs.GetInt("Audio") == 1 ? true : false;
    }
}
