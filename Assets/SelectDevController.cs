// using Unity.VisualScripting;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct FaseDevInfos
{
    public string Name;
    [TextArea(2, 5)]
    public string Description;
    public Sprite DevSprite;
    public Sprite LittleDevIcon;
    public bool HasLinearEffect;
    
}

public class SelectDevController : MonoBehaviour
{
    [SerializeField] private FaseDevInfos[] _fasesInfos;

    public FaseDevInfos[] FasesInfos => _fasesInfos;
    public RectTransform[] FasesPos;
    public Image PlayerIcon;
    public TransitionAsyncWithParticles TransitionAsyncWithParticles;
    [SerializeField] private Button[] _levelButtons;

    [Header("Configurações de Card")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _devImage;
    [SerializeField] private Image _devBGImage;
    [SerializeField] private GameObject _linearEffectImage;
    [SerializeField] private GameObject _StartButton;

    [SerializeField] private string _sceneToLoad = "MainMenu";

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_levelButtons[0].gameObject);
        // CheckButtons();
    }

    public void SetCardInfos(int index)
    {
        if (index < 0 || index >= _fasesInfos.Length)
        {
            Debug.LogError("Index fora do intervalo das fases disponíveis.");
            return;
        }

        FaseDevInfos selectedFase = _fasesInfos[index];
        // _currentFaseIndex = index;
        UpdateCardUI(selectedFase);
        UpdatePlayerIconPosition(index);
    }
    public void ShowStartButton()
    {
        _StartButton.SetActive(true);
    }
    public void HideStartButton()
    {
        _StartButton.SetActive(false);
    }

    public void UpdatePlayerIconPosition(int index)
    {
        if (index < 0 || index >= FasesPos.Length)
        {
            Debug.LogError("Index fora do intervalo das posições disponíveis.");
            return;
        }
        
        PlayerIcon.rectTransform.position = FasesPos[index].position;
    }

    void UpdateCardUI(FaseDevInfos faseInfo)
    {
        _nameText.text = faseInfo.Name;
        _descriptionText.text = faseInfo.Description;
        _devImage.sprite = faseInfo.DevSprite;
        _devBGImage.sprite = faseInfo.DevSprite;
        _linearEffectImage.SetActive(faseInfo.HasLinearEffect);
        PlayerIcon.sprite = faseInfo.LittleDevIcon;

    }

    public void LoadCurrentFaseScene()
    {
        TransitionAsyncWithParticles.LoadSceneAsync(_sceneToLoad);
    }


}
