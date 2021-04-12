using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    [Header("Paused Game")]
    [SerializeField]
    private Animator pauseMenuAnimator;
    // Start is called before the first frame update
   

    //Changesc pause menu bool within the animator. To be used with the Unity inspector
    public void ChangePauseMenu(bool newBool)
    {
        pauseMenuAnimator.SetBool("isOpen", newBool);
    }
}
