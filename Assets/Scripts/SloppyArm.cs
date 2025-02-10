using UnityEngine;

public class SloppyArm : MonoBehaviour
{
    public Transform bodyOrigin;  // Ursprung des Arms (z.B. SloppyArmsRoot)
    public Transform handTarget;  // Handposition (z.B. LeftHandAnchor oder RightHandAnchor)
    public LineRenderer lineRenderer;
    public Rigidbody[] armSegments;

    private void Start()
    {
        lineRenderer.positionCount = armSegments.Length;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < armSegments.Length; i++)
        {
            // Aktualisiere den LineRenderer mit den Positionen der Segmente
            lineRenderer.SetPosition(i, armSegments[i].position);

            if (i == 0)
            {
                // Das erste Segment wird direkt auf die Position von bodyOrigin gesetzt
                armSegments[i].position = bodyOrigin.position;
            }
            else
            {
                // Die anderen Segmente folgen dem handTarget mit physikalischer Simulation
                Vector3 direction = (handTarget.position - armSegments[i].position).normalized;
                armSegments[i].AddForce(direction * 5f);
            }
        }
    }
}