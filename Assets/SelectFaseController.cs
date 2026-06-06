// using Unity.VisualScripting;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct FaseCardInfos
{
    public string FaseName;
    public string FaseTime;

    public Sprite FaseSprite;
    [TextArea(2, 5)]
    public string Description;
    public string SceneName;

    public int PointsForTwoStars;
    public int PointsForThreeStars;

    public Color CardColor;
    public bool Unlocked;
}


/// <summary>
/// Esse script funcionara assim, Terão Pontos/Botões no mapa referentes as fases.
/// Quando esse ponto/Botão for Selected, Eles vão Alterar o card de informações(Nome da Fase, Tempo, Imagem, Descrição)
/// Nesse Card, Tem o Campo De Estrelas
/// Quando ENtrar na fase com o respectivo card,Vai ser criado dois PLayer Prefs, se ja ouver, não ira criar;
/// Um player pref São os pontos que ele pegou, e um player pref bool para saber se o Objetivo for concluido;
/// Se apenas o objetivo for concluido, Ele ganha uma estrela
/// se ele só tiver pontos, tera um calculo para saber se eles conseguiram os pontos nescessarios para 2 estrelas
/// ex: 100 pontos = 1 estrela
///     250 pontos = 2 estrelas
///     250 pontos + Objetivo = 3 Estrelas
///     Obs: Preciso ter o controle publico sobre esses valores 
/// </summary>

public class SelectFaseController : MonoBehaviour
{
    [SerializeField] private FaseCardInfos[] _fasesInfos;

    public FaseCardInfos[] FasesInfos => _fasesInfos;
    public RectTransform[] FasesPos;
    public Image PlayerIcon;
    public TransitionAsyncWithParticles TransitionAsyncWithParticles;
    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private Button[] tutorialButtons;
    [SerializeField] private EventSystem _eventSystems;

    [Header("Configurações de Card")]
    [SerializeField] private TMP_Text _faseNameText;
    [SerializeField] private TMP_Text _faseTimeText;
    [SerializeField] private TMP_Text _desctiptionText;
    [SerializeField] private Image _faseImage;
    [SerializeField] private Image _cardImage;

    [Header("Configurações de Estrelas")]
    [SerializeField] private Sprite _starFilledSprite;
    [SerializeField] private Sprite _starEmptySprite;
    [SerializeField] private Image[] _starImages; // Array de imagens para as estrelas

    private int _currentFaseIndex = 0;

    [SerializeField] private string _sceneToLoad;


    private void Start()
    {
        CheckButtons();
    }

    public void SetFaseCardInfos(int index)
    {
        if (index < 0 || index >= _fasesInfos.Length)
        {
            Debug.LogError("Index fora do intervalo das fases disponíveis.");
            return;
        }

        FaseCardInfos selectedFase = _fasesInfos[index];
        _currentFaseIndex = index;
        _sceneToLoad = selectedFase.SceneName;
        UpdateFaseCardUI(selectedFase);
        UpdatePlayerIconPosition(index);
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

    public void CheckButtons()
    {

        for (int i = 0; i < _fasesInfos.Length; i++)
        {
            if (i == 0)
            {
                _levelButtons[i].interactable = true;
            } else if (PlayerPrefs.HasKey($"Level{i}_Points") && PlayerPrefs.HasKey($"Level{i}_ObjectiveCompleted"))
            {
                if (PlayerPrefs.GetInt($"Level{i}_Points") >= _fasesInfos[i].PointsForTwoStars || PlayerPrefs.GetInt($"Level{i}_ObjectiveCompleted") == 1)
                {
                    _levelButtons[i].interactable = true;
                }
  
                }
             
            else
            {
                _levelButtons[i].interactable = false;
            }
        }
    }

    void UpdateFaseCardUI(FaseCardInfos faseInfo)
    {
        _faseNameText.text = faseInfo.FaseName;
        _faseTimeText.text = faseInfo.FaseTime.ToString();
        _desctiptionText.text = faseInfo.Description;
        _faseImage.sprite = faseInfo.FaseSprite;
        _cardImage.color = faseInfo.CardColor;

        CheckPlayerPrefs(faseInfo.SceneName, Array.IndexOf(_fasesInfos, faseInfo));
    }

    public void LoadCurrentFaseScene()
    {
        CreatePlayerPrefsForFase(_sceneToLoad);
        TransitionAsyncWithParticles.LoadSceneAsync(_sceneToLoad);
        AudioController.Instance.SoftAudioTransition("GameMusic", 2.5f);
    }

    public void CreatePlayerPrefsForFase(string faseName)
    {
        string pointsKey = $"{faseName}_Points";
        string objectiveKey = $"{faseName}_ObjectiveCompleted";

        if (!PlayerPrefs.HasKey(pointsKey))
            PlayerPrefs.SetInt(pointsKey, 0);

        if (!PlayerPrefs.HasKey(objectiveKey))
            PlayerPrefs.SetInt(objectiveKey, 0);
    }

    void CheckPlayerPrefs(string faseName, int faseIndex)
    {
        string pointsKey = $"{faseName}_Points";
      //  Debug.Log(pointsKey);
       // Debug.Log(PlayerPrefs.GetInt(pointsKey).ToString());
        
        string objectiveKey = $"{faseName}_ObjectiveCompleted";

        if (PlayerPrefs.HasKey(pointsKey) && PlayerPrefs.HasKey(objectiveKey))
        {
            int points = PlayerPrefs.GetInt(pointsKey);
            bool objectiveCompleted = PlayerPrefs.GetInt(objectiveKey) == 1;

            // Supondo que o índice da fase atual seja 0, você pode ajustar conforme necessário
            int pointsForTwoStars = _fasesInfos[faseIndex].PointsForTwoStars;
            int pointsForThreeStars = _fasesInfos[faseIndex].PointsForThreeStars;
            UpdateCardStars(points, objectiveCompleted, pointsForTwoStars, pointsForThreeStars);
        }
        else
        {
            int pointsForTwoStars = _fasesInfos[faseIndex].PointsForTwoStars;
            int pointsForThreeStars = _fasesInfos[faseIndex].PointsForThreeStars;
            UpdateCardStars(0, false, pointsForTwoStars, pointsForThreeStars);
        }

    }

    void UpdateCardStars(int points, bool objectiveCompleted, int pointsForTwoStars, int pointsForThreeStars)
    {
        Debug.Log(points);
        int starsEarned = 0;

        if (objectiveCompleted)
            starsEarned++;

        if (points >= pointsForTwoStars) // Supondo que o primeiro elemento do array seja a fase atual
            starsEarned++;
        if (points >= pointsForThreeStars)
            starsEarned++;

        for (int i = 0; i < _starImages.Length; i++)
        {
            if (i < starsEarned)
                _starImages[i].sprite = _starFilledSprite;
            else
                _starImages[i].sprite = _starEmptySprite;
        }
    }


}
