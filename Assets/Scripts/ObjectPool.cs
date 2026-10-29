using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> objectPoolList = new List<GameObject>();


    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// Instantiate new Object and add it to the pool list
    /// </summary>
    /// <returns></returns>
    private GameObject CreateNewObject()
    {
        GameObject newObject = Instantiate(objectPrefab);

        objectPoolList.Add(newObject);

        newObject.SetActive(false);

        return newObject;
    }

    public GameObject GetObjectFromPool()
    {
        GameObject theObject = objectPoolList.Find(obj => !obj.activeInHierarchy);

        if(theObject == null)
        {
            theObject = CreateNewObject();
        }

        theObject.SetActive(true);
        return theObject;
    }
}
