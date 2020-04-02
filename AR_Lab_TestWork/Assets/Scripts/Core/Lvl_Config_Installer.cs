using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Lvl_Config_Installer", menuName = "Installers/Lvl_Config_Installer")]
public class Lvl_Config_Installer : ScriptableObjectInstaller<Lvl_Config_Installer>
{
    [SerializeField] private LvlConfig config;

    public override void InstallBindings()
    {
        Container.BindInstance(config);
    }
}