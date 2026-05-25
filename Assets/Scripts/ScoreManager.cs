using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

   [SerializeField] TextMeshProUGUI scoreText;
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
}
