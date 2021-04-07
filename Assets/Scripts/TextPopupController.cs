using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextPopup
{
    public class TextPopupController : MonoBehaviour
    {
        [SerializeField]
        private BlackBorder blackBorder;

        [TextArea(1,3)]
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

        [Header("Spawnpoint Variables")]
        public int peopleSpokenTo;


        public void SpawnText()
        {
           /*DUE TO A BUG WITH UNITY'S NEW INPUT SYSTEM, 
            DEBUGTIMESTRIGGERED HAS TO BE USED TO TRACK 
            EACH TIME THE INPUT IS TRIGGERED */

            //If the input for the function has been triggered 0 times, it'll instantiate the prefab...
            if(debugTimesTriggered < 1)
            {     

                //Chooses a spawnpoint from the list and instantiates the text prefab.
                int randomSpawn = Random.Range(0, spawnpoint.Count);
                Instantiate(textPrefab, spawnpoint[randomSpawn]);

                //Update the total amount of people spoken to
                peopleSpokenTo++;
                blackBorder.UpdatePeopleSpokenTo();
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


}

