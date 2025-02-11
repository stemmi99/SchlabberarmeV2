using UnityEngine;

public class GrabInteractionValidator : MonoBehaviour
{
    public GameObject object1;  // Erstes Objekt mit BoxCollider
    public GameObject object2;  // Zweites Objekt, dessen Bewegung erlaubt werden soll
    private bool canMoveObject2 = false;  // Status, ob object2 bewegt werden darf

    private void OnCollisionEnter(Collision collision)
    {
        // Prüfe, ob beide Objekte miteinander kollidieren
        if ((collision.gameObject == object1 && gameObject == object2) || 
            (collision.gameObject == object2 && gameObject == object1))
        {
            canMoveObject2 = true;
            Debug.Log("Object 2 can now be moved!");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Wenn die Objekte sich nicht mehr berühren, deaktiviere die Bewegung
        if ((collision.gameObject == object1 && gameObject == object2) || 
            (collision.gameObject == object2 && gameObject == object1))
        {
            canMoveObject2 = false;
            Debug.Log("Object 2 movement disabled.");
        }
    }

    void Update()
    {
        // Beispiel: Objekt 2 bewegen, wenn es erlaubt ist
        if (canMoveObject2 && Input.GetKey(KeyCode.G))  // Nur ein Beispiel mit Taste G
        {
            object2.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
}