using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPopup;

public class BlackBorder : MonoBehaviour
{
    [SerializeField]
    private TextPopupController textPopupController;
    public GameObject blackBorderObject;
    [SerializeField]
    private GameObject cameraBlock;
    

    [Header("Size Settings")]
    [SerializeField]
    private int peopleSpokenTo;
    [SerializeField]
    private CanvasGroup[] blackBorderCanvasGroup;
    [SerializeField]
    private CanvasGroup redCanvasGroup;

    [Header("Shake Settings")]
    [SerializeField]
    private float timeBeforeShakeStart;
    [SerializeField]
    private TraumaInducer traumaInducer;
    [SerializeField]
    private float shakeTime;
    [SerializeField]
    private float shakeStrength;


    [Header("Timing Settings")]
    [SerializeField]
    private float redFlashTime;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeBorderSizing();
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
        StartCoroutine("BlackBorderTransition");


    }

    IEnumerator BlackBorderTransition()
    {
        
        redCanvasGroup.alpha = 1;
        cameraBlock.SetActive(true);
        yield return new WaitForSeconds(redFlashTime);
        cameraBlock.SetActive(false);
        redCanvasGroup.alpha = 0;
        yield return new WaitForSeconds(timeBeforeShakeStart);
        StartCoroutine(traumaInducer.Start());

        //UpdatePeopleSpokenTo();

        //If everyones been spoken to
        if (peopleSpokenTo > blackBorderCanvasGroup.Length - 1)
        {    
            blackBorderCanvasGroup[peopleSpokenTo].alpha = 1;
            blackBorderCanvasGroup[peopleSpokenTo - 1].alpha = 0;
        }

       
       
    }
}
