using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPopup;

public class BlackBorder : MonoBehaviour
{
    [SerializeField]
    private TextPopupController textPopupController;
    public GameObject blackBorderObject;

    [Header("Size settings")]
    [SerializeField]
    private int peopleSpokenTo;
    [SerializeField]
    private Vector3[] borderPositions;
    [SerializeField]
    private Vector3[] borderScales;

    // Start is called before the first frame update
    void Start()
    {
        ChangeBorderSizing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePeopleSpokenTo()
    {
        peopleSpokenTo = textPopupController.peopleSpokenTo;
        ChangeBorderSizing();
    }

    public void ChangeBorderSizing()
    {
        if(borderPositions!=null)
        {
            blackBorderObject.transform.position = borderPositions[peopleSpokenTo];

        }

        if(borderScales !=null)
        {
            blackBorderObject.transform.localScale = borderScales[peopleSpokenTo];
        }

    }
}
