using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private RectTransform gameOverMenu;
    private float _fixedTimestep = 0.2f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        ResetState();
    }

    public void increseScore(int x)
    {
        _score += x;
        scoreText.SetText(_score.ToString());

        if (_score % 10 == 0)
        {
            _fixedTimestep -= 0.01f / Mathf.Pow(2, _score/10);
            Time.fixedDeltaTime = _fixedTimestep;
        }
    }

    public void ResetState()
    {
        _fixedTimestep = 0.1f;
        Time.fixedDeltaTime = _fixedTimestep;
        _score = 0;
        scoreText.SetText(_score.ToString());
        gameOverMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void gameOver(bool won)
    {
        winText.SetText(won ? "you win!" : "you lose!");
        gameOverMenu.gameObject.SetActive(true);
        finalScoreText.SetText("Final Length : " + _score.ToString());
        Time.timeScale = 0;
    }

}
