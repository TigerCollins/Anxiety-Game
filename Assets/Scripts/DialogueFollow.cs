using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueHolder;
    [SerializeField]
    private Transform dialogueTransform;
    [SerializeField]
    private Transform textTarget;


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
        dialogueHolder.transform.position = Vector3.SmoothDamp(dialogueHolder.transform.position, new Vector3(textTarget.position.x + xOffset, textTarget.position.y + yOffset,textTarget.position.z + zOffset), ref velocity, cameraDelay);
    }
}
