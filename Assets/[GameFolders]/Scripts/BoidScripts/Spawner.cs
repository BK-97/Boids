using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Boid prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public Color colour;
    private ChanceCalculator chanceCalculate;
    public ChanceCalculator ChanceCalculate { get { return (chanceCalculate == null) ? chanceCalculate = GetComponent<ChanceCalculator>() : chanceCalculate; } }
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float randomChance = Random.Range(0f, 1f);

            float cumulativeChance = 0f;
            FishTypes selectedFishType = FishTypes.type1; 

            foreach (FishTypes fishType in FishTypes.GetValues(typeof(FishTypes)))
            {
                float chance = ChanceCalculate.GetChance((int)fishType);

                if (randomChance <= cumulativeChance + chance)
                {
                    selectedFishType = fishType;
                    break;
                }

                cumulativeChance += chance;
            }

            SpawnFishBoid(selectedFishType);
        }
        BoidManager.Instance.Instantiate();

    }

    public void SpawnFishBoid(FishTypes fishType)
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
        Boid boid = Instantiate(prefab);
        boid.transform.position = pos;
        boid.transform.forward = Random.insideUnitSphere;
        boid.gameObject.GetComponent<FishModel>().SetModel(fishType);
        BoidManager.Instance.AddBoid(boid);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
}