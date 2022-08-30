using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region Variables
    private GameController gameController;

    [Header("GuideAnimation")]
    public Image Image;
    public float timer;
    public bool onFirstSprite;
    public Sprite sprite1;
    public Sprite sprite2;

    [Header("Score")]
    public Image Medal;
    public Image Score;
    public Sprite gold;
    public Sprite silver;
    public Sprite newScore;
    public Sprite normalScore;

    [Header("UI`s")]
    public GameObject StartMenu;
    public GameObject GameUI;
    public GameObject PauseMenu;
    public GameObject GameOver;

    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text gameOverCurrentScore;
    [SerializeField] private TMP_Text gameOverBestScore;
    [SerializeField] private TMP_Text restart;

    private int curScore;
    private int bestScore;
    #endregion

    #region UnityEvents
    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    private void Update()
    {
        curScore = gameController.CurrentScore;
        bestScore = gameController.BestScore;

        if (GameUI.activeInHierarchy == true)
        {
            score.text = $"{curScore}";
        }

        if (StartMenu.activeInHierarchy == true)
        {
            GuideAnim();
        }

        if (GameOver.activeInHierarchy == true)
        {
            gameOverCurrentScore.text = $"{curScore}";

            if (curScore == bestScore)
            {
                Medal.sprite = gold;
                Score.sprite = newScore;
            }

            else if (curScore > bestScore / 2)
            {
                Medal.sprite = silver;
                Score.sprite = normalScore;
            }

            else
            {
                Medal.color = new Color(0, 0, 0, 0);
                Score.sprite = normalScore;
            }

            gameOverBestScore.text = $"{bestScore}";
        }
    }
    #endregion


    #region Other


    public void OffAll()
    {
        StartMenu.SetActive(false);
        GameUI.SetActive(false);
        PauseMenu.SetActive(false);
        GameOver.SetActive(false);
    }

    private void GuideAnim()
    {
        timer += Time.deltaTime;

        if (timer > 0.5)
        {
            Vector2 size;
            if ((onFirstSprite))
            {
                size = new Vector2(356.25f, 306.25f);
            }
            else
            {
                size = new Vector2(356.25f, 318.75f);
            }

            Vector2 pos;
            if ((onFirstSprite))
            {
                pos = new Vector2(-300, 0);
            }
            else
            {
                pos = new Vector2(-300, 6.25f);
            }

            Image.rectTransform.sizeDelta = size;
            Image.rectTransform.anchoredPosition = pos;
            Image.sprite = (onFirstSprite) ? sprite1 : sprite2;
            onFirstSprite = !onFirstSprite;
            timer = 0;
        }
    }
    #endregion
}
