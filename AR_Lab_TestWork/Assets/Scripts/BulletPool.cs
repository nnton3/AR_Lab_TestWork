using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private PoolParam poolParams;

    private List<GameObject> pool = new List<GameObject>();
    private GameObject parentObj;
    [Inject] private DiContainer container;

    private void Start()
    {
        SetParentObj();
        FillThePool();
    }

    private void FillThePool()
    {
        for (int i = 0; i < poolParams.size; i++)
        {
            InstanceNewBullet();
        }
    }

    private GameObject InstanceNewBullet()
    {
        var bulletInstance = container.InstantiatePrefab(poolParams.bulletPref, parentObj.transform);
        pool.Add(bulletInstance);
        bulletInstance.SetActive(false);
        return bulletInstance;
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
                return pool[i];
        }

        return InstanceNewBullet();
    }

    private void SetParentObj()
    {
        parentObj = GameObject.Find("BulletPool");
        if (parentObj == null)
        {
            parentObj = Instantiate(new GameObject("BulletPool"));
        }
    }

    [System.Serializable]
    private struct PoolParam
    {
        public GameObject bulletPref;
        public int size;
    }
}
