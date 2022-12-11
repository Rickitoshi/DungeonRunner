using Game.Player;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectConfigInstaller", menuName = "Installers/ProjectConfigInstaller")]
public class ProjectConfigInstaller : ScriptableObjectInstaller<ProjectConfigInstaller>
{
    [SerializeField] private ProjectSettings projectSettings;
    [SerializeField] private PlayerConfig playerConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(projectSettings);
        Container.BindInstance(playerConfig);
    }
}