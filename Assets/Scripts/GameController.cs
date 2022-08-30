using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Variables
    private UI ui;

    [Header("Game Stats")]
    public int CurrentScore;
    public int BestScore;
    public bool GameOnStart;
    public bool GameOver;
    public bool GamePaused;
    public float WallsSpeed;
    public float BackSpeed;
    public float TimeBetweenWalls;
    #endregion

    #region UnityEvents
    private void Awake()
    {
        ui = GetComponent<UI>();
        ui.OffAll();
        ui.StartMenu.SetActive(true);
        Time.timeScale = 1;
        LoadBestScore();
    }

    private void Update()
    {
        Inputs();

        if (GameOver)
        {
            Time.timeScale = 1;
            ui.OffAll();
            ui.GameOver.SetActive(true);
        }

        if (GameOver || GamePaused || GameOnStart)
        {
            return;
        }

        if (CurrentScore > BestScore)
        {
            SetNewBestScore();
        }

        WallsSpeed += 0.0005f;
        BackSpeed += 0.0005f;
        TimeBetweenWalls -= 0.0001f;
    }
    #endregion

    #region Other
    private void SetNewBestScore()
    {
        BestScore = CurrentScore;
        using var sw = new StreamWriter("save.txt");
        sw.WriteLine(BestScore);
    }

    private void LoadBestScore()
    {
        using var sr = new StreamReader("save.txt");
        if (int.TryParse(sr.ReadLine(), out int res))
        {
            BestScore = res;
        }

        else
        {
            using var sw = new StreamWriter("save.txt");
            sw.WriteLine(0);
        }
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver && !GameOnStart)
        {
            OnPause();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Quit();
        }

        if (!GamePaused && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            GameOnStart = false;
            ui.OffAll();
            ui.GameUI.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPause()
    {
        GamePaused = !GamePaused;
        Time.timeScale = (GamePaused) ? 0 : 1;
        ui.PauseMenu.SetActive(GamePaused);
    }

    public static GameController GetGameController()
    {
        return GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
    }
    #endregion
}
