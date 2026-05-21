using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public enum Gender { M, F }
public enum FishType {none, CascudoViola, tipo2, tipo3, tipo4 }
[CreateAssetMenu(fileName = "Fishdata", menuName = "Scriptable Objects/Fishdata")]
public class Fishdata : ScriptableObject
{
  public string fishName;
    public string scientificName;
    public string description;
    public Gender gender;
    public FishType type;
    public Sprite fishImage;
    public GameObject MeshPrefab;
}
