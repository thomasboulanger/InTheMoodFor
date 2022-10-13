using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public List<int> DemonSpawnTimer;
    public List<int> DemonOrderOfElement;
}
