using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsTrap : MonoBehaviour
{
    public GameObject DropPrefab;
    public Transform dropsTrapAimer;    
    private float timeCounter;
    private float randomTimeToDrop;
    public float randomTimeToDropMax = 5;
    public
        float randomTimeToDropMin = 1;

    private void Start()
    {
        RandomRangeSpawn();
    }
    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > randomTimeToDrop)
        {
            SpawnSlime();
            RandomRangeSpawn();
        }
    }
    private void RandomRangeSpawn()
    {
        randomTimeToDrop = Random.Range(randomTimeToDropMin, randomTimeToDropMax);
    }
    private void SpawnSlime()
    {
        Instantiate(DropPrefab, dropsTrapAimer.position, dropsTrapAimer.rotation);
        timeCounter = 0;
    }
}
