using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public PlayerInputManager playerManager;
    private bool roomFull = false;
    public GameObject roadBlock;
    public EventSystem pe1, pe2;
    public GameObject Buttons;
    void Start()
    {
        Buttons.SetActive(false);
        roadBlock.SetActive(true);

    }

    void Update()
    {
        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
            roadBlock.SetActive(false);
            Buttons.SetActive(true);
            pe1.SetSelectedGameObject(pe1.firstSelectedGameObject);
            pe2.SetSelectedGameObject(pe2.firstSelectedGameObject);
            PlayerManager.Instance?.FindPlayers();
        }
    }
    public void Close()
    {
        Application.Quit();
    }
    public void Text()
    {
        Debug.Log("alalala");
    }

    public void NavigationJump(GameObject gameObject)
    {
        pe1.SetSelectedGameObject(gameObject);
    }
}
