using UnityEngine;

public class SloppyArmPhysics : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Rigidbody[] armSegments;

    void FixedUpdate()
    {
        for (int i = 0; i < armSegments.Length; i++)
        {
            Transform bone = skinnedMeshRenderer.bones[i];
            bone.position = armSegments[i].position;
            bone.rotation = armSegments[i].rotation;
        }
    }
}