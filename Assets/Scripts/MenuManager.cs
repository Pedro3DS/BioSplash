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

    public Button startButton;

    private bool _canPlay = false;

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

    /// <summary>
    /// Faz uma animação cartunesca de encolher a imagem que é utilizada como Bloqueio de entrada no menu, para dar a sensação de que o menu se abriu e os jogadores podem entrar. O bloqueio é desativado no final da animação.
    /// Essa Animação é feita por script, Ela vai encolher em uma certa velocidade, quanod estiver bem pequena, quase sumindo, ela volta um pouquinho(parecer que recocheteou) e depois some, dando a sensação de que o menu se abriu.
    /// O bloqueio é desativado no final da animação.
    /// </summary>
    void HideRoadBlockWithAnim()
    {
        
        StartCoroutine(HideRoadBlockWithAnimCoroutine());
    }

    IEnumerator HideRoadBlockWithAnimCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        float scale = 1f;
        while (scale > 0.1f)
        {
            scale -= Time.deltaTime * 2f; // Velocidade de encolhimento
            roadBlock.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }
        roadBlock.SetActive(false);
    }

    void Update()
    {
        UpdatePlayerIconsByPlayerConncetion();

        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
            // roadBlock.SetActive(false);
            EnableObjects();
            HideRoadBlockWithAnim();
            
            Buttons.SetActive(true);
            EventSystem.current.SetSelectedGameObject(startButton.gameObject);
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
