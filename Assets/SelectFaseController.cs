// using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct FaseCardInfos
{
    public string FaseName;
    public float FaseTime;

    public Sprite FaseSprite;

    public string Description;
}


public class SelectFaseController : MonoBehaviour
{
    [SerializeField] private FaseCardInfos[] _fasesInfos;

    public FaseCardInfos[] FasesInfos => _fasesInfos;


    [SerializeField] private Button[] _faseButtons;
}
