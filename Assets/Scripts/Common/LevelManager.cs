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
            InitialVent,
            CargoVent,
            ThirdFloor,
        }

        [Header("Common")] 
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject player;

        [Header("AirLock")] 
        [SerializeField] private AudioClip airLockSound;  // Now plays only once when the door opens
        [SerializeField] private AudioClip airLockDialogueOne;
        [SerializeField] private AudioClip airLockDialogueTwo;
        [SerializeField] private GameObject keyPadScreen;
        [SerializeField] private Animator airLockAnimator; // Door animator
        [SerializeField] private AudioClip doorOpening;
        [SerializeField] private AudioClip keypadUnlock;
        [SerializeField] private AudioClip airlockGas;
        
        [Header("Vent")]
        [SerializeField] private AudioClip heartBeat;
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip monsterSound;
        [SerializeField] private GameObject deadGuy;  // Reference to the dead guy in vent

        private AudioSource heartBeatSource;
        private AudioSource bgMusicSource;

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Start playing airlock sequence as soon as the scene loads
            StartCoroutine(PlayAirlockSequence());

            // Setup separate audio sources for background music and heartbeat
            heartBeatSource = gameObject.AddComponent<AudioSource>();
            heartBeatSource.clip = heartBeat;
            heartBeatSource.loop = true;
            heartBeatSource.volume = 0.2f; // Start at low volume

            bgMusicSource = gameObject.AddComponent<AudioSource>();
            bgMusicSource.clip = backgroundMusic;
            bgMusicSource.loop = true;
        }

        #endregion

        #region Public Methods

        public void TriggerZoneEntered(GameObject obj, TriggerType type)
        {
            Debug.Log($"Performing {type} action for {obj.name}");

            switch (type)
            {
                case TriggerType.AirLock:
                    // Airlock sequence starts automatically at scene load
                    break;

                case TriggerType.FirstFloor:
                    AdjustPlayerSize(0.3f, 0.5f);
                    player.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5);
                    player.GetComponent<Player.Player>().CurrentState = Player.Player.State.Flying;

                    // Start background music for the entire game
                    if (!bgMusicSource.isPlaying)
                    {
                        bgMusicSource.Play();
                        Debug.Log("Background music started...");
                    }
                    break;

                case TriggerType.InitialVent:
                    StartCoroutine(PlayHeartbeatIncreasingVolume());
                    if (!bgMusicSource.isPlaying)
                    {
                        bgMusicSource.Play();
                    }
                    break;

                case TriggerType.CargoVent:
                    // Play monster sound once
                    audioSource.PlayOneShot(monsterSound);
                    Debug.Log("Monster sound played in cargo vent!");
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
            // Step 1: Play first dialogue
            audioSource.volume = 0.75f;
            audioSource.PlayOneShot(airLockDialogueOne);
            Debug.Log("Playing first dialogue...");

            // Wait for first dialogue to finish
            yield return new WaitForSeconds(airLockDialogueOne.length - 2);

            // Step 2: Play second dialogue
            audioSource.PlayOneShot(airLockDialogueTwo);
            Debug.Log("Playing second dialogue...");
            
            audioSource.volume = 1f;
            yield return new WaitForSeconds(airLockDialogueTwo.length);

            // Step 3: Wait 0.3 sec, then turn keypad green
            yield return new WaitForSeconds(0.3f);
            keyPadScreen.GetComponent<Renderer>().material.color = Color.green;
            audioSource.PlayOneShot(keypadUnlock);
            Debug.Log("Keypad turned green!");

            // Step 4: Wait 0.3 sec, then open the airlock
            yield return new WaitForSeconds(0.3f);
            OpenAirLock();
            audioSource.PlayOneShot(doorOpening);
            yield return new WaitForSeconds(doorOpening.length * 0.5f); 
            audioSource.PlayOneShot(airlockGas);

            Debug.Log("Airlock door opened!");

            // Step 5: **Play airlock sound once (not looping)**
            audioSource.PlayOneShot(airLockSound);
            Debug.Log("Airlock sound played once.");
        }

        private void OpenAirLock()
        {
            airLockAnimator.SetBool("Open", true);
            airLockAnimator.SetBool("Close", false);
        }

        private IEnumerator PlayHeartbeatIncreasingVolume()
        {
            heartBeatSource.Play();
            Debug.Log("Heartbeat started...");

            while (heartBeatSource.volume < 1f)
            {
                float distance = Vector3.Distance(player.transform.position, deadGuy.transform.position);
                float volume = Mathf.Clamp(1f - (distance / 10f), 0.5f, 1f); // Volume increases as player gets closer
                heartBeatSource.volume = volume;
                yield return new WaitForSeconds(0.2f); // Gradual increase
            }
        }

        #endregion
    }
}
