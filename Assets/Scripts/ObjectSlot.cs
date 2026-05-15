using UnityEngine;

public class ObjectSlot : MonoBehaviour, IInteractable
{
    [SerializeField] Transform objPoint;
    public GameObject heldObj;
    private bool hasObj;

   void ReceiveObject(PlayerMovement player)
    {
        if (player.hasObject)
        {
            heldObj = player.grabbedObj;
            hasObj = true;
            player.grabbedObj = null;
            player.hasObject = false;
            heldObj.transform.parent = objPoint;
            heldObj.transform.position = objPoint.position;
            heldObj.GetComponent<CarryableObj>().captor = this.gameObject;
        }
    }

    public void LetGo()
    {
        if (heldObj != null)
        {
            heldObj = null;
            hasObj = false;
        }
        

    }
    public void Interact(PlayerMovement player) => ReceiveObject(player);
}
