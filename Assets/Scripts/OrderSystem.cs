using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{
    public static OrderSystem instance;
    public GameObject orderPrefab;
    public int quantity;
    public List<Fishdata> ordersFish;
    public List<FishType> currentOrders;
    public List<GameObject> objectOrders;
    public bool ordersComplete;

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
        for (int i = 0; i < quantity; i++)
        {
            GameObject go = Instantiate(orderPrefab, this.gameObject.transform);
            Image[] img = go.GetComponentsInChildren<Image>();
            TextMeshProUGUI fihName =  go.GetComponentInChildren<TextMeshProUGUI>();
            Fishdata fish = ordersFish[Random.Range(0,ordersFish.Count)];
            img[1].sprite = fish.fishImage;
            fihName.text = fish.fishName;
            ordersFish.Remove(fish);
            currentOrders.Add(fish.type);
            objectOrders.Add(go);
        }
    }
}
