using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Adapted from ShowMessageFromList (from Unity VR tutorials)

public class GameController : MonoBehaviour
{
    [Header("Text Boxes")]
    [Tooltip("The first text Canvas")]
    public GameObject firstTextCanvas;
    [Tooltip("The second text Canvas")]
    public GameObject secondTextCanvas;
    [Tooltip("The third text Canvas")]
    public GameObject thirdTextCanvas;
    [Tooltip("The first text location")]
    public TextMeshProUGUI firstText;
    [Tooltip("The second text location")]
    public TextMeshProUGUI secondText;
    [Tooltip("The third text location")]
    public TextMeshProUGUI thirdText;
    [Tooltip("The button text of the final textbox")]
    public TextMeshProUGUI finalButtonText;

    [Header("Viewing Windows")]
    [Tooltip("First (diatom) view window")]
    public GameObject firstWindow;
    [Tooltip("Second (lab) view window")]
    public GameObject secondWindow;
    [Tooltip("Third (bioinformatics) view window")]
    public GameObject thirdWindow;

    [Header("Diatoms")]
    [Tooltip("Parent object containing diatom game objects")]
    public GameObject diatomContainer;
    private DiatomController diatomController;

    [Header("Corer")]
    [Tooltip("Parent object containing the corer")]
    public GameObject corer;

    [Header("Sediment Core")]
    [Tooltip("Parent object containing the sediment core")]
    public GameObject sedimentCore;

    [Header("Particle Systems")]
    [Tooltip("The sediment Particle System")]
    public ParticleSystem sedimentParticles;

    [Header("Diatom Flask")]
    [Tooltip("Parent object containing the diatom flask")]
    public GameObject diatomFlask;

    [Header("Bioinformatics Canvas")]
    [Tooltip("First page of bioinformatics analysis")]
    public GameObject firstAnalysis;
    [Tooltip("Second page of bioinformatics analysis")]
    public GameObject secondAnalysis;
    [Tooltip("Third page of bioinformatics analysis")]
    public GameObject thirdAnalysis;

    // What happens once the list is completed
    // Check if this is required
    public UnityEvent OnComplete = new UnityEvent();

    [Header("Message list")]
    [Tooltip("The list of messages that are shown")]
    [TextArea] public List<string> messages = new List<string>();

    // Index number of the current message
    private int index = 0;

    void Start()
    {
        diatomController = diatomContainer.GetComponent<DiatomController>();
        NextMessage();
    }

    public void NextMessage()
    {
        if (index > (messages.Count - 1))
        {
            OnComplete.Invoke();
        }
        else
        {
            // Check the most elegant way to assign this text
            // Make a new function?
            TextMeshProUGUI textDisplay = firstText;

            if (index > 7 && index < 11)
            {
                textDisplay = secondText;
            }
            else if (index > 10)
            {
                textDisplay = thirdText;
            }

            textDisplay.text = messages[index];
            StageEffects(index);
            index++;
        }
    }

    // Which effects to play during this step of the experience
    void StageEffects(int msgStep)
    {
        if (msgStep == 1)
        {
            diatomController.AnimationTrigger("Sink");
        }
        else if (msgStep == 2)
        {
            diatomController.SubsetTrigger("Rise");
        }
        else if (msgStep == 3)
        {
            // No effects here
        }
        else if (msgStep == 4)
        {
            sedimentParticles.Play();
        }
        else if (msgStep == 5)
        {
            diatomController.AnimationTrigger("Burial");
        }
        else if (msgStep == 6)
        {
            diatomController.FadeOutTrigger();
        }
        else if (msgStep == 7)
        {
            sedimentParticles.Stop();
            corer.SetActive(true);
            Destroy(diatomContainer);
        }
        else if (msgStep == 8)
        {
            // Switch from 'diatom' display to 'lab' display
            firstTextCanvas.gameObject.SetActive(false);
            secondTextCanvas.gameObject.SetActive(true);

            // Switch the fading on the windows
            FadeMaterial firstWindowFade = firstWindow.GetComponent<FadeMaterial>();
            FadeMaterial secondWindowFade = secondWindow.GetComponent<FadeMaterial>();

            firstWindowFade.StartFadeIn();
            secondWindowFade.StartFadeOut();
        }
        else if (msgStep == 9)
        {
            CoreSliceMovement coreSliceMovement = sedimentCore.GetComponent<CoreSliceMovement>();
            coreSliceMovement.MoveSlices();
        }
        else if (msgStep == 10)
        {
            //Destroy the moved slices
            CoreSliceMovement coreSliceMovement = sedimentCore.GetComponent<CoreSliceMovement>();
            coreSliceMovement.DestroySlices();

            // Spawn in a flask (fade in if possible)
            diatomFlask.SetActive(true);
        }
        else if (msgStep == 11)
        {
            // Switch from 'lab' display to 'bioinformatics' display
            secondTextCanvas.gameObject.SetActive(false);
            thirdTextCanvas.gameObject.SetActive(true);

            // Switch the fading on the windows
            FadeMaterial secondWindowFade = secondWindow.GetComponent<FadeMaterial>();
            FadeMaterial thirdWindowFade = thirdWindow.GetComponent<FadeMaterial>();

            secondWindowFade.StartFadeIn();
            thirdWindowFade.StartFadeOut();
        }
        else if (msgStep == 12)
        {
            firstAnalysis.SetActive(true);
        }
        else if (msgStep == 13)
        {
            firstAnalysis.SetActive(false);
            secondAnalysis.SetActive(true);
        }
        else if (msgStep == 14)
        {
            secondAnalysis.SetActive(false);
            thirdAnalysis.SetActive(true);
        }
        else if (msgStep == 15)
        {
            finalButtonText.text = "Restart";
        }
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
	    Application.Quit();
    #endif
    }
}
