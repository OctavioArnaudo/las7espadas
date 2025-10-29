using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InitSpawn : InitSlider
{
    protected List<GameObject> poolList = new List<GameObject>();
    public event Action<GameObject> OnObjectSpawned;

    public void SpawnPrefab(GameObject prefab)
    {
        for (int i = 0; i < spawnSize; i++)
        {
            RestartCoroutine(AssignPrefab(prefab));
        }
    }
    private IEnumerator AssignPrefab(GameObject prefab)
    {
        yield return WaitFor(spawnDelay);

        spawnDistance = Vector3.Distance(prefab.transform.position, transform.position);

        foreach (GameObject obj in poolList)
        {
            if (!obj.activeInHierarchy)
                OnObjectSpawned?.Invoke(obj);
        }

        GameObject instance = Instantiate(prefab);
        instance.SetActive(false);
        poolList.Add(instance);

        OnObjectSpawned?.Invoke(instance);
    }

    protected override void Start()
    {
        base.Start();
        OnObjectSpawned += HandleSpawnedObject;
    }

    private void KillSpawnedObject(GameObject instance)
    {
        OnObjectSpawned += KillSpawnedObject;
        RestartCoroutine(WaitFor(lifetime));
        instance.SetActive(false);
    }
    private void HandleSpawnedObject(GameObject instance)
    {
        instance.transform.position = gameObject.transform.position;
        instance.transform.rotation = gameObject.transform.rotation;
        instance.SetActive(true);
    }
}
