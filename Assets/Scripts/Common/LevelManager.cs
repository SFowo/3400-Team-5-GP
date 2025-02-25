using System;
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
        [SerializeField] private GameObject player;
    
        [Header("AirLock")] 
        [SerializeField] private AudioClip airLockSound;
        [SerializeField] private AudioClip initialDialogue;

        // Add more headers and objects
        #endregion

        #region Trigger&Delgator
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
                    audioSource.PlayOneShot(airLockSound);
                    Invoke(nameof(OpenAirLock), airLockSound.length);
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

        private void OpenAirLock()
        {
        
        }
    }
}