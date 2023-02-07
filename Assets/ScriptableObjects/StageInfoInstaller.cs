using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StageInfoInstaller", menuName = "Installers/StageInfoInstaller")]
public class StageInfoInstaller : ScriptableObjectInstaller<StageInfoInstaller>
{
    public override void InstallBindings()
    {
    }
}