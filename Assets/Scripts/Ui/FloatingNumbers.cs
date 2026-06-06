using System.Collections;
using UnityEngine;

/// <summary>
/// Esse script funciona junto com o ScoreManager;
/// Ele tem como objetivo, criar numeros na tela, que vão flutuar em direção ao contador, e assim mudar o valor dele
/// esses numeros vão ser criados fazendo calculo transformando a posição do mundo que vai ser passada, e posicionar no rect transform
/// 
/// </summary>
public class FloatingNumbers : MonoBehaviour
{
    public static FloatingNumbers instance;
    [SerializeField] private GameObject floatingNumberPrefab;
    [SerializeField] private RectTransform canvasRectTransform;

    [SerializeField] private RectTransform counterRectTransform;

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

    public RectTransform RandomizeRectTransform(RectTransform rectTransform)
    {
        Vector2 randomOffset = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
        rectTransform.anchoredPosition += randomOffset;
        return rectTransform;
    }

    public float RandomDuration(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void CreateFloatingNumber(int value, Vector3 worldPosition, Color color)
    {
        // Instancia o prefab do número flutuante
        GameObject floatingNumber = Instantiate(floatingNumberPrefab, RandomizeRectTransform(canvasRectTransform).transform);

        // Converte a posição do mundo para a posição da tela
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Posiciona o número flutuante na posição da tela
        // floatingNumber.GetComponent<RectTransform>().position = screenPosition;

        // Define o texto do número flutuante
        floatingNumber.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"+{value.ToString()}";
        // Define a cor do número flutuante
        floatingNumber.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = color;

        SoftNumMovementToTarget(floatingNumber, counterRectTransform.position, RandomDuration(0.5f, 1.5f), value);
    }



    public void SoftNumMovementToTarget(GameObject floatingNumber, Vector3 targetPosition, float duration, int value)
    {
        StartCoroutine(MoveToTarget(floatingNumber, targetPosition, duration, value));
    }

    IEnumerator MoveToTarget(GameObject floatingNumber, Vector3 targetPosition, float duration, int value)
    {
        Vector3 startPosition = floatingNumber.GetComponent<RectTransform>().position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            floatingNumber.GetComponent<RectTransform>().position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Certifique-se de que o número flutuante esteja exatamente na posição do alvo no final
        floatingNumber.GetComponent<RectTransform>().position = targetPosition;

        // Destrói o número flutuante após alcançar o alvo
        Destroy(floatingNumber);
        ScoreManager.instance.AddScore(value);
        }
}
