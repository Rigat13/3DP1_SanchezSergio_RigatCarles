using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] int numberOfObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] bool areParticles;
    List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            addObjectToPool();
        }
    }

    public GameObject enableObject(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject obj = getAvailableObject();
        pooledObjects.Remove(obj);
        pooledObjects.Add(obj);

        activateObject(obj, position, rotation, parent);
        return obj;
    }

    public GameObject getAvailableObject()
    {
        for (int i=0; i<pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return pooledObjects[0];
    }

    GameObject addObjectToPool()
    {
        GameObject obj = Instantiate(objectToPool, this.transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    void activateObject(GameObject obj, Vector3 position, Quaternion rotation, Transform parent)
    {
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.parent = parent;
        if (areParticles) { obj.GetComponent<ParticleSystem>().Play(); }
    }
}
