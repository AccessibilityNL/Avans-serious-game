using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnables;
    [SerializeField]
    private int waves = 1;
    [SerializeField]
    private int minSpawnAmount = 1;
    [SerializeField]
    private int maxSpawnAmount = 5;
    [SerializeField]
    private float timeBetweenSpawns = 3f;
    [SerializeField]
    private UnityEvent onDoneSpawning;
    [SerializeField]
    private float minRotationalStart = 1f;
    [SerializeField]
    private float maxRotationalStart = 10f;


    private List<string> spawnedFruits;

    void Start()
    {
        if (minSpawnAmount > maxSpawnAmount)
            Debug.LogError("min spawn amount is more then max spawn amount");
        spawnedFruits = new List<string>();
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        var i = 0;
        while (i < waves)
        {
            var min = transform.position - (transform.localScale / 2);
            var max = transform.position + (transform.localScale / 2);
            var spawnAmount = UnityEngine.Random.Range(minSpawnAmount, maxSpawnAmount);
            for (var j = 0; j < spawnAmount; j++)
            {
                var x = UnityEngine.Random.Range(min.x, max.x);
                var y = UnityEngine.Random.Range(min.y, max.y);
                var z = UnityEngine.Random.Range(min.z, max.z);
                var fruit = spawnables[UnityEngine.Random.Range(0, spawnables.Length)];
                var spawnedFruit = Instantiate(fruit, new Vector3(x, y, z), Quaternion.identity);
                spawnedFruits.Add(fruit.name.Replace("Bouncy", ""));
                var rigidBody = spawnedFruit.GetComponent<Rigidbody>();
                if (rigidBody)
                {
                    var rotX = UnityEngine.Random.Range(minRotationalStart, maxRotationalStart);
                    var rotY = UnityEngine.Random.Range(minRotationalStart, maxRotationalStart);
                    var rotZ = UnityEngine.Random.Range(minRotationalStart, maxRotationalStart);
                    rigidBody.angularVelocity = new Vector3(rotX, rotY, rotZ);
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.1f));
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
            i++;
        }

        onDoneSpawning.Invoke();
    }

    public int GetAmountSpawned()
    {
        return spawnedFruits.Count;
    }

    public int GetAmountSpawnedOf(string name)
    {
        var amount = 0;
        spawnedFruits.ForEach(s =>
        {
            if (s.Equals(name))
                amount++;
        });
        return amount;
    }
}
