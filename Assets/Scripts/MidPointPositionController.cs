using UnityEngine;

namespace GogoGaga.OptimizedRopesAndCables
{
    [ExecuteAlways]
    public class MidPointPositionController : MonoBehaviour
    {
        private Rope ropeScript;

        private void Start()
        {
            // Hole das Rope-Script auf demselben GameObject
            ropeScript = GetComponent<Rope>();

            if (ropeScript == null)
            {
                Debug.LogError("Rope script not found on the GameObject!");
                enabled = false;
            }
        }

        private void Update()
        {
            if (ropeScript != null)
            {
                ropeScript.midPointPosition = Mathf.Lerp(0.25f, 0.75f, Mathf.PingPong(Time.time / 2f, 1f));
            }
        }
    }
}