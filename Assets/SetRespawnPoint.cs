using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawnPoint : MonoBehaviour
{

    public Transform respawnPoint;

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement.respawnPoint = respawnPoint.position;
        }
    }
}
