using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class DiatomController : MonoBehaviour
{
    [Tooltip("List of Diatom GameObjects in Scene")]
    public List<GameObject> diatomList;

    // Activated by ShowMessageFromList at the appropriate stage of the presentation
    public void AnimationTrigger(string trigger)
    {
        foreach (GameObject diatom in diatomList)
        {
            Animator diatomAnimator = diatom.GetComponent<Animator>();
            diatomAnimator.SetTrigger(trigger);
        }
    }

    public void FadeOutTrigger()
    {
        foreach (GameObject diatom in diatomList)
        {
            FadeMaterial fadeScript = diatom.GetComponent<FadeMaterial>();
            fadeScript.StartFadeOut();
        }
    }

    public void SubsetTrigger(string trigger)
    {
        List<int> indices = new List<int>() { 1, 2, 3 };

        foreach (int index in indices)
        {
            Animator diatomAnimator = diatomList[index].GetComponent<Animator>();
            diatomAnimator.SetTrigger(trigger);
        }
    }
}
