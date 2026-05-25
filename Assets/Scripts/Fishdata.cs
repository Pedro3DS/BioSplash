using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public enum Gender { M, F }
public enum FishType {none, CascudoViola, Acari, Bagre, CascudoOnca, CascudoTroglobio, CiclidioAzul, Dourado, Piranha, ArraiaXingu, TetraCego }
[CreateAssetMenu(fileName = "Fishdata", menuName = "Scriptable Objects/Fishdata")]
public class Fishdata : ScriptableObject
{
    public int ID;
  public string fishName;
    public string scientificName;
    public string description;
    public Gender gender;
    public FishType type;
    public Sprite fishImage;
    public GameObject MeshPrefab;
}
