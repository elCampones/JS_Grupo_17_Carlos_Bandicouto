using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLockAxis : MonoBehaviour
{

    [SerializeField]
    private Transform player = null;

    [SerializeField]
    private int axisIndex = 0;

    [SerializeField]
    private float xOffset = 0;

    [SerializeField]
    private float yOffset = 0;

    [SerializeField]
    private float zOffset = 0;

    private int[] follow = new int[3];

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < follow.Length; i++)
            if (axisIndex != i)
                follow[i] = 1;
    }

    private void FixedUpdate() => transform.position = new Vector3(follow[0] * player.position.x + (1 - follow[0]) * transform.position.x + xOffset,
            follow[1] * player.position.y + (1 - follow[1]) * transform.position.y + yOffset, 
            follow[2] * player.position.z + (1 - follow[2]) * transform.position.z + zOffset);

}
