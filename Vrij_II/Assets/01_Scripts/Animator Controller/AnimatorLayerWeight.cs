using UnityEngine;
using UnityEngine.Animations.Rigging;


public class AnimatorLayerWeight : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public bool isAiming;

    private int upperBodyLayerIndex;

    [SerializeField]
    private Rig[] rigs;
    [SerializeField]
    private Transform spearHeldTransform;
    [SerializeField]
    public Transform spearAimTransform;
    [SerializeField]
    private Transform spearBoneTransform;

    private void Start()
    {
        upperBodyLayerIndex = animator.GetLayerIndex("UpperBody");
    }

    private void Update()
    {
        float weight = isAiming ? 1f : 0f;
        animator.SetLayerWeight(upperBodyLayerIndex, weight);
        animator.SetBool("hasSpear", isAiming);

        foreach (Rig rig in rigs)
        {
            rig.weight = weight;
        }

        if (weight == 0)
        {
            spearBoneTransform.localRotation = spearHeldTransform.localRotation;
            spearBoneTransform.localPosition = spearHeldTransform.localPosition;
        }
        else
        {
            spearBoneTransform.localRotation = spearAimTransform.localRotation;
            spearBoneTransform.localPosition = spearAimTransform.localPosition;
        }
    }
}
