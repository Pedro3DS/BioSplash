using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    public List<int> fishCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        while (fishCount.Count < 20) fishCount.Add(0);
    }
}
