using UnityEngine;

// Code to offset the default bobbing of the diatoms so they don't move in unison
// Code from https://discussions.unity.com/t/using-cycle-offset-feature-inside-of-animator/172679/3
public class DiatomFloatOffset : MonoBehaviour
{
    private Animator diatomAnimator;
    private string floatAnimationName;

    void Start()
    {
        diatomAnimator = GetComponent<Animator>();

        if (gameObject.name.Contains("Diatom_Small"))
        {
            floatAnimationName = "Diatom_Small_Floating";
        }
        else
        {
            floatAnimationName = "Diatom_Floating";
        }

        //Set a random part of the animation to start from
        float randomStartPos = Random.Range(0, diatomAnimator.GetCurrentAnimatorStateInfo(0).length);
        diatomAnimator.Play(floatAnimationName, 0, randomStartPos);
    }
}
