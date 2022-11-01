using UnityEngine;

namespace Zenject
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Configs/Zenject/ProjectSettings", order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        [SerializeField] private int targetFPS = 60;

        public int TargetFPS => targetFPS;
    }
}