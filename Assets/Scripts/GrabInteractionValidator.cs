using UnityEngine;
using Oculus;  // Für OVRHand

public class GrabInteractionValidator : MonoBehaviour
{
    public GameObject object1;  // Erstes Objekt mit Collider
    public GameObject object2;  // Zweites Objekt, das gekoppelt werden soll
    public OVRHand hand;        // Die Hand, um die Pinch-Geste zu überprüfen

    private bool isColliding = false;
    private bool isHolding = false;  // Zum Überprüfen, ob das Objekt gerade gehalten wird

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
        // Prüfen, ob die Kollision aktiv ist und die Pinch-Geste (Daumen und Zeigefinger) gehalten wird
        if (isColliding && hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if (!isHolding)
            {
                isHolding = true;
                Debug.Log("Pinch detected: Object coupled.");
            }

            // Kopple Position und Rotation von object2 an object1
            object2.transform.position = object1.transform.position;
            object2.transform.rotation = object1.transform.rotation;
        }
        else
        {
            if (isHolding)
            {
                isHolding = false;
                Debug.Log("Pinch released: Object uncoupled.");
            }
        }
    }
}