using UnityEngine;

[System.Serializable]
public class SmileFacePattern : CoinPattern
{
    public SmileFacePattern()
    {
        // Define the positions for the smiley face pattern
        positions = new Vector3[]
        {
            new Vector3(0, 0, 0),     // Center
            new Vector3(-1, 1, 0),    // Left eye
            new Vector3(1, 1, 0),     // Right eye
            new Vector3(-2, -1, 0),   // Left cheek
            new Vector3(2, -1, 0),    // Right cheek
        };
    }
}