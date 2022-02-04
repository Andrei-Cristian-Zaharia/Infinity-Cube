using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    public GameObject playerSkin;
    public int gold;
    public string donateText;
    public bool donate;
    public bool unlocked;

    public Button inShopButton;
    public GameObject background;

    public Text infoText;

    private MainMenuScript MMS;
    private ShopManager SM;
    private MenuCharacter MC;

    private void Start()
    {
        MC = GameObject.FindGameObjectWithTag("MenuCharacter").GetComponent<MenuCharacter>();
        MMS = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>();
        SM = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<ShopManager>();
    }

    public void TryToUnlock()
    {
        SM.ShowConfirmationPanel(this.GetComponent<CharacterShop>());
    }

    public void UnlockCharacter()
    {
        if (MMS.gold >= gold)
        {
            if (SM == null)
                SM = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<ShopManager>();

            MMS.gold -= gold;
            unlocked = true;

            inShopButton.onClick.RemoveAllListeners();

            inShopButton.onClick.AddListener(SelectThisSkin);

            SM.UpdateInfoTexts();

            SM.SaveData();

            MMS.SaveGold();
        }
        else
            SM.ShowFailPanel();
    }

    public void SelectThisSkin()
    {
        if (unlocked) {
            if (MMS == null)
                MMS = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>();

            if (MC == null)
                MC = GameObject.FindGameObjectWithTag("MenuCharacter").GetComponent<MenuCharacter>();
                
            MMS.currentPlayer = playerSkin;
            MC.changeCharacterSkin(playerSkin);

            if (SM == null)
                SM = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<ShopManager>();

            SM.selectedCharacter = playerSkin.name;
            SM.SaveSelected();
            SM.UpdateInfoTexts(); 
        }     
    }

    public void ResetInfoText()
    {
        if (MMS == null)
            MMS = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>();

        if (donate)
        {
            if (!unlocked)
            { 
                infoText.text = donateText;
                background.GetComponent<Image>().color = MMS.GetComponent<ShopManager>().lockedColor;
            }
            else
            {
                infoText.text = "Unlocked"; 
                background.GetComponent<Image>().color = MMS.GetComponent<ShopManager>().unlockedColor;
            }
        }
        else
        if (unlocked || gold == 0)
        { infoText.text = "Unlocked"; background.GetComponent<Image>().color = MMS.GetComponent<ShopManager>().unlockedColor; }
        else { infoText.text = gold + " Gold"; background.GetComponent<Image>().color = MMS.GetComponent<ShopManager>().lockedColor; }

        if (MMS.currentPlayer == playerSkin)
            infoText.text = "In use";
    }
}
