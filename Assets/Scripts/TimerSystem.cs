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

    public static event Action onTimeZero;

    [SerializeField] private Text _timerOnScreen;

    [Tooltip("Coloque o tempo em segundos")]
    public float maxTime;

    private float _timer, _remainingTime;
    private int _min, _seg;

    public bool isGameplayOn;


    public bool isActive = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (isActive)
        {
            isGameplayOn = true;

            if (maxTime <= 0f)
            {
                maxTime = 300f;
            }

            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        while (isGameplayOn)
        {
            _timer += Time.deltaTime;

            _timerOnScreen.text = FixText(_timer);

            yield return null;
        }

        _remainingTime = maxTime - _timer;

        //onTimeZero?.Invoke();        
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
}