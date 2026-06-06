using System.Linq;
using UnityEngine;

public class DeliverySlot : MonoBehaviour, IInteractable
{


    public void ReceiveBox(PlayerMovement player)
    {
        if(AudioController.Instance) AudioController.Instance.PlaySFXAudio("CompleteAudio");
       FishBox box = player.grabbedObj.GetComponent<FishBox>();
       
        player.DropObject();
        if (OrderSystem.instance.currentOrders.Contains(box.type))
        {
            for (int i = 0; i < OrderSystem.instance.currentOrders.Count; i++)
            {
                if (OrderSystem.instance.currentOrders[i] == box.type)
                {
                    box.scoreValue = Mathf.RoundToInt(box.scoreValue * 1.5f);
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
        FloatingNumbers.instance.CreateFloatingNumber(box.scoreValue, transform.position, Color.green);
        // ScoreManager.instance.AddScore(box.scoreValue);
        Destroy(box.gameObject);
    }
    public void Interact(PlayerMovement player) => ReceiveBox(player);
}
