using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : Plane
{
    [Inject] private BulletPool bulletPool;
    [Inject] private SignalBus signalBus;
    [Inject] private UI_Controller ui_Controller;

    private Rigidbody rb;
    private Vector2 moveDirection;
    private int kills;
    public int Kills => kills;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        signalBus.Subscribe<EnemiePlaneIsDead>(() =>
        {
            kills++;
            ui_Controller.UpdateKillsEnumerator();
        });

        SetStartParams();
    }

    private void SetStartParams()
    {
        healthPoint = config.PlayerHP;
    }

    public void MoveHorizontal(int direction)
    {
        if ((direction < 0 && transform.position.x <= config.HorizontalBounds.MinValue) || 
            (direction > 0 && transform.position.x >= config.HorizontalBounds.MaxValue))
        {
            moveDirection.x = 0;
            return;
        }
        
        moveDirection.x = config.PlayerSpeed * direction;
    }

    public void MoveVertical(int direction)
    {
        if ((direction < 0 && transform.position.y <= config.VerticalBounds.MinValue) ||
            (direction < 0 && transform.position.y <= config.VerticalBounds.MinValue))
        {
            moveDirection.y = 0;
            return;
        }

        moveDirection.y = config.PlayerSpeed * direction;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void StartAttack(bool isAttack)
    {
        if (isAttack)
            InvokeRepeating("Fire", 0, 0.5f);
        else
            CancelInvoke("Fire");
    }

    private void Fire()
    {
        var bullet = bulletPool.GetBullet();
        bullet.transform.position = transform.position + Vector3.forward * 5;
        bullet.transform.eulerAngles = new Vector3(-90, 0, 0);
        bullet.GetComponent<BulletControl>().TargetTag = "Enemie";
        bullet.SetActive(true);
        bullet.GetComponent<BulletControl>().damage = UnityEngine.Random.Range(config.PlayerDamage.MinValue, config.PlayerDamage.MaxValue);
    }

    private void Movement()
    {
        rb.velocity = moveDirection;
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, config.HorizontalBounds.MinValue, config.HorizontalBounds.MaxValue),
            Mathf.Clamp(transform.position.y, config.VerticalBounds.MinValue, config.VerticalBounds.MaxValue));
    }

    private bool PlayerInMoveZone()
    {
        return transform.position.x < config.HorizontalBounds.MaxValue &&
                     transform.position.x > config.HorizontalBounds.MinValue &&
                     transform.position.y < config.VerticalBounds.MaxValue &&
                     transform.position.x > config.VerticalBounds.MinValue;
    }
}
