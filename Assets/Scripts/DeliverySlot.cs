using System.Linq;
using UnityEngine;

public class DeliverySlot : MonoBehaviour, IInteractable
{


    public void ReceiveBox(PlayerMovement player)
    {
       FishBox box = player.grabbedObj.GetComponent<FishBox>();
       ScoreManager.instance.AddScore(box.scoreValue);
        player.DropObject();
        if (OrderSystem.instance.currentOrders.Contains(box.type))
        {
            for (int i = 0; i < OrderSystem.instance.currentOrders.Count; i++)
            {
                if (OrderSystem.instance.currentOrders[i] == box.type)
                { 
                    OrderSystem.instance.currentOrders.RemoveAt(i);
                    Destroy(OrderSystem.instance.objectOrders[i]);
                    OrderSystem.instance.objectOrders.RemoveAt(i);
                    if(OrderSystem.instance.currentOrders.Count == 0)
                    {
                        Debug.Log("Tudo completo");
                        OrderSystem.instance.ordersComplete = true;
                    }
                }
            }
        }
        Destroy(box.gameObject);
    }
    public void Interact(PlayerMovement player) => ReceiveBox(player);
}
