using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField] private List<GameObject> objectPool= new List<GameObject>();
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();

    private const int startingAreaCount = 4;
    private const int distanceBetweenObstacles = 20;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        int index = Random.Range(0, objectPool.Count);

        for (int i = 0; i < startingAreaCount; i++)
        {
            if (obstacles.Count < 1)
            {
                var obstaclePos = objectPool[index].transform.position;
                obstaclePos = Vector3.zero;
                obstaclePos.z += 5;

                objectPool[index].transform.position = obstaclePos;

                obstacles.Add(objectPool[index]);
                objectPool.Remove(objectPool[index]);
            }
            else
            {
                GetObjectFromPool();
            }
        }
    }

    private void GetObjectFromPool()
    {
        int index = Random.Range(0, objectPool.Count);
        do
        {
            if (!obstacles.Contains(objectPool[index]))
            {
                int lastUsedObstacleIndex = obstacles.Count - 1;

                var obstaclePos = obstacles[lastUsedObstacleIndex].transform.position;
                obstaclePos.z += distanceBetweenObstacles;

                objectPool[index].transform.position = obstaclePos;

                obstacles.Add(objectPool[index]);

                objectPool.Remove(objectPool[index]);
                break;  
            }
            index = Random.Range(0, objectPool.Count);
        } while (true);
    }

    public void ReturnToPool(GameObject obstacleArea)
    {
        if (obstacles.Contains(obstacleArea))
        {
            obstacles.Remove(obstacleArea);
            objectPool.Add(obstacleArea);
            GetObjectFromPool();
        }
    }
}