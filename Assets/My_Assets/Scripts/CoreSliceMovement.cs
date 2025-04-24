using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreSliceMovement : MonoBehaviour
{
    public List<GameObject> coreSlices;

    public void DestroySlices()
    {
        for (int i = 0; i < coreSlices.Count; i++)
        {
            Destroy(coreSlices[i]);
        }
    }

    public void MoveSlices()
    {
        for (int i = 0; i < coreSlices.Count; i++)
        {
            StartCoroutine(AnimateSlice(i));
        }
    }

    private IEnumerator AnimateSlice(int sliceIndex)
    {
        // Get Animator of the relevant core slice
        Animator sliceAnimator = coreSlices[sliceIndex].GetComponent<Animator>();

        // Wait [index] seconds to stagger the slices' disappearance
        yield return new WaitForSeconds(sliceIndex);

        // Play the relevant animation
        sliceAnimator.Play("CoreSlice_Away");
        
    }
}
