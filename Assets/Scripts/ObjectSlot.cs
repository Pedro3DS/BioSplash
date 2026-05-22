using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSlot : MonoBehaviour, IInteractable
{
    [SerializeField] Transform objPoint, subPoint1, subPoint2;
    [SerializeField] Slider slider;   
    public GameObject maleFish, femaleFish;
    public bool hasObj;
    public bool isReproduct;
    public FishType tankBiome;
    public float fishRotateSpeed = 50;
    private float sliderTimerrr;
    public float sliderTime;
    public bool fishReady;

    private void Start()
    {
        if (isReproduct)
        {
            slider.maxValue = sliderTime;
        }
    }

    private void Update()
    {
        if (maleFish != null || femaleFish != null)
        {
            objPoint.RotateAround(objPoint.position, new Vector3(0,1,0), fishRotateSpeed * Time.deltaTime);
        }
        if (maleFish != null && femaleFish != null && isReproduct && !fishReady)
        {

            sliderTimerrr += Time.deltaTime;
            slider.value = sliderTimerrr;
            if (slider.value == slider.maxValue)
            {
                fishReady = true;
                femaleFish.gameObject.GetComponent<CarryableObj>().isHeld = true;
                maleFish.gameObject.GetComponent<CarryableObj>().isHeld = true;
            }
        }
    }
    void ReceiveObject(PlayerMovement player)
    { if (!fishReady)
        {
            if (maleFish == null || femaleFish == null)
            {
                if (player.hasObject)
                {

                    var data = player.grabbedObj.GetComponent<Fish>()?.fishData;
                    if (player.grabbedObj.GetComponent<Fish>().registered)
                    {
                        if (tankBiome == data.type || tankBiome == FishType.none)
                        {
                            tankBiome = data.type;
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
                                    maleFish.GetComponent<Fish>().resting = true;
                                }
                                else if (femaleFish == null)
                                {
                                    femaleFish = player.grabbedObj;
                                    player.grabbedObj = null;
                                    player.hasObject = false;
                                    femaleFish.transform.parent = objPoint;
                                    femaleFish.transform.position = subPoint2.position;
                                    femaleFish.GetComponent<CarryableObj>().captor = this.gameObject;
                                    femaleFish.GetComponent<Fish>().resting = true;
                                }
                            }
                        }
                    }
                }
            }
        } else
        {
            ScoreManager.instance.AddScore(maleFish.GetComponent<Fish>(), femaleFish.GetComponent<Fish>());
            Destroy(maleFish);
            maleFish = null;
            Destroy(femaleFish);
            femaleFish = null;
            hasObj = false;
            slider.value = slider.minValue;
            sliderTimerrr = slider.minValue;
            fishReady = false;
            tankBiome = FishType.none;
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
                maleFish.GetComponent<Fish>().resting = false;
                maleFish = null;              
            } if (fishie == femaleFish)
            {
                femaleFish.GetComponent<Fish>().resting = false;
                femaleFish = null;                
            }
        }
        if(maleFish == null && femaleFish == null)
        {
            tankBiome = FishType.none;
        }

    }
    public void Interact(PlayerMovement player) => ReceiveObject(player);
}
