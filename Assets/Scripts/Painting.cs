using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public Transform player;
    public Transform orientation;
    public float maxDistance;

    public float maxRotation;
    public float minRotation;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject door1;
    [SerializeField]
    private GameObject door2;
    private Transform tf;

    private void Start()
    {
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, tf.position) <= maxDistance && orientation.eulerAngles.y > minRotation && orientation.eulerAngles.y < maxRotation)
        {
            canvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                canvas.enabled = false;
                door1.SetActive(false);
                door2.SetActive(false);
            }
        } else
        {
            canvas.enabled = false;
        }
    }
}
