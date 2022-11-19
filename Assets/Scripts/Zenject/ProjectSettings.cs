using UnityEngine;

namespace Zenject
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Configs/Zenject/ProjectSettings", order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        [SerializeField] private int targetFPS = 60;
        [SerializeField] private bool useMultitouch=false;

        public int TargetFPS => targetFPS;
        public bool UseMultitouch => useMultitouch;
    }
}