using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public int gold = 0;
    public LevelStatus[] levels;
    public int adsWatched;

    [Header("Panels")]
    public GameObject LevelsPanel;
    public GameObject ShopPanel;
    public GameObject MenuPanel;
    public GameObject BackgroundPanel;

    public bool reset;
    public int current_Level_Loaded;
    public LevelBox LB;

    private static MainMenuScript menu;

    public GameObject currentPlayer;

    private GameObject[] playersObjects;

    public Text goldText;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (menu == null)
            menu = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitMenu();
    }

    void InitMenu()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            PlayerPrefs.SetInt("Gold", 0); // init gold data
        }

        LoadData();

        for (int i = 1; i < levels.Length; i++)
        {
            if (levels[i - 1].unlocked && levels[i - 1].stars >= 2) 
                levels[i].unlocked = true;
            else levels[i].unlocked = false;

            levels[i].InitLevelButton();
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < levels.Length; i++) // load the stars for each level
        { 
            levels[i].stars = PlayerPrefs.GetInt("Level_" + i.ToString());
            levels[i].done = PlayerPrefs.GetInt("Level_done_" + i.ToString()) == 1 ? true : false;
        }

        gold = PlayerPrefs.GetInt("Gold"); // load gold
        goldText.text = "Gold: " + gold;
    }

    private void SaveData()
    {
        for (int i = 0; i < levels.Length; i++) // save the stars for each level
        {
            PlayerPrefs.SetInt("Level_" + i.ToString(), levels[i].stars);           
            PlayerPrefs.SetInt("Level_done_" + i.ToString(), levels[i].done == true ? 1 : 0);
        }

        PlayerPrefs.SetInt("Gold", gold); // Save gold data
        goldText.text = "Gold: " + gold;
        PlayerPrefs.Save();
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("Gold", gold);
        goldText.text = "Gold: " + gold;

        PlayerPrefs.Save();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level_" + (level + 1).ToString());
        LoadLevelOfIndex(level);
    }

    private void LoadLevelOfIndex(int index)
    {
        current_Level_Loaded = index;
        LevelsPanel.SetActive(false);
        BackgroundPanel.SetActive(false);
    }

    public void GameEnded(int gold, int stars, bool canPass, bool openMenuPanel)
    {
        PlayerPrefs.SetInt("Gold", this.gold + gold);

        this.gold += gold;

        if (canPass)
        { if (levels[current_Level_Loaded].done)
            {
                if (levels[current_Level_Loaded].stars < stars)
                    levels[current_Level_Loaded].stars = stars;
            }
            else
                levels[current_Level_Loaded].stars = stars;

            if (levels[current_Level_Loaded].done == false)
           levels[current_Level_Loaded].done = true;
        }

        SaveData();

        if (openMenuPanel)
        {
            MenuPanel.SetActive(true);
            BackgroundPanel.SetActive(true);
            InitMenu();
        }
    }

    public void Update()
    {
        if (reset) { ResetData(); reset = false; }
    }

    public void ResetData()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            PlayerPrefs.SetInt("Level_" + i.ToString(), 0);
            PlayerPrefs.SetInt("Level_done_" + i.ToString(), 0);
        }

        PlayerPrefs.SetInt("Gold", 100);

        goldText.text = "Gold: " + 100;
        ShopManager shopManager = this.GetComponent<ShopManager>();
        shopManager.ResetData();


        PlayerPrefs.Save();

        InitMenu();
    }

    public void OpenShop()
    {
        MenuPanel.SetActive(false);
        turnOffCharacterAnim();
        ShopPanel.SetActive(true);
        goldText.gameObject.SetActive(true);
    }

    public void OpenMenu()
    {
        MenuPanel.SetActive(true);
        turnOnCharacterAnim();
        ShopPanel.SetActive(false);
        LevelsPanel.SetActive(false);
        goldText.gameObject.SetActive(true);

        LB.CloseCurrentBox();
    }

    public void OpenLevels()
    {
        MenuPanel.SetActive(false);
        turnOffCharacterAnim();
        LevelsPanel.SetActive(true);
        goldText.gameObject.SetActive(false);

        LB.OpenCurrentBox();
    }

    void turnOnCharacterAnim()
    {
        MenuCharacter MC = GameObject.FindGameObjectWithTag("MenuCharacter").GetComponent<MenuCharacter>();
        MC.GetComponent<Animator>().enabled = true;
    }

    void turnOffCharacterAnim()
    {
        MenuCharacter MC = GameObject.FindGameObjectWithTag("MenuCharacter").GetComponent<MenuCharacter>();
        MC.GetComponent<Animator>().enabled = false;
        MC.transform.position = new Vector3(-4000, -4000, 0);
    }
}
