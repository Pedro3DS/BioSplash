using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerSystem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject fishCanvas;
    [SerializeField] TextMeshProUGUI fishName;
    [SerializeField] TextMeshProUGUI scienceName;
    [SerializeField] TextMeshProUGUI biomes;
    [SerializeField] Image fishGender;
    [SerializeField] Image fishPicture;
    [SerializeField] AudioSource audioSource;



    public void IdentifyFish(PlayerMovement player)
    {
        if (player.hasObject)
        {
            StartCoroutine(ImageWait(player));
        }
    }




    private IEnumerator ImageWait(PlayerMovement player)
    {
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
        fishCanvas.SetActive(false);
        
    }

    public void Interact(PlayerMovement player) => IdentifyFish(player);
}
