using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Plane : MonoBehaviour
{
    [Inject] protected LvlConfig config;

    [SerializeField] protected int healthPoint;
    public UnityEvent IsDamaged;

    public int HealthPoint => healthPoint;

    public void ApplyDamage(int damage)
    {
        healthPoint -= damage;
        IsDamaged.Invoke();
        Debug.Log("is damaged");
        if (HealthPoint <= 0)
        {
            healthPoint = 0;
            IsDead();
        }
    }
    
    public virtual void IsDead()
    {
        ResetParams();
        gameObject.SetActive(false);
    }

    protected virtual void ResetParams() { }
}
