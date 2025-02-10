using UnityEngine;

public class SloppyArm : MonoBehaviour
{
    public Transform bodyOrigin;  // Feste Position für den Anfang des Arms
    public Transform handTarget;  // Zielposition der Hand
    public LineRenderer lineRenderer;
    public Rigidbody[] armSegments;

    private void Start()
    {
        if (lineRenderer != null && armSegments.Length > 0)
        {
            lineRenderer.positionCount = armSegments.Length;
        }
    }

    private void FixedUpdate()
    {
        if (armSegments == null || armSegments.Length == 0 || handTarget == null) return;

        for (int i = 0; i < armSegments.Length; i++)
        {
            // Aktualisiere die Positionen im LineRenderer
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(i, armSegments[i].position);
            }

            if (i == 0)
            {
                // Das erste Segment bleibt an der festen Position von bodyOrigin
                armSegments[i].MovePosition(bodyOrigin.position);
            }
            else if (i == armSegments.Length - 1)
            {
                // Das letzte Segment folgt direkt der Handposition
                armSegments[i].MovePosition(handTarget.position);
                armSegments[i].useGravity = false;  // Verhindert, dass das letzte Segment absinkt
            }
            else
            {
                // Die mittleren Segmente folgen physikalisch der Handposition mit reduzierter Kraft
                Vector3 direction = (handTarget.position - armSegments[i].position).normalized;
                armSegments[i].AddForce(direction * 2f);  // Sanftere Kraft für eine geschmeidige Bewegung
            }
        }
    }
}