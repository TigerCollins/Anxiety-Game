using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCamera : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.worldCamera = FindObjectOfType<Camera>();
    }

}
