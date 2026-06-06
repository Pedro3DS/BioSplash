using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public Button resumeButton;

    private bool _isPaused = false;

    public void OpenMenuInGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OpenMenu();
        }
    }

    void Update()
    {
        if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OpenMenu();
        }
        if(Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
        {
            OpenMenu();
        }
    }



    public void OpenMenu()
    {
        if (_isPaused) CloseMenu();
        else
        {
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
            _isPaused = true;
            PauseGame();

        }
    }

    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
        _isPaused = false;
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    void TimeUpdate() { }
}
