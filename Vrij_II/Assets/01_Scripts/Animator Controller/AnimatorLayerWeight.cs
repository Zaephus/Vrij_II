using UnityEngine;

public class AnimatorLayerWeight : MonoBehaviour
{
    public Animator animator;
    public bool hasSpear;

    private int upperBodyLayerIndex;

    private void Start()
    {
        upperBodyLayerIndex = animator.GetLayerIndex("UpperBody");
    }

    private void Update()
    {
        float weight = hasSpear ? 1f : 0f;
        animator.SetLayerWeight(upperBodyLayerIndex, weight);
        animator.SetBool("hasSpear", hasSpear);
    }
}
