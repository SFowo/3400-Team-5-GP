using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class LevelManager : MonoBehaviour
    {
        #region Inspector Variables

        [Serializable]
        public class TriggerZone
        {
            public Collider collider;
            public TriggerType triggerType;
        }

        public enum TriggerType
        {
            AirLock,
            FirstFloor,
            SecondFloor,
            ThirdFloor,
        }

        public List<TriggerZone> triggerZones;

        [Header("Common")] 
        [SerializeField] private AudioSource audioSource;

        [Header("AirLock")] 
        [SerializeField] private AudioClip airLockSound;   // Background noise (loops)
        [SerializeField] private AudioClip initialDialogue; // One-time dialogue
        [SerializeField] private GameObject keyPadScreen;
        [SerializeField] private Animator airLockAnimator; //door animator

        #endregion

        #region Unity Methods

        private void Start()
        {
            StartCoroutine(PlayDialogueAndHandleAirlockSequence());
        }

        #endregion

        #region Trigger & Delegator

        private void OnTriggerEnter(Collider other)
        {
            foreach (var zone in triggerZones)
            {
                if (zone.collider == other)
                {
                    Debug.Log($"{other.gameObject.name} entered {zone.collider.name}");
                    PerformAction(other.gameObject, zone.triggerType);
                    break;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (var zone in triggerZones)
            {
                if (zone.collider == other)
                {
                    Debug.Log($"{other.gameObject.name} exited {zone.collider.name}");
                    break;
                }
            }
        }

        private void PerformAction(GameObject obj, TriggerType type)
        {
            Debug.Log($"Performing {type} action for {obj.name}");

            switch (type)
            {
                case TriggerType.AirLock:
                    StartCoroutine(PlayDialogueAndHandleAirlockSequence());
                    break;

                case TriggerType.FirstFloor:
                    break;

                case TriggerType.SecondFloor:
                    break;

                case TriggerType.ThirdFloor:
                    break;
            }
        }

        #endregion

        #region Airlock Handling

        private IEnumerator PlayDialogueAndHandleAirlockSequence()
        {
            // Step 1: Play both dialogue & background noise immediately
            audioSource.PlayOneShot(initialDialogue);
            audioSource.clip = airLockSound;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Playing initial dialogue & looping airlock sound...");

            // Step 2: Wait for dialogue to finish
            yield return new WaitForSeconds(initialDialogue.length);

            // Step 3: After 0.5 sec, switch keypad to green
            yield return new WaitForSeconds(0.5f);
            keyPadScreen.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Keypad turned green!");

            // Step 4: After another 0.5 sec, open the airlock door
            yield return new WaitForSeconds(0.5f);
            OpenAirLock();
            Debug.Log("Airlock door opened!");
            audioSource.Stop();
            audioSource.clip = null;
        }
        

        private void OpenAirLock()
        {
            airLockAnimator.SetBool("Open", true);
            airLockAnimator.SetBool("Close", false);
        }

        #endregion
    }
}
