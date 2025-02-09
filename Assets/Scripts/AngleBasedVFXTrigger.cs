using UnityEngine;
using UnityEngine.VFX;

public class AngleBasedVFXTrigger : MonoBehaviour
{
    public VisualEffect vfxGraph;  // Zieh deinen VFX Graph hier ins Inspector-Fenster
    private bool isFlowActive = false;

    void Update()
    {
        // Z-Rotation des GameObjects auslesen
        float zRotation = transform.eulerAngles.z;

        // Winkel auf Bereich zwischen -180 und 180 normalisieren
        if (zRotation > 180)
            zRotation -= 360;

        // Prüfen, ob der Winkel zwischen 40° und 270° liegt
        if (zRotation >= 40 && zRotation <= 270)
        {
            if (!isFlowActive)
            {
                vfxGraph.SendEvent("StartFlow");  // Nachricht an den VFX Graph senden
                isFlowActive = true;
            }
        }
        else
        {
            if (isFlowActive)
            {
                vfxGraph.SendEvent("StopFlow");  // Nachricht an den VFX Graph senden
                isFlowActive = false;
            }
        }
    }
}