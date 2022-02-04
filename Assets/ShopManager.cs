using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public CharacterShop[] characterShops;
    public string selectedCharacter;

    public Color lockedColor;
    public Color unlockedColor;

    public GameObject failedPanel;
    public GameObject confirmPanel;

    private CharacterShop currentCS;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetString("FirstTime", "Nope");
            PlayerPrefs.SetInt("Character_0", 1);
            PlayerPrefs.Save();
            selectedCharacter = characterShops[0].playerSkin.name;
            SaveSelected();
        }

        LoadData();
    }

    public void SaveData()
    {
        for (int i = 0; i < characterShops.Length; i++)
        {
            PlayerPrefs.SetInt("Character_" + i.ToString(), characterShops[i].unlocked ? 1 : 0);
            SaveSelected();
        }

        PlayerPrefs.Save();
    }

    public void SaveSelected()
    {
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);

        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        for (int i = 0; i < characterShops.Length; i++)
        {
            characterShops[i].unlocked = PlayerPrefs.GetInt("Character_" + i.ToString()) == 1 ? true : false;

            characterShops[i].GetComponent<Button>().onClick.RemoveAllListeners();

            if (characterShops[i].unlocked)
                characterShops[i].GetComponent<Button>().onClick.AddListener(characterShops[i].SelectThisSkin);
            else if (!characterShops[i].donate)
                characterShops[i].GetComponent<Button>().onClick.AddListener(characterShops[i].TryToUnlock);
        }

        selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        StartSelectCharacter();
    }

    private void StartSelectCharacter()
    {
        foreach (CharacterShop c in characterShops)
        {
            if (c.playerSkin.name == selectedCharacter)
            {
                c.SelectThisSkin();
                UpdateInfoTexts();

                return;
            }
        }
    }

    public void UpdateInfoTexts()
    {
        foreach (CharacterShop characterShop in characterShops)
            characterShop.ResetInfoText();
    }

    public void DonateComplete()
    {
        PlayerPrefs.SetInt("Character_4", 1);
        PlayerPrefs.Save();
    }

    public void ShowFailPanel()
    {
        failedPanel.SetActive(true);
    }

    public void CloseFailPanel()
    {
        failedPanel.SetActive(false);
    }

    public void ShowConfirmationPanel(CharacterShop CS)
    {
        confirmPanel.SetActive(true);
        currentCS = CS;
    }

    public void CloseConfirmationPanel()
    {
        confirmPanel.SetActive(false);
    }

    public void ConfirmBuyButton()
    {
        if (currentCS != null)
            currentCS.UnlockCharacter();

        CloseConfirmationPanel();
    }

    public void ResetData()
    {
        for (int i = 1; i < characterShops.Length; i++)
        {
            PlayerPrefs.SetInt("Character_" + i.ToString(), 0);
        }

        PlayerPrefs.SetString("SelectedCharacter", characterShops[0].playerSkin.name);

        PlayerPrefs.Save();

        LoadData();
    }

    public bool BuyButton()
    {
        return true;
    }
}
