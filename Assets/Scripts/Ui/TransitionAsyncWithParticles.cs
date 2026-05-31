using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionAsyncWithParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _transitionParticles;

    [SerializeField] private float _transitionDuration = 1f;

    [SerializeField] private string _sceneToLoad;

    [SerializeField] private bool _loadSceneOnStart = false;
    [SerializeField] private bool _dontDestroyOnLoad = false;

    [SerializeField] private Image fadeImage;

    private bool _isTransitioning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_dontDestroyOnLoad)        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        if(_isTransitioning) return;
        _isTransitioning = true;
        _transitionParticles.Play();
        StartCoroutine(LoadSceneAsyncWithFade(sceneName, _transitionDuration, fadeImage));
    }

    IEnumerator LoadSceneAsyncWithFade(string sceneName, float duration, Image fadeImage)
    {
        float elapsedTime = 0f;
        Color initialColor = fadeImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            fadeImage.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Optionally, you can add a fade-out effect here after the scene has loaded
        _transitionParticles.Stop(); // Stop the particle system from looping

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
