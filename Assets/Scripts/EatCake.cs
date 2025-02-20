using UnityEngine;

public class EatCake : MonoBehaviour
{
    public GameObject[] cakePieces; // Array of cake pieces
    private int cakeIndex = 0; // Track the current piece being eaten

    void Start()
    {
        // Ensure all cake pieces are active at the start
        foreach (GameObject cake in cakePieces)
        {
            if (cake != null)
                cake.SetActive(true);
        }
    }

    void Update()
    {
        // Check for eating input
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEatCake();
        }
    }

    void TryEatCake()
    {
        // Get the actual seat the player is sitting in
        SitDown currentSeat = SitDown.currentSeat;

        // Check if the player is sitting and NOT in the starting seat
        if (currentSeat != null && currentSeat.IsPlayerCurrentlySittingHere() && !currentSeat.IsStartingSeat())
        {
            EatNextPiece();
        }
        else
        {
            Debug.Log("You can only eat cake while sitting in a non-starting seat!");
        }
    }

    void EatNextPiece()
    {
        // If all cake pieces are eaten, do nothing
        if (cakeIndex >= cakePieces.Length)
        {
            Debug.Log("No more cake left!");
            return;
        }

        // Disable the next cake piece
        if (cakePieces[cakeIndex] != null)
        {
            cakePieces[cakeIndex].SetActive(false);
        }

        cakeIndex++; // Move to the next piece
    }
}