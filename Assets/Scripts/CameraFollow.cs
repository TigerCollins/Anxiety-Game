using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Camera cameraScript;
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    private Transform cameraTarget;
    [SerializeField]
    private CharacterController targetObject;

    [Header("Camera Movement Settings")]
    [Tooltip("The lower the number, the shorter the delay. 0 for instant follow.")]
    [SerializeField]
    private float cameraDelay;
    [SerializeField]
    private float xOffset;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float zOffset;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //velocity = targetObject.velocity; //current velocity
        cameraObject.transform.position = Vector3.SmoothDamp(cameraObject.transform.position, new Vector3(cameraTarget.position.x + xOffset, cameraTarget.position.y + yOffset, cameraTarget.position.z + zOffset),ref velocity, cameraDelay);
    }
}
