using UnityEditor;
using UnityEngine;

public class CarryableObj : MonoBehaviour, IInteractable
{
    public bool isHeld;
    public bool onSlot;
    public GameObject captor;
    void GotInteracted(PlayerMovement player)
    {
        if (!onSlot)
        {
            if (!player.hasObject)
            {
                if (captor != null)
                {
                    captor.GetComponent<ObjectSlot>()?.LetGo(this.gameObject);
                    captor.GetComponent<PlayerMovement>()?.DropObject();
                    captor.GetComponent<FishGenerator>()?.LetSpawnGo();
                }

                player.GrabObject(this.gameObject);
                captor = player.gameObject;
            }
        }
            
    }

    public void Interact(PlayerMovement player) => GotInteracted(player);
}
