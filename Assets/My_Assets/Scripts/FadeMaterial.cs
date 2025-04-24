using System.Collections;
using UnityEngine;

// Syntax adapted from the FadeCanvas script included in the Unity VR Room tutorials

public class FadeMaterial : MonoBehaviour
{
    [Tooltip("The speed at which the canvas fades")]
    public float defaultDuration = 1.0f;

    public Coroutine CurrentRoutine { private set; get; } = null;

    private MeshRenderer meshRenderer = null;
    private Color originalColour;
    private Color newColour;
    private float alpha = 0.0f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColour = meshRenderer.material.color;
        newColour = originalColour;
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeIn(defaultDuration));
    }

    public void StartFadeOut()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeOut(defaultDuration));
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0.0f;

        while (alpha <= 255.0f)
        {
            SetAlpha(elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0.0f;

        while (alpha >= 0.0f)
        {
            SetAlpha(1 - (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetAlpha (float value)
    {
        alpha = value;
        newColour.a = alpha;
        meshRenderer.material.color = newColour;
    }
}