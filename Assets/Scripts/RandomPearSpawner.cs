using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPearSpawner : MonoBehaviour
{

    [SerializeField]
    private float spawnProbability = .2f;

    [SerializeField]
    private float verticalDisplacement = .5f;

    public GameObject pear;

    // Start is called before the first frame update
    void Start()
    {
        float r = Random.Range(0, 1);
        if (r < spawnProbability)
            Instantiate(pear, transform.position + Vector3.up * verticalDisplacement, Quaternion.identity);
    }
    
}
