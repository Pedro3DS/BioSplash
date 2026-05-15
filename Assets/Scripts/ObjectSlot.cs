using UnityEngine;

public class ObjectSlot : MonoBehaviour, IInteractable
{
    [SerializeField] Transform objPoint;
    public GameObject maleFish, femaleFish;
    private bool hasObj;
    public Biome tankBiome;

   void ReceiveObject(PlayerMovement player)
    {
        if (player.hasObject)
        {
            
            var data = player.grabbedObj.GetComponent<Fish>()?.fishData;
            if (tankBiome == data.biome || tankBiome == Biome.none)
            {
                tankBiome = data.biome;
                switch (data.gender)
                {
                    case Gender.M:
                        maleFish = player.grabbedObj;                      
                        player.grabbedObj = null;
                        player.hasObject = false;
                        maleFish.transform.parent = objPoint;
                        maleFish.transform.position = objPoint.position;                      
                        maleFish.GetComponent<CarryableObj>().captor = this.gameObject;
                        break;
                    case Gender.F:
                        femaleFish = player.grabbedObj;
                        player.grabbedObj = null;
                        player.hasObject = false;
                        femaleFish.transform.parent = objPoint;
                        femaleFish.transform.position = objPoint.position;                       
                        femaleFish.GetComponent<CarryableObj>().captor = this.gameObject;
                        break;

                }
                if (maleFish != null && femaleFish != null)
                {
                    maleFish.GetComponent<CarryableObj>().onSlot = true;
                    femaleFish.GetComponent<CarryableObj>().onSlot = true;
                    hasObj = true;
                }

            }
        }
    }

    public void LetGo()
    {
        if (maleFish != null && femaleFish == null)
        {
            maleFish = null;
            hasObj = false;
        }
        else if (maleFish == null && femaleFish != null)
        {
            femaleFish = null;
            hasObj = false;
        }
        

    }
    public void Interact(PlayerMovement player) => ReceiveObject(player);
}
