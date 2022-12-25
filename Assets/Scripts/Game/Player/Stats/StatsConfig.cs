using UnityEngine;

[CreateAssetMenu(fileName = "StatsConfig", menuName = "Configs/Game/StatsConfig", order = 0)]
public class StatsConfig : ScriptableObject
{
    [SerializeField] private Stat[] stats;

    public Stat[] Stats => stats;

    public Stat GetStatByName(string statsName)
    {
        foreach (var stat in stats)
        {
            if (stat.name == statsName)
            {
                return stat;
            }
        }

        return null;
    }
    
    public Stat GetStatByID(int id)
    {
        foreach (var stat in stats)
        {
            if (stat.ID == id)
            {
                return stat;
            }
        }

        return null;
    }
}