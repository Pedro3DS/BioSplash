using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    public static TimerSystem instance;
    public delegate void PlayerDelegate();
    public static PlayerDelegate playerDelegate;

    public delegate void OnTimeZero();
    public static OnTimeZero onTimeZero;

    [SerializeField] private TextMeshProUGUI _timerOnScreen;
    [SerializeField] private TextMeshProUGUI StartText;
    [SerializeField] private TextMeshProUGUI EndText;

    [Tooltip("Coloque o tempo em segundos")]
    public float maxTime;

    private float _timer, _remainingTime;
    private int _min, _seg;

    public bool isGameplayOn;


    public bool isActive = false;

    public GameObject StartCanvas, WinScreen, EndTextCanvas, EndScreen;


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

    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            isActive = true;
            Begin();
        }
    }
    public void Begin()
    {


        if (isActive)
        {

            if (maxTime <= 0f)
            {
                maxTime = 300f;
            }

            StartCanvas.SetActive(true);
        }
    }
    private void End()
    {
        EndTextCanvas.SetActive(true);

    }

    IEnumerator Timer()
    {
        _timerOnScreen.gameObject.SetActive(true);
        while (isGameplayOn)
        {
            _timer += Time.deltaTime;

            _timerOnScreen.text = FixText(_timer);

            yield return null;
        }

        End();
        onTimeZero?.Invoke();        
    }

    private string FixText(float s)
    {
        _remainingTime = maxTime - s;

        _min = Mathf.FloorToInt(_remainingTime / 60f);
        _seg = Mathf.FloorToInt(_remainingTime % 60f);

        if (_remainingTime <= 0)
        {
            isGameplayOn = false;
            return "00:00";
        }
        else
        {
            return $"{_min:00}:{_seg:00}";
        }
    }

    public void StartSegment(int i )
    {
        switch (i)
        { 
            case 0:
                StartText.text = "Já!";
                break;
            case 1:
                StartCanvas.SetActive(false);
                isGameplayOn = true;
                StartCoroutine(Timer());
                playerDelegate?.Invoke();
                break;
        }
    }
    public void EndSegment() 
    {
        EndTextCanvas.SetActive(false);
        if (OrderSystem.instance.ordersComplete)
        {
            WinScreen.SetActive(true);
            PlayerManager.Instance.ITooCanMakeNavigationJumps(WinScreen.GetComponentInChildren<Button>().gameObject);

        } else
        {
            EndScreen.SetActive(true);
            PlayerManager.Instance.ITooCanMakeNavigationJumps(EndScreen.GetComponentInChildren<Button>().gameObject);
        }
    }

}