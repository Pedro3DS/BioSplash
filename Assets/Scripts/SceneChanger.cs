using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public static SceneChanger instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadByPlayerInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();

    }
}
