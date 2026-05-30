using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public PlayerInputManager playerManager;
    private bool roomFull = false;
    public GameObject roadBlock;
    public EventSystem pe1, pe2;
    public GameObject Buttons;

    [SerializeField] private GameObject[] _activeObjectsBeforeCanPlay;

    [SerializeField] private Image P1Image;
    [SerializeField] private Image P2Image;
    [SerializeField] private RotateIcon P1ImageAnim;
    [SerializeField] private RotateIcon P2ImageAnim;

    [SerializeField] private Color InGameColor;
    [SerializeField] private Color OutGameColor;


    void Start()
    {
        Buttons.SetActive(false);
        roadBlock.SetActive(true);

        P2ImageAnim.enabled = false;
        P1ImageAnim.enabled = false;

    }

    void Update()
    {
        UpdatePlayerIconsByPlayerConncetion();
        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
            roadBlock.SetActive(false);
            EnableObjects();
            Buttons.SetActive(true);
            pe1.SetSelectedGameObject(pe1.firstSelectedGameObject);
            pe2.SetSelectedGameObject(pe2.firstSelectedGameObject);
            PlayerManager.Instance?.FindPlayers();
        }
    }

    void UpdatePlayerIconsByPlayerConncetion()
    {
        if (playerManager.playerCount == 0)
        {
            P1Image.color = OutGameColor;
            P2Image.color = OutGameColor;
                P1ImageAnim.enabled = false;
                P2ImageAnim.enabled = false;
        }
        else if (playerManager.playerCount == 1)
        {
            P1Image.color = InGameColor;
            P2Image.color = OutGameColor;
                P1ImageAnim.enabled = true;
                P2ImageAnim.enabled = false;
        }
        else if (playerManager.playerCount == 2)
        {
            P1Image.color = InGameColor;
            P2Image.color = InGameColor;
                P1ImageAnim.enabled = true;
                P2ImageAnim.enabled = true;
        }
    }

    void EnableObjects()
    {
        foreach (GameObject obj in _activeObjectsBeforeCanPlay)
        {
            obj.SetActive(true);
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
