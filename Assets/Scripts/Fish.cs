using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Fishdata fishData;
    private  GameObject genderImage;
    private GameObject registImage;
    public bool registered;
    public int baseScore = 5;
    public float freshnessLeeway = 15f;
    public float mult = 0.75f;
    private float timer;
    public bool resting;

    [SerializeField] private GameObject infoCanvas;

    public void Begin(Fishdata data)
    {
        fishData = data;
        Debug.Log((int)fishData.type);
      genderImage =  Instantiate(SpriteManager.instance.genderPrefabs[((int)fishData.gender)], infoCanvas.transform);
        registImage = Instantiate(SpriteManager.instance.registerPrefabs[0], infoCanvas.transform);
       GameObject fishMesh = Instantiate(data.MeshPrefab, this.gameObject.transform);
        fishMesh.transform.position = this.gameObject.transform.position;
        timer = freshnessLeeway;
    }

    private void Update()
    {
        if (!resting)
        {


            if (mult > 0.4f)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    //  Debug.Log(timer);
                }
                else
                {
                    mult -= Time.deltaTime * 0.05f;
                    //  Debug.Log(mult);
                }
            }
        }
        if (genderImage != null && registImage != null)
        {
            genderImage.transform.rotation = Quaternion.LookRotation(genderImage.transform.position - Camera.main.transform.position);
            registImage.transform.rotation = Quaternion.LookRotation(genderImage.transform.position - Camera.main.transform.position);
        }
    }

    public void Register()
    {
        if (!registered)
        {
            registered = true;
            Destroy(registImage);
            registImage = Instantiate(SpriteManager.instance.registerPrefabs[1], infoCanvas.transform);
            if (timer <= 0)
            {
                ScoreManager.instance.AddScore(1);
            }
            else
            {
                ScoreManager.instance.AddScore(2);
            }
        }
    }
}
