using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPopup;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public TextPopupController textPopupController;
    public int dialogueID;
 
    public bool isSelected;

    [Header("Notification Settings")]
    [SerializeField]
    private CanvasGroup dialogueIcon;
    [SerializeField]
    private float fadeMultiplier;
    [SerializeField]
    private CanvasGroup animationGroup1;
[SerializeField]
    private CanvasGroup animationGroup2;
    
    [SerializeField]
    private float timeBetweenBlinks;
    private bool hasBeenChecked;

    // Start is called before the first frame update
    void Start()
    {
   
    }



    public void TestPress(string testString)
    {
        Debug.Log(testString);
    }

    public void UpdateIcons()
    {
        if (playerController.dialogueTrigger == null)
        {
                isSelected = false;
        }

        else
        {
            //Selected dialogue trigger ID matches this ID
            if (playerController.dialogueTrigger.dialogueID == dialogueID)
            {
                isSelected = true;
            }

            //Selected dialogue trigger ID does not match
            else
            {
                isSelected = false;
            }
           
        }
    }

    public void StartTextPopup(bool mouseUsed)
    {
        textPopupController.SpawnText(mouseUsed);
    }

    public void FixedUpdate()
    {
        IconFader();
        UpdateIcons();
    }

    public void Update()
    {
        CoroutineStarter();
    }

    //Starts Coroutine for blinking
    public void CoroutineStarter()
    {
        //Checks if its already been started (RUNNING IN UPDATE, STOPS FROM MULTIPLE STARTS)
        if (hasBeenChecked == false)
        {
            //If it is not null, start coroutine
            if (isSelected)
            {
                hasBeenChecked = true;
                StartCoroutine(FlashAnimation());
            }
        }
    }

    public void IconFader()
    {
        //If the player is looking at this
        if (isSelected == true)
        {
            //If the dialogue isn't already completely opaque
            if (dialogueIcon.alpha != 1)
            {
                dialogueIcon.alpha += Time.deltaTime * fadeMultiplier;
            }

        }

        // When the player looks away
        else
        {
            //If the dialogue isn't already completely transparent
            if (dialogueIcon.alpha != 0)
            {
                dialogueIcon.alpha -= Time.deltaTime * fadeMultiplier;
            }
        }
    }

    public IEnumerator FlashAnimation()
    {
        //Phase 1 of blinks
        if(isSelected)
        {
            animationGroup1.alpha = 1;
            animationGroup2.alpha = 0;
        }

        //If the dialogue trigger is unselected, end it
        else
        {
            animationGroup1.alpha = 0;
            animationGroup2.alpha = 0;
            hasBeenChecked = false;
            yield break;
        }

        yield return new WaitForSeconds(timeBetweenBlinks);

        //Phase 2 of blinks
        if (isSelected)
        {
            animationGroup1.alpha = 0;
            animationGroup2.alpha = 1;
        }

        //If the dialogue trigger is unselected, end it
        else
        {
            animationGroup1.alpha = 0;
            animationGroup2.alpha = 0;
            hasBeenChecked = false;
            yield break;
        }
        yield return new WaitForSeconds(timeBetweenBlinks);
        //Restart
        StartCoroutine(FlashAnimation());
    }
}
