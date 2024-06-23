using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrashControl : MonoBehaviour
{

    public GameObject[] trashPrefabs;
    public BoxCollider2D gridArea;
    public float spawnInterval = 2f;
    public int maxTrashCount = 5;
    public string gameOverSceneName = "GameOver";

    private int trashCount = 0;
    private const int TrashThreshold = 10;
    private const float MinSpawnInterval = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 0f, spawnInterval);
    }

    private void SpawnTrash()
    {
        Vector3 spawnPosition = RandomizePosition();
        GameObject trash = RandomizeTrash(spawnPosition);

        trashCount++;

        if(trashCount >= TrashThreshold)
        {
            spawnInterval = Mathf.Max(MinSpawnInterval, spawnInterval - 1f);

            CancelInvoke(nameof(SpawnTrash));
            InvokeRepeating(nameof(SpawnTrash), spawnInterval, spawnInterval);

            trashCount = 0;
        }

    }
    private Vector3 RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);



        return new Vector3(x, y, 0.0f);
    }


    private GameObject RandomizeTrash(Vector3 position)
    {
        int index = Random.Range(0, trashPrefabs.Length);
        GameObject selectedTrash = trashPrefabs[index];
        return Instantiate(selectedTrash, position, Quaternion.identity);
    }

    
        
    
}
