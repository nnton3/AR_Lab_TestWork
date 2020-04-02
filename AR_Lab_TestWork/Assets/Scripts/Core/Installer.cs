using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameStarted>();
        Container.DeclareSignal<EnemiePlaneIsDead>();
    }
}