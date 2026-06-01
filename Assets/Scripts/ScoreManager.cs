using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI scoreText, loseScoreText, winScoreText, hiScoreText;
    int score = 0;
    private Coroutine _pointAnimCoroutine;

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

    public void UpdateScoreLerpAndPopAnimation(int value)
    {
        // StartCoroutine(LerpScoreText(value));
        if (_pointAnimCoroutine != null)
            StopCoroutine(_pointAnimCoroutine);

        _pointAnimCoroutine = StartCoroutine(PopTextAnimation());
    }
    IEnumerator PopTextAnimation()
    {
        Vector3 originalScale = scoreText.transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            scoreText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        scoreText.transform.localScale = originalScale;
    }


    public void AddScore(int value)
    {
        // PopTextAnimation();
        score += value;
        scoreText.text = score.ToString();
    }

    public void FinalScore()
    {
        string pointsKey = $"{SceneManager.GetActiveScene().name}_Points";
        Debug.Log(pointsKey);
        loseScoreText.text = "Pontua��o: " + score.ToString();
        winScoreText.text = "Pontua��o: " + score.ToString();
        if (score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "hiscore"))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "hiscore", score);
            PlayerPrefs.SetInt(pointsKey, score);
            Debug.Log(PlayerPrefs.GetInt(pointsKey).ToString());

        }
        hiScoreText.text = "Mais alta: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "hiscore").ToString();
    }
}
