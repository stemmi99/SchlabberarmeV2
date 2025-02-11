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

            // Korrigiere die Rotation (Y- und Z-Achse tauschen)
            Quaternion handRotation = rightHandAnchor.rotation;
            transform.rotation = Quaternion.Euler(handRotation.eulerAngles.x, handRotation.eulerAngles.z, handRotation.eulerAngles.y);
        }
        else
        {
            Debug.LogWarning("RightHandAnchor not assigned or not found.");
        }
    }
}