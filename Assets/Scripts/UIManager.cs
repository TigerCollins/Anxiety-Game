using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField]
    private UIAnimator animatorScript;
    [SerializeField]
    private PlayerController playerController;

    [Header("Game Variables")]
    public bool gamePaused;

    [Header("Touch UI")]
    [SerializeField]
    private Button leftUI;
    [SerializeField]
    private Button rightUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Mainly to be used with the unity inpsector. Sets the games pause state
    public void ChangePauseState(bool newBool)
    {
        if(newBool == true)
        {
            gamePaused = true;
            playerController.gamePaused = true;
            Time.timeScale = 0;
            leftUI.interactable = false;
            rightUI.interactable = false;
        }

        else
        {
            gamePaused = false;
            playerController.gamePaused = false;
            Time.timeScale = 1;
            leftUI.interactable = true;
            rightUI.interactable = true;
        }
    }

}
