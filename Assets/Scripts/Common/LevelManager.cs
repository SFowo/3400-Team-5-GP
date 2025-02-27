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
        [SerializeField] private AudioClip airLockSound;  // Background noise (loops)
        [SerializeField] private AudioClip airLockDialogueOne;
        [SerializeField] private AudioClip airLockDialogueTwo;
        [SerializeField] private GameObject keyPadScreen;
        [SerializeField] private Animator airLockAnimator; // Door animator

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Start playing airlock sequence as soon as the scene loads
            StartCoroutine(PlayAirlockSequence());
        }

        #endregion

        #region Public Methods

        public void TriggerZoneEntered(GameObject obj, TriggerType type)
        {
            Debug.Log($"Performing {type} action for {obj.name}");

            switch (type)
            {
                case TriggerType.AirLock:
                    // Airlock sequence now starts automatically at scene load, so no need to trigger it here.
                    break;

                case TriggerType.FirstFloor:
                    AdjustPlayerSize(0.3f, 0.5f);
                    player.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5);
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

        private IEnumerator PlayAirlockSequence()
        {
            // Step 1: Start looping airlock sound
            audioSource.clip = airLockSound;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Looping airlock background sound...");

            // Step 2: Play first dialogue
            audioSource.volume = 0.75f;
            audioSource.PlayOneShot(airLockDialogueOne);
            Debug.Log("Playing first dialogue...");

            // Wait for first dialogue to finish
            yield return new WaitForSeconds(airLockDialogueOne.length);

            // Step 3: Play second dialogue immediately
            audioSource.PlayOneShot(airLockDialogueTwo);
            Debug.Log("Playing second dialogue...");
            
            audioSource.volume = 1f;
            // Wait for second dialogue to finish
            yield return new WaitForSeconds(airLockDialogueTwo.length);

            // Step 4: Wait 0.5 sec, then turn keypad green
            yield return new WaitForSeconds(0.5f);
            keyPadScreen.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Keypad turned green!");

            // Step 5: Wait 0.3 sec, then open the airlock
            yield return new WaitForSeconds(0.3f);
            OpenAirLock();
            Debug.Log("Airlock door opened!");

            // Stop the looping background sound
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
