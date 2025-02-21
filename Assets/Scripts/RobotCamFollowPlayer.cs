using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCamFollowPlayer : MonoBehaviour {
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        Vector3 target = transform.position - player.transform.position;

        transform.LookAt(player.transform);
    }
}
