using UnityEngine;
using UnityEngine.VFX;

public class AngleBasedVFXTrigger : MonoBehaviour
{
    public VisualEffect vfxGraph;
    public Transform spoutPosition;     // Transform der Kannenöffnung
    public Transform vfxTransform;      // Transform des VFX (NICHT Child der Kanne)

    private bool isFlowActive = false;

    void Update()
    {
        // Z-Rotation der Kanne prüfen
        float zRotation = transform.eulerAngles.z;
        if (zRotation > 180) zRotation -= 360;

        // Prüfen, ob der Winkel zwischen 40° und 270° liegt
        if (zRotation >= 40 && zRotation <= 270)
        {
            if (!isFlowActive)
            {
                vfxGraph.SendEvent("StartFlow");
                isFlowActive = true;
            }
        }
        else
        {
            if (isFlowActive)
            {
                vfxGraph.SendEvent("StopFlow");
                isFlowActive = false;
            }
        }

        // Berechne die korrekte Weltposition
        Vector3 correctWorldPosition = spoutPosition.parent.TransformPoint(spoutPosition.localPosition);

        // **Offset hinzufügen, um die Position manuell zu korrigieren**
        correctWorldPosition += new Vector3(3.15f, -1.2f, -1.057f);  // Passe die Werte an, bis es unter der Kanne sitzt

        // Debug-Ausgabe der korrigierten Position
        Debug.Log("Corrected VFX Position with Offset: " + correctWorldPosition);

        // Setze die Position des VFX
        vfxTransform.position = correctWorldPosition;

        // Fixiere die Rotation des VFX
        vfxTransform.rotation = Quaternion.identity;
    }
}