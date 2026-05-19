using UnityEngine;

public class ObjectSlot : MonoBehaviour, IInteractable
{
    [SerializeField] Transform objPoint, subPoint1, subPoint2;
    public GameObject maleFish, femaleFish;
    public bool hasObj;
    public bool isReproduct;
    public Biome tankBiome;
    public float fishRotateSpeed = 50;

    private void Update()
    {
        if (maleFish != null || femaleFish != null)
        {
            objPoint.RotateAround(objPoint.position, new Vector3(0,1,0), fishRotateSpeed * Time.deltaTime);
        }
    }
    void ReceiveObject(PlayerMovement player)
    {
        if (maleFish == null || femaleFish == null)
        {
            if (player.hasObject)
            {

                var data = player.grabbedObj.GetComponent<Fish>()?.fishData;
                if (tankBiome == data.biome || tankBiome == Biome.none)
                {
                    tankBiome = data.biome;
                    if (isReproduct)
                    {
                        switch (data.gender)
                        {
                            case Gender.M:
                                maleFish = player.grabbedObj;
                                player.grabbedObj = null;
                                player.hasObject = false;
                                maleFish.transform.parent = objPoint;
                                maleFish.transform.position = subPoint1.position;
                                maleFish.GetComponent<CarryableObj>().captor = this.gameObject;
                                break;
                            case Gender.F:
                                femaleFish = player.grabbedObj;
                                player.grabbedObj = null;
                                player.hasObject = false;
                                femaleFish.transform.parent = objPoint;
                                femaleFish.transform.position = subPoint2.position;
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
                    else
                    {
                        if (maleFish == null)
                        {
                            maleFish = player.grabbedObj;
                            player.grabbedObj = null;
                            player.hasObject = false;
                            maleFish.transform.parent = objPoint;
                            maleFish.transform.position = subPoint1.position;
                            maleFish.GetComponent<CarryableObj>().captor = this.gameObject;
                        }
                        else if (femaleFish == null)
                        {
                            femaleFish = player.grabbedObj;
                            player.grabbedObj = null;
                            player.hasObject = false;
                            femaleFish.transform.parent = objPoint;
                            femaleFish.transform.position = subPoint2.position;
                            femaleFish.GetComponent<CarryableObj>().captor = this.gameObject;
                        }
                    }

                }
            }
        }
    }

    public void LetGo(GameObject fishie)
    {
        if (isReproduct)
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
        } else
        {
            if (fishie == maleFish)
            {
                maleFish = null;
            } if (fishie == femaleFish)
            {
                femaleFish = null;
            }
        }
        

    }
    public void Interact(PlayerMovement player) => ReceiveObject(player);
}
