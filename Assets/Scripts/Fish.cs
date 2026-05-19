using UnityEngine;

public class Fish : MonoBehaviour
{
    public Fishdata fishData;
    private  GameObject genderImage;

    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private MeshFilter fishMesh;

    public void Begin(Fishdata data)
    {
        fishData = data;
        Debug.Log((int)fishData.gender);
      genderImage =  Instantiate(SpriteManager.instance.genderSprites[((int)fishData.gender)], infoCanvas.transform);
        fishMesh.mesh = fishData.mesh;
    }

    private void Update()
    {
        if (genderImage != null)
        {
            genderImage.transform.rotation = Quaternion.LookRotation(genderImage.transform.position - Camera.main.transform.position);
        }
    }
}
