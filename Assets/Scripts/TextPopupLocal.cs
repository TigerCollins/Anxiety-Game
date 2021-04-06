using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPopup;
using UnityEngine.UI;

public class TextPopupLocal : MonoBehaviour
{
    [SerializeField]
    private TextPopupController textPopupController;
    [SerializeField]
    private int targetStringID;
    [SerializeField]
    private TextMesh textObject;
    [SerializeField]
    private float elapsedTime;
    private bool hasTriggered;
    private Color tempColour;
  


    // Start is called before the first frame update
    void Start()
    {

        //Finds the text controller via FindObject - better optimised than 'GameObject.Find'
        textPopupController = FindObjectOfType<TextPopupController>();

        //Sets the text colour to be invisible
        tempColour.a = 0;
        textObject.color = tempColour;

        GenerateNewStringID();
        UpdateString();
        StartCoroutine("ActivateCanvasGroup");
    }

    public void GenerateNewStringID()
    {
        //Generates an ID for which string to pull from in a list
        targetStringID = Random.Range(0, textPopupController.partyDialogue.Count);
    }

    public void UpdateString()
    {
        //Sets the text string to a string in the list with the corresponding ID
        textObject.text = textPopupController.partyDialogue[targetStringID];
    }


    IEnumerator ActivateCanvasGroup()
    {


        //Waits for 'x' amount of seconds before continiuing
        yield return new WaitForSeconds(textPopupController.timeBeforeTextAppears);

        //Changes 'hasTriggered' bool when the buffer time is finished
        hasTriggered = true;
        

    }

    public void MakeCanvasGroupOpaque()
    {

        //If the text can be made visible but isn't yet fully opaque
        if (hasTriggered == true && textObject.color.a != 255)
        {
            //Lerps text colour from 0 alpha to 255 alpha
            tempColour.a += Time.fixedDeltaTime * textPopupController.canvasTransitionTime;
            textObject.color = tempColour;
        }
    }

    public void FixedUpdate()
    {
        MakeCanvasGroupOpaque();
    }
       

}
