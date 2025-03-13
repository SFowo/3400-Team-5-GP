using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject mirror;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject rug;
    [SerializeField] private float mirrorTime = 20f;
    [SerializeField] private float floorTime = 25f;

    private float timer = 0f;
    private bool mirrorEnabled = false;
    private bool floorRugDisabled = false;

    private void Start() {
        if (mirror != null) mirror.SetActive(false);
    }

    private void Update() {
        timer += Time.deltaTime;

        if (!mirrorEnabled && timer >= mirrorTime) {
            if (mirror != null) mirror.SetActive(true);
            mirrorEnabled = true;
        }

        if (!floorRugDisabled && timer >= floorTime) {
            if (floor != null) floor.SetActive(false);
            if (rug != null) rug.SetActive(false);
            floorRugDisabled = true;
        }
    }
}