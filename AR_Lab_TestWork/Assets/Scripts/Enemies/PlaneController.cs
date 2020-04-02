using UnityEngine;
using Panda;
using Zenject;

public class PlaneController : Plane
{
    [Inject] private SignalBus signalBus;
    [Inject] private BulletPool bulletPool;

    private bool canAttack;
    public bool CanAttack => canAttack;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        SetStartParams();
    }

    private void SetStartParams()
    {
        healthPoint = config.PlaneHP;
    }

    [Task]
    private void GoInFight()
    {
        //anim.SetTrigger("goInFight");
        //if (InFight) Task.current.Succeed();
    }

    [Task]
    private void Attack()
    {
        var bullet = bulletPool.GetBullet();
        bullet.transform.position = transform.position + Vector3.forward;
        bullet.transform.localEulerAngles = Vector3.zero;
        bullet.GetComponent<BulletControl>().TargetTag = "Player";
        bullet.SetActive(true);
        bullet.GetComponent<BulletControl>().damage = Random.Range(config.PlayerDamage.MinValue, config.PlayerDamage.MaxValue);
    }

    [Task]
    private void EscapeFromFight()
    {
        anim.SetTrigger("outOnFight");
        Task.current.Succeed();
    }

    [Task]
    private void OutOnFight()
    {
        
    }

    public override void IsDead()
    {
        signalBus.Fire<EnemiePlaneIsDead>();
        gameObject.SetActive(false);
        ResetParams();
    }

    protected override void ResetParams()
    {
        healthPoint = config.PlaneHP;
    }
}
