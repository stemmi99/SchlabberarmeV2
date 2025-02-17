using UnityEngine;
using Oculus;

public class GrabInteractionValidator : MonoBehaviour
{
    public GameObject object1;  // Erstes Objekt mit Collider
    public GameObject object2;  // Zweites Objekt, das gekoppelt werden soll
    public OVRHand hand;        // Die Hand, um die Pinch-Geste zu überprüfen

    public Vector3 positionOffset = Vector3.zero;  // Offset für die Position (anpassbar im Inspector)
    public Vector3 rotationOffset = Vector3.zero;  // Offset für die Rotation (in Euler-Winkeln, anpassbar im Inspector)

    private bool isColliding = false;
    private bool isHolding = false;  // Zum Überprüfen, ob das Objekt gerade gehalten wird
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Ursprüngliche Position und Rotation speichern
        originalPosition = object2.transform.position;
        originalRotation = object2.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == object1)
        {
            isColliding = true;
            Debug.Log("Collision detected: Ready to grab.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == object1)
        {
            isColliding = false;
            Debug.Log("Collision ended.");
        }
    }

    void Update()
    {
        // Prüfen, ob die Kollision aktiv ist und die Pinch-Geste gehalten wird
        if (isColliding && hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if (!isHolding)
            {
                isHolding = true;
                Debug.Log("Pinch detected: Object coupled.");
            }

            // Berechne die Zielposition mit dem Offset
            Vector3 targetPosition = object1.transform.position + object1.transform.TransformDirection(positionOffset);

            // Berechne die Zielrotation mit dem Offset
            Quaternion targetRotation = object1.transform.rotation * Quaternion.Euler(rotationOffset);

            // Setze die Position und Rotation von object2 auf die berechneten Werte
            object2.transform.position = targetPosition;
            object2.transform.rotation = targetRotation;
        }
        else
        {
            if (isHolding)
            {
                isHolding = false;
                Debug.Log("Pinch released: Object uncoupled.");

                // Führe das Objekt zurück zur ursprünglichen Position und Rotation
                StartCoroutine(ReturnToOriginalPosition());
            }
        }
    }

    System.Collections.IEnumerator ReturnToOriginalPosition()
    {
        float duration = 0.5f;  // Dauer der Rückkehrbewegung (in Sekunden)
        float elapsedTime = 0f;
        Vector3 startPosition = object2.transform.position;
        Quaternion startRotation = object2.transform.rotation;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpoliere Position und Rotation
            object2.transform.position = Vector3.Lerp(startPosition, originalPosition, t);
            object2.transform.rotation = Quaternion.Slerp(startRotation, originalRotation, t);

            yield return null;
        }

        // Stelle sicher, dass das Objekt exakt an der ursprünglichen Position/Rotation endet
        object2.transform.position = originalPosition;
        object2.transform.rotation = originalRotation;
    }
}