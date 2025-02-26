using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class LevelManager : MonoBehaviour
    {
        #region Inspector Variables

        public enum TriggerType
        {
            AirLock,
            FirstFloor,
            SecondFloor,
            ThirdFloor,
        }

        [Header("Common")] 
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject player;

        [Header("AirLock")] 
        [SerializeField] private AudioClip airLockSound;   // Background noise (loops)
        [SerializeField] private AudioClip initialDialogue; // One-time dialogue
        [SerializeField] private GameObject keyPadScreen;
        [SerializeField] private Animator airLockAnimator; // Door animator

        #endregion

        #region Public Methods

        public void TriggerZoneEntered(GameObject obj, TriggerType type)
        {
            Debug.Log($"Performing {type} action for {obj.name}");

            switch (type)
            {
                case TriggerType.AirLock:
                    StartCoroutine(PlayDialogueAndHandleAirlockSequence());
                    break;

                case TriggerType.FirstFloor:
                    AdjustPlayerSize(0.3f, 0.5f);
                    break;

                case TriggerType.SecondFloor:
                    // Add logic if needed
                    break;

                case TriggerType.ThirdFloor:
                    // Add logic if needed
                    break;
            }
        }

        public void TriggerZoneExited(GameObject obj, TriggerType type)
        {
            Debug.Log($"{obj.name} exited {type} area.");
        }

        #endregion

        #region Private Methods

        private void AdjustPlayerSize(float radius, float height)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.radius = radius;
                controller.height = height;
            }
        }

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
