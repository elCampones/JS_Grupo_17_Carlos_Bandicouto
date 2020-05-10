using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    [SerializeField]
    private CinemachineBrain cmb;

    [SerializeField]
    public CinemachineFreeLook newCam;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" &&
            cmb.ActiveVirtualCamera.VirtualCameraGameObject.gameObject != newCam.VirtualCameraGameObject.gameObject)
        {
            cmb.ActiveVirtualCamera.Priority = 10;
            newCam.Priority = 15;
        }
    }
}
