using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPopup;

public class DialogueTrigger : MonoBehaviour
{
    public TextPopupController textPopupController;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void StartTextPopup()
    {
        textPopupController.SpawnText();
    }
}
