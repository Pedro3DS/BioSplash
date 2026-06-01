using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI scoreText, loseScoreText, winScoreText, hiScoreText;
    int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        TimerSystem.onTimeZero += FinalScore;
    }
    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public int CalculateScore(Fish fish1, Fish fish2)
    {
        float mult = fish1.mult + fish2.mult;
        Debug.Log(mult);
        int basePoints = fish1.baseScore + fish2.baseScore;
        int added = Mathf.RoundToInt(basePoints * mult);
        return added;
    }

    public void AddScore(int value)
    {
        
        score += value;
        scoreText.text = score.ToString();
    }

    public void FinalScore()
    {
        string pointsKey = $"{SceneManager.GetActiveScene().name}_Points";
        Debug.Log(pointsKey);
        loseScoreText.text = "Pontuação: " + score.ToString();
        winScoreText.text = "Pontuação: " + score.ToString();
        if (score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "hiscore"))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "hiscore", score);
            PlayerPrefs.SetInt(pointsKey, score);
            Debug.Log(PlayerPrefs.GetInt(pointsKey).ToString());

        }
        hiScoreText.text = "Mais alta: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "hiscore").ToString();
    }
}
