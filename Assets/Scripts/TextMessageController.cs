using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMessageController : MonoBehaviour
{
    [SerializeField]
    private List<CanvasGroup> textCanvasGroup;
    [SerializeField]
    private CanvasGroup textMessageHolder;
    [SerializeField]
    private PlayerController playerController;

    private int currentTextCount;

    [Header("Timer Variables")]
    [SerializeField]
    private float timeBeforeFirstText;
    [SerializeField]
    private float timeBetweenTexts;
    [SerializeField]
    private float timeToCloseMessages;

    [Header("Transition Variables")]
    [SerializeField]
    [Tooltip("Higher is faster")]
    private float fadeSpeedMultiplier;
    [SerializeField]
    [Tooltip("Lower is slower")]
    private bool canFade;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowTextMessages");

        //Close all individualTexts
        textMessageHolder.alpha = 1;
        int tempIndex = 0;
        foreach (CanvasGroup canvasGroup in textCanvasGroup)
        {
            textCanvasGroup[tempIndex].alpha = 0;
            tempIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Fade text message grouping away if the coroutine is done and the alpha is not 0
        if(canFade == true && textMessageHolder.alpha !=0)
        {
            textMessageHolder.alpha -= Time.deltaTime * (fadeSpeedMultiplier / 10);
        }
    }

    IEnumerator ShowTextMessages()
    {
        //If text messages exist in the list...
        if(textCanvasGroup != null)
        {
            //Trigger first text message
            yield return new WaitForSeconds(timeBetweenTexts);
            textCanvasGroup[currentTextCount].alpha = 1;


            //Repeat the following lines for each text message to emulate a convo...
            foreach (CanvasGroup canvasGroup in textCanvasGroup)
            {
                //Pause coroutine between each texts for 'x' time
                yield return new WaitForSeconds(timeBetweenTexts);

                //Make the text message visible via canvas group
                textCanvasGroup[currentTextCount].alpha = 1;

                //increases int that decides the text message ID
                currentTextCount++;
            }

            //Close text message 'app' after x seconds...
            yield return new WaitForSeconds(timeToCloseMessages);
            canFade = true;
            textMessageHolder.blocksRaycasts = false;
            playerController.textMessageOver = true;
        }

    }
}
