using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FishGenerator : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] GameObject fishPrefab;
    public List<Fishdata> possibleFish;
    public float minTimer = 4, maxTimer = 7;
    private bool timerOn;
    private float timer;
    private bool hasFish;
    private CarryableObj currentFish;

    private void Update()
    {
        if (timerOn)
        {
            if (!hasFish)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    GameObject obj = Instantiate(fishPrefab, spawnPoint);
                    obj.transform.position = spawnPoint.position;
                    currentFish = obj.GetComponent<CarryableObj>();
                    currentFish.gameObject.GetComponent<Fish>().Begin(possibleFish[Random.Range(0,possibleFish.Count)]);
                    currentFish.captor = this.gameObject;
                    hasFish = true;
                }
            }
        } else
        {
            timerOn = true;
            timer = Random.Range(minTimer, maxTimer);
        }
    }

    public void LetSpawnGo()
    {
        hasFish = false;
        timerOn = false;
    }

}
