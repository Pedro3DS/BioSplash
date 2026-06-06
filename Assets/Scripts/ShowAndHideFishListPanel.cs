using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowAndHideFishListPanel : MonoBehaviour
{

    public Image fishListPanel;

    public RectTransform panelRectTransform;

    public RectTransform ShowPosition;
    public RectTransform HidePosition;

    private bool isPanelVisible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowPanel(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(isPanelVisible)
            {
                HidePanel();
            }
            else
            {
                ShowPanel();
            }
        }
    }

    void ShowPanel()
    {
        panelRectTransform.anchoredPosition = ShowPosition.anchoredPosition;
        isPanelVisible = true;
    }

    void HidePanel()
    {
        panelRectTransform.anchoredPosition = HidePosition.anchoredPosition;
        isPanelVisible = false;
    }
    
}
