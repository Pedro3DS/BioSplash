using UnityEditor;
using UnityEngine;

public class CarryableObj : MonoBehaviour, IInteractable
{
    public bool isHeld;
    public GameObject captor;
    void GotInteracted(PlayerMovement player)
    {
        // if (!isHeld)
        //  {
        if (!player.hasObject)
        {
            if (captor != null)
            {
                captor.GetComponent<ObjectSlot>()?.LetGo();
                captor.GetComponent<PlayerMovement>()?.DropObject();
            }

            player.GrabObject(this.gameObject);
            captor = player.gameObject;
        }
            
      //  }
    }

    public void Interact(PlayerMovement player) => GotInteracted(player);
}
