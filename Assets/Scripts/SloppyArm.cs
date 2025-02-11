using UnityEngine;

public class SloppyArm : MonoBehaviour
{
    public Transform bodyOrigin;   // Feste Basisposition des Arms
    public Transform handTarget;   // Zielposition der Hand
    public LineRenderer lineRenderer;
    public Rigidbody[] armSegments;

    public float followForce = 2f;  // Kraft, mit der die mittleren Segmente der Hand folgen
    public Vector3 armOffset = Vector3.zero;  // Offset, der die Ausgangsposition des ersten Segments bestimmt

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

        // Berechne die korrigierte Position für das erste Segment mit dem Arm-Offset
        Vector3 correctedBodyPosition = bodyOrigin.position + armOffset;
        Vector3 correctedHandPosition = handTarget.position;

        for (int i = 0; i < armSegments.Length; i++)
        {
            // Aktualisiere die Positionen im LineRenderer
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(i, armSegments[i].position);
            }

            if (i == 0)
            {
                // Das erste Segment bleibt an der korrigierten Position von bodyOrigin (mit Offset)
                armSegments[i].MovePosition(correctedBodyPosition);
            }
            else if (i == armSegments.Length - 1)
            {
                // Das letzte Segment folgt direkt der Handposition
                armSegments[i].MovePosition(correctedHandPosition);
                armSegments[i].useGravity = false;  // Verhindert, dass das letzte Segment absinkt
            }
            else
            {
                // Die mittleren Segmente folgen physikalisch der Handposition mit reduzierter Kraft
                Vector3 direction = (correctedHandPosition - armSegments[i].position).normalized;
                armSegments[i].AddForce(direction * followForce);
            }
        }
    }
}