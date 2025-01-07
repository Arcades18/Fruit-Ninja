using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    [Header("Prefab")]
    [SerializeField] private GameObject[] fruitPrefab;
    [SerializeField] private GameObject bombPrefab;

    [Header("SpawnDelay")]
    [SerializeField] private float minSpawnDelay = 0.25f;
    [SerializeField] private float maxSapwnDelay = 1f;

    [Header("Angle")]
    [SerializeField] private float minAngle = -15f;
    [SerializeField] private float maxAngle = 15f;

    [Header("Force")]
    [SerializeField] private float minForce = 18f;
    [SerializeField] private float maxForce = 22f;

    [Header("LifeTime")]
    [SerializeField] private float lifeTime = 5f;

    [Header("BombChance")]
    [Range(0,1)]
    [SerializeField] private float bombChance = 0.05f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab = fruitPrefab[Random.Range(0, fruitPrefab.Length)];

            if(Random.value < bombChance)
            {
                prefab = bombPrefab;
            }
            
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));

            GameObject fruit = Instantiate(prefab , position, rotation);
            Destroy(fruit,lifeTime);

            float force = Random.Range(minForce,maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force ,ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSapwnDelay));
        }
    }
}
