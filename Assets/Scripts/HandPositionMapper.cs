using UnityEngine;

public class HandPositionMapper : MonoBehaviour
{
    public Transform rightHandAnchor;            // Referenz zur rechten Hand
    public Vector3 positionOffset = Vector3.zero; // Positionsoffset (x, y, z), im Inspector anpassbar
    public float smoothTime = 0.5f;              // Zeit in Sekunden, bis die Position die Zielposition erreicht (je höher, desto träger)

    private Vector3 currentVelocity = Vector3.zero;  // Zwischenspeicher für die Geschwindigkeit (SmoothDamp)

    void Update()
    {
        if (rightHandAnchor != null)
        {
            // Berechne die Zielposition mit dem Offset und verdopple die Differenz zur Startposition
            Vector3 targetPosition = transform.position + 2 * (rightHandAnchor.position + positionOffset - transform.position);

            // Verwende SmoothDamp für eine weiche, träge Bewegung
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

            // Hole die Euler-Winkel der Rotation
            Vector3 handRotation = rightHandAnchor.rotation.eulerAngles;

            // Spiegle die Y- und Z-Werte
            handRotation.x *= -1;
            handRotation.y *= -1;
            handRotation.z *= -1;

            // Setze die gespiegelte Rotation direkt (ohne Trägheit)
            transform.rotation = Quaternion.Euler(handRotation);
        }
        else
        {
            Debug.LogWarning("RightHandAnchor not assigned or not found.");
        }
    }
}