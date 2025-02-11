using UnityEngine;

namespace GogoGaga.OptimizedRopesAndCables
{
    [RequireComponent(typeof(RopeMesh))]
    public class RopeMeshWaveWithFixedStartAndEndLinks : MonoBehaviour
    {
        public Transform targetObject;       // Das Zielobjekt, an dem das Seilende fixiert ist
        public float waveAmplitude = 0.2f;   // Maximale Höhe der Wellenbewegung
        public float waveFrequency = 1f;     // Anzahl der Wellen entlang des Seils
        public float waveSpeed = 2f;         // Geschwindigkeit der Wellenbewegung
        public int fixedEndSegments = 2;     // Anzahl der letzten Segmente, die nicht schlabbern

        private RopeMesh ropeMesh;

        void Start()
        {
            ropeMesh = GetComponent<RopeMesh>();
        }

        void Update()
        {
            if (ropeMesh == null || targetObject == null) return;

            // Hole die Punkte des Seils
            Vector3[] points = new Vector3[ropeMesh.OverallDivision + 1];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = ropeMesh.GetComponent<Rope>().GetPointAt(i / (float)ropeMesh.OverallDivision);
            }

            // Wellenbewegung nur auf die mittleren Punkte anwenden
            for (int i = 1; i < points.Length - fixedEndSegments; i++)  // Beginne bei 1, um das erste Segment zu überspringen
            {
                points[i].y += Mathf.Sin(Time.time * waveSpeed + i * waveFrequency) * waveAmplitude;
            }

            // Setze die letzten Segmente in einer weichen Linie zum Zielobjekt, um eine zugespitzte Form zu vermeiden
            for (int i = points.Length - fixedEndSegments; i < points.Length; i++)
            {
                float t = (float)(i - (points.Length - fixedEndSegments)) / (fixedEndSegments - 1);
                points[i] = Vector3.Lerp(points[i], targetObject.position, t);
            }

            // Richte die Rotation des letzten Abschnitts an der Richtung des Seils aus
            if (points.Length > 2)
            {
                Vector3 direction = (points[points.Length - 1] - points[points.Length - 2]).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction, targetObject.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetObject.rotation, Time.deltaTime * 10f);
            }

            // Seil-Mesh mit den modifizierten Punkten neu generieren
            ropeMesh.ropeWidth = Mathf.Max(ropeMesh.ropeWidth, 0.01f);  // Mindestbreite festlegen
            ropeMesh.CreateRopeMesh(points, ropeMesh.ropeWidth, ropeMesh.radialDivision);
        }
    }
}