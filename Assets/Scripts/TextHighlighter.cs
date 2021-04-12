using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TextHighlighter : MonoBehaviour
{
    //THIS SCRIPT IS TO BE ATTACHED TO TEXTMESHOBJECTS
    //THIS SCRIPT IS TO BE USED WITH EVENT TRIGGER (enter and move for keyboard/mouse support. point for touch)

    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject buttonObject;
    [SerializeField]
    private TextMeshProUGUI textObject;
    [SerializeField]
    private Color seletedColour;
    [SerializeField]
    private Color normalColour;
    public bool isSelected;

    public void Start()
    {
        OnMove();
    }

    public void IsSelected()
    {
        //If selected, change to selected colour

            textObject.color = seletedColour;
        
      
    }

    public void NotSelected()
    {
        //If bool is false, show the normal colour
  
            textObject.color = normalColour;
     
       
    }

    public void OnMove()
    {
        //Whenever on move is called in event trigger, check when the currently selected object is - for keyboard inut
        GameObject currentSelected = eventSystem.currentSelectedGameObject;

      //  SetSelected(eventSystem.IsPointerOverGameObject().);


        //If the object thats selected is this, set selected
        if (isSelected == true || currentSelected == buttonObject)
        {
            IsSelected();
            SetSelected(false);
        }

        else
        {
            SetSelected(true);
            NotSelected();
        }
    }

    public void SetSelected(bool newBool)
    {
        //set the bool for reading in other scripts
        isSelected = newBool;
    }

}
