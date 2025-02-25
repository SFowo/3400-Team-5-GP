using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingBehavior : MonoBehaviour
{
    public AudioSource audioSource;
    public List<Image> soundWaves;

    public float smoothingSpeed = 5f;
    public float scaleMultiplier = 500f;
    public float minHeight = 50f;
    public float maxHeight = 150f;
    public float randomOffsetRange = 10f;

    private float[] samples = new float[64];

    void Start()
    {
        //GameObject[] waveObjects = GameObject.FindGameObjectsWithTag("SoundUI");
        //foreach (GameObject obj in waveObjects)
        //{
        //    Image img = obj.GetComponent<Image>();
        //    if (img)
        //        soundWaves.Add(img);
        //}
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            audioSource.GetOutputData(samples, 0);

            for (int i = 0; i < soundWaves.Count; i++)
            {
                float randomOffset = ((i % 2 == 0) ? -1 : 1) * UnityEngine.Random.Range(0, randomOffsetRange);
                float rawHeight = Mathf.Abs(samples[i % samples.Length]) * scaleMultiplier;
                float targetHeight = Mathf.Clamp(rawHeight + randomOffset, minHeight, maxHeight);

                float newHeight = Mathf.Lerp(soundWaves[i].rectTransform.sizeDelta.y, targetHeight, Time.deltaTime * smoothingSpeed);

                soundWaves[i].rectTransform.sizeDelta = new Vector2(soundWaves[i].rectTransform.sizeDelta.x, newHeight);
            }
        }
    }
}
