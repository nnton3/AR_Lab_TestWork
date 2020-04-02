using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject playerInterface;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Text killsEnumerator;

    [Inject] private LvlConfig config;
    [Inject] private SignalBus signalBus;
    [Inject] private PlayerControl player;

    private int kills;

    private void Start()
    {
        InitializePlayerInterface();
        signalBus.Subscribe<GameStarted>(EnablePlayerInterface);
        player.IsDamaged.AddListener(UpdatePlayerHealthtBar);

        EnableStartMenu();
    }

    private void EnablePlayerInterface()
    {
        startMenu.SetActive(false);
        playerInterface.SetActive(true);
    }

    private void EnableStartMenu()
    {
        playerInterface.SetActive(false);
        startMenu.SetActive(true);
    }

    private void InitializePlayerInterface()
    {
        playerHealthBar.maxValue = config.PlayerHP;
        playerHealthBar.value = playerHealthBar.maxValue;
    }

    private void UpdatePlayerHealthtBar()
    {
        playerHealthBar.value = player.HealthPoint;
    }

    public void UpdateKillsEnumerator()
    {
        killsEnumerator.text = $"Kills : {player.Kills}";
    }
}
