using UnityEngine;

public class AssignBones : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform[] boneTransforms;  // Hier deine Bone-Objekte aus der Hierarchie zuweisen

    void Start()
    {
        if (skinnedMeshRenderer != null && boneTransforms.Length > 0)
        {
            skinnedMeshRenderer.bones = boneTransforms;
            Debug.Log("Bones successfully assigned!");
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer or bones are missing!");
        }
    }
}