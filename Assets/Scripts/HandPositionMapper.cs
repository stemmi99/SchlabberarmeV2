using UnityEngine;

public class HandPositionMapper : MonoBehaviour
{
    public Transform rightHandAnchor;  // Referenz zur rechten Hand
    public Vector3 positionOffset = Vector3.zero;  // Positionsoffset (x, y, z), im Inspector anpassbar

    void Update()
    {
        if (rightHandAnchor != null)
        {
            // Setze die Position der Hand mit dem Offset
            transform.position = rightHandAnchor.position + positionOffset;

            // Hole die Euler-Winkel der Rotation
            Vector3 handRotation = rightHandAnchor.rotation.eulerAngles;

            // Spiegle die Y- und Z-Werte
            handRotation.x *= -1;
            handRotation.y *= -1;
            handRotation.z *= -1;

            // Setze die gespiegelte Rotation
            transform.rotation = Quaternion.Euler(handRotation);
        }
        else
        {
            Debug.LogWarning("RightHandAnchor not assigned or not found.");
        }
    }
}