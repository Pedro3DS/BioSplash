using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerSystem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject fishCanvas, fishCanvas2;
    [SerializeField] TextMeshProUGUI fishName , fishName2;
    [SerializeField] TextMeshProUGUI scienceName , scienceName2;
    [SerializeField] TextMeshProUGUI biomes, biomes2;
    [SerializeField] Image fishGender, fishGender2;
    [SerializeField] Image fishPicture , fishPicture2;
    [SerializeField] AudioSource audioSource;
    private bool isBusy, isBusy2;



    public void IdentifyFish(PlayerMovement player)
    {
        if (player.hasObject)
        {
            if (!isBusy && player.gameObject == PlayerManager.Instance.p1)
            {
                Debug.Log("1");
                StartCoroutine(ImageWait(player));
            } else if (!isBusy2 && player.gameObject == PlayerManager.Instance.p2)
            {
                Debug.Log("2");
                StartCoroutine(ImageWait2(player));
            }

        }
    }




    private IEnumerator ImageWait(PlayerMovement player)
    {
        isBusy = true;
        player.movementDisabled = true;
        fishCanvas.SetActive(true);
        audioSource.Play();
        var data = player.grabbedObj.GetComponent<Fish>()?.fishData;
        fishName.text = data.fishName;
        scienceName.text = data.scientificName;
        biomes.text = data.description;
        fishGender.sprite = SpriteManager.instance.genderSprites[(int)data.gender];
        fishPicture.sprite = data.fishImage;
        player.grabbedObj.GetComponent<Fish>().Register();
        yield return new WaitForSeconds(2);
        player.movementDisabled = false;
        yield return new WaitForSeconds(3);
        fishCanvas.SetActive(false);
        isBusy = false;
        
    }
    private IEnumerator ImageWait2(PlayerMovement player)
    {
        isBusy2 = true;
        player.movementDisabled = true;
        fishCanvas2.SetActive(true);
        audioSource.Play();
        var data = player.grabbedObj.GetComponent<Fish>()?.fishData;
        fishName2.text = data.fishName;
        scienceName2.text = data.scientificName;
        biomes2.text = data.description;
        fishGender2.sprite = SpriteManager.instance.genderSprites[(int)data.gender];
        fishPicture2.sprite = data.fishImage;
        player.grabbedObj.GetComponent<Fish>().Register();
        yield return new WaitForSeconds(2);
        player.movementDisabled = false;
        yield return new WaitForSeconds(3);
        fishCanvas2.SetActive(false);
        isBusy2 = false;

    }

    public void Interact(PlayerMovement player) => IdentifyFish(player);
}
