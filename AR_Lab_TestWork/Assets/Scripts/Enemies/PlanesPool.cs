using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlanesPool : MonoBehaviour
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
            InstanceNewPlane();
        }
    }

    private GameObject InstanceNewPlane()
    {
        var planeInstance = container.InstantiatePrefab(poolParams.planePref, parentObj.transform);
        pool.Add(planeInstance);
        planeInstance.SetActive(false);
        return planeInstance;
    }

    public GameObject GetPlane()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        return InstanceNewPlane();
    }

    private void SetParentObj()
    {
        parentObj = GameObject.Find("PlanePool");
        if (parentObj == null)
        {
            parentObj = Instantiate(new GameObject("PlanePool"));
        }
    }

    [System.Serializable]
    private struct PoolParam
    {
        public GameObject planePref;
        public int size;
    }
}
