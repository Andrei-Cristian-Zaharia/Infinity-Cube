using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public Slider slider;
    private GameObject goal;
    private GameObject player;
    private float endPos;
    private float startPos;

    public static int gold;
    public float timer = 0f;

    // EndGame Panel
    public GameObject endGamePanel;

    public Text titleText;
    public Text starsText;
    public GameObject winPanel;
    public GameObject lostPanel;

    private MainMenuScript MMS;
    private int stars = 0;
    private bool canPass = false;

    // Set Player Stats
    private GameObject vCam;
    public GameObject abilityButton;

    public void Start()
    {
        MMS = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>();

        player = Instantiate(MMS.currentPlayer, new Vector3(0, 0.31f, 0), Quaternion.identity);
        abilityButton.GetComponent<Button>().onClick.AddListener(player.GetComponent<AbilityManager>().UseAbility);

        goal = GameObject.FindGameObjectWithTag("Goal");

        vCam = GameObject.FindGameObjectWithTag("VCAM");
        vCam.GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;

        if (player.gameObject.name == "Normal Player(Clone)")
            abilityButton.SetActive(false);

        if (player == null) Debug.LogError("There is no player in the game !!!");
        if (goal == null) Debug.LogError("There is no goal point in the game !!!");

        startPos = player.transform.position.y;
        endPos = goal.transform.position.y;
        slider.maxValue = endPos;
        slider.minValue = startPos;
        slider.minValue = startPos;

        gold = 0;
    }

    public void Update()
    {
        slider.value = player.transform.position.y;
        timer += Time.deltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);

        ResumeGame();
    }

    public void PlayerReachedTheEndPoint()
    {
        stars = CalculateStarts(timer);
        canPass = true;

        endGamePanel.SetActive(true);

        winPanel.SetActive(true);
        titleText.text = "Congratulations";
        starsText.text = "Stars: " + stars.ToString();

        PauseGame();
    }

    public void PlayerDied()
    {
        canPass = false;

        endGamePanel.SetActive(true);

        lostPanel.SetActive(true);
        titleText.text = "Cube lost in space :(";
        starsText.text = "Stars: 0";

        PauseGame();
    }

    public void RetryLevel()
    {
        MMS.GameEnded(gold, stars, canPass, false);
            MMS.LoadLevel(MMS.current_Level_Loaded);

        ResumeGame();
    }

    public void NextLevel()
    {
        MMS.GameEnded(gold, stars, canPass, false);

        if (canPass)
            MMS.LoadLevel(MMS.current_Level_Loaded + 1);

        ResumeGame();
    }

    public void BackToMenu()
    {
        MMS.GameEnded(gold, stars, canPass, true);

        SceneManager.LoadScene("Menu");

        ResumeGame();
    }

    public static void AddGold(int amount)
    {
        gold += amount;
    }

    private int CalculateStarts(float timer)
    {
        if (MMS.levels[MMS.current_Level_Loaded].timesForStars[2] >= timer)
            return 3;
        else
        if (MMS.levels[MMS.current_Level_Loaded].timesForStars[1] >= timer)
            return 2;
        else
        if (MMS.levels[MMS.current_Level_Loaded].timesForStars[0] >= timer)
            return 1;

        return 0;
    }
}