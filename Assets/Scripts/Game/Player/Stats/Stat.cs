using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Configs/Game/Stat", order = 0)]
public class Stat: ScriptableObject
{
    [field:SerializeField] public int ID { get; set; }
    [field:SerializeField] public StatLevel[] Levels { get; set; }
}