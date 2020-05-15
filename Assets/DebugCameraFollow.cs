
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
   
   public GameObject pauseMenuUI;

   

   public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;
 
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;
 

    void Start ()
	{
		transform.position = player.position + offset;
	}
    
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.unscaledDeltaTime;
		rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.unscaledDeltaTime;
		rotationY = Mathf.Clamp (rotationY, -90, 90);
 
		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
 
	 	if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
	 	{
			transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.unscaledDeltaTime;
			transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.unscaledDeltaTime;
	 	}
	 	else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
	 	{
			transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.unscaledDeltaTime;
			transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.unscaledDeltaTime;
	 	}
	 	else
	 	{
	 		transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.unscaledDeltaTime;
			transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.unscaledDeltaTime;
	 	}
 
 
		if (Input.GetKey (KeyCode.Q)) {transform.position += transform.up * climbSpeed * Time.unscaledDeltaTime;}
		if (Input.GetKey (KeyCode.E)) {transform.position -= transform.up * climbSpeed * Time.unscaledDeltaTime;}
 
		/*if (Input.GetKeyDown (KeyCode.End))
		{
			Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
		}*/
        
            
        
    }

    
    
}
