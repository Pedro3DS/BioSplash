using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    public List<GameObject> genderPrefabs;
    public List<Sprite> genderSprites;
    public List<GameObject> biomeSprites;
    public List<GameObject> registerPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
