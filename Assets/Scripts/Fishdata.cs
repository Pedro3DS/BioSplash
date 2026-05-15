using UnityEngine;

public enum Gender { M, F }
public enum Biome {none, bioma1, bioma2, bioma3, bioma4 }
[CreateAssetMenu(fileName = "Fishdata", menuName = "Scriptable Objects/Fishdata")]
public class Fishdata : ScriptableObject
{
  public string fishName;
    public Gender gender;
    public Biome biome;
}
