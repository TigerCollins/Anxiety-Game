using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextPopup
{
    public class TextPopupController : MonoBehaviour
    {
        [SerializeField]
        private BlackBorder blackBorder;
        [SerializeField]
        private PlayerController playerScript;

        [TextArea(1, 3)]
        public List<string> partyDialogue;
        public List<bool> partyDialogueTriggered;
        public GameObject textPrefab;
        [SerializeField]
        private int debugTimesTriggered;

        [Header("Popup Variables")]
        [Tooltip("Higher is shorter.")]
        public float canvasTransitionTime;
        public float timeBeforeTextAppears = 0.5f;


        [Header("Spawnpoint Variables")]
        [SerializeField]
        private List<Transform> spawnpoint;
        [SerializeField]
        private int spawnPointInt;
        [SerializeField]
        private int spawnpointsCompleted;

        [Header("Spawnpoint Variables")]
        public int peopleSpokenTo;
        [Tooltip("Sets the max amount of text objects spawnable. Set to -1 for infinite")]
        public int maxTextCount;
        [SerializeField]
        private int currentTextCount;


        public void ChooseSpawn()
        {
            if (currentTextCount < maxTextCount)
            {
                //Run the following code if the spawn points have all been ran through
                if (spawnpointsCompleted < partyDialogueTriggered.Count)
                {
                    //Chooses a random spawn point when function is called
                    int potentialSpawn = Random.Range(0, partyDialogueTriggered.Count);


                    //Checks if the dialogue that may be called has already been called
                    if (partyDialogueTriggered[potentialSpawn] == false)
                    {
                        spawnPointInt = potentialSpawn;
                    }


                    //If it's already been called, restart function
                    else
                    {
                        ChooseSpawn();
                    }

                }

                else
                {

                    Debug.LogWarning("Already ran through all of the possible spawnpoints - Attempting Reset...");
                    ResetDialogueCycle();
                }
            }

        }
       
        public void ResetDialogueCycle()
        {

            for (int i = 0; i < partyDialogueTriggered.Count; i++)
            {
              
                partyDialogueTriggered[i] = false;
            }
            peopleSpokenTo = 0;
          
            spawnpointsCompleted = 0;
            DebugTestAll();
        }

        void DebugTestAll()
        {
            Debug.Log("people spoken to: "+peopleSpokenTo);
            Debug.Log("completed spawns: "+ spawnpointsCompleted);
        }

        public void SpawnText(bool mouseUsed)
        {
            /*DUE TO A BUG WITH UNITY'S NEW INPUT SYSTEM, 
             DEBUGTIMESTRIGGERED HAS TO BE USED TO TRACK 
             EACH TIME THE INPUT IS TRIGGERED */

            //If the UI button is pressed, skip the multiple input check
            if(mouseUsed)
            {
                SpawnTextPrefab();
            }

            //Normal input is press, multiple input check
            else
            {
                //If the input for the function has been triggered 0 times, it'll instantiate the prefab...
                if (debugTimesTriggered < 1)
                {
                    SpawnTextPrefab();

                }
                //Records how many times the input for this function was triggered
                debugTimesTriggered++;

                //If the new input system has been triggered 3 times, reset to 0...
                if (debugTimesTriggered >= 3)
                {
                    debugTimesTriggered = 0;
                }
            }
          

        }
        private void SpawnTextPrefab()
        {
            //counts all of the spawned text
            currentTextCount = 0;
            foreach (Transform item in spawnpoint)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    currentTextCount++;
                }

            }

            //
            if (currentTextCount < maxTextCount)
            {
                //Set spawn point
                ChooseSpawn();

                if (playerScript.dialogueTrigger != null)
                {

                    //Chooses a spawnpoint from the list and instantiates the text prefab, the string is decided from the ID on the dialogue trigger
                    Instantiate(textPrefab, spawnpoint[spawnPointInt]);
                    partyDialogueTriggered[spawnPointInt] = true;
                    spawnpointsCompleted++;

                    //Update the total amount of people spoken to
                    peopleSpokenTo++;


                    blackBorder.UpdatePeopleSpokenTo();
                }

            }
        }


        
    }


}

