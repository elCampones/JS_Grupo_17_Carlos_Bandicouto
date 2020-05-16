using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPearSpawner : MonoBehaviour
{

    [SerializeField]
    private int spawnProbability = 20;

    [SerializeField]
    private float verticalDisplacement = .5f;

    public GameObject pear;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0,100);
        Debug.Log(r);
        if (r < spawnProbability){
            Instantiate(pear, transform.position + new Vector3(17.5f, 1, -17), Quaternion.identity);
        }
    }
    
}
