using UnityEngine;
using Panda;
using Zenject;

public class WavesControllerEvents : MonoBehaviour
{
    [Inject] private PlanesPool planesPool;
    [Inject] private LvlConfig config;
    [Inject] private SignalBus signalBus;
    [Inject] private PlayerControl player;

    private bool gameStarted;

    public void StartGame()
    {
        signalBus.Fire<GameStarted>();
        gameStarted = true;
    }

    [Task]
    private void WaitToStart()
    {
        if (gameStarted)
            Task.current.Succeed();
    }

    [Task]
    private void AddPlaneFaceToPlayer()
    {
        var plane = planesPool.GetPlane();
        plane.transform.position = config.GetPlaneStartPosition();
        plane.transform.LookAt(player.transform);
        Task.current.Succeed();
    }

    [Task]
    private void AddPlaneBackToPlayer()
    {
        var plane = planesPool.GetPlane();
        plane.transform.position = config.GetPlaneStartPosition();
        plane.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Task.current.Succeed();
    }

    [Task]
    private void InstanceBossPlane()
    {

    }
}
