using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour
{
    [Inject] private LvlConfig config;
    [Inject] private DiContainer container;
    public int damage;
    public string TargetTag = "Enemie";

    private Rigidbody rb;

    private void Awake()
    {
        container.Inject(this);
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * config.BulletVelocity;
    }

    private void OnEnable()
    {
        StartCoroutine("DestroyTimer");
    }

    private void OnDisable()
    {
        StopCoroutine("DestroyTimer");
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(config.BulletLifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TargetTag))
        {
            other.GetComponent<Plane>().ApplyDamage(damage);
        }
    }
}
