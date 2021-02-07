using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerPrompts : MonoBehaviour
{
    [System.Serializable]

    private class Prompt
    {
        [Header("ControllerPrompt")]
        public PromptCondition promptCondition;
        public SteamVR_Action_Boolean promptAction;
        public string promptInstructions;

        public Coroutine holdPromptActive;
    }


    private enum PromptCondition
    {
        None, 
        EmptyHand, 
        InteractingHand,
        InteractingWithThisInteractable
    }

    [Header("Stats")]
    [SerializeField] private float promptUpdateRate = 0.1f;

    [Header("Prompts")]
    [SerializeField] private List<Prompt> interactablePrompts = new List<Prompt>();
    private Interactable interactable;
    private bool promptActive;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Interactable>(out Interactable i))
            interactable = i;
    }

    #region General
    public void ActivatePrompt(int index)
    {
        if (interactablePrompts.Count < index || interactablePrompts[index] == null)
            return;

        Debug.Log("Activate Prompt");

        if (interactablePrompts[index].holdPromptActive != null)
            StopCoroutine(interactablePrompts[index].holdPromptActive);

        interactablePrompts[index].holdPromptActive = StartCoroutine(HoldPromptActive(interactablePrompts[index]));
    }

    public void DisableAllPrompts()
    {
        if (promptActive == false)
            return;

        foreach (Prompt prompt in interactablePrompts)
        {
            if (prompt.holdPromptActive != null)
                StopCoroutine(prompt.holdPromptActive);
        }

        foreach (Hand hand in Player.instance.hands)
        {
            ControllerButtonHints.HideAllButtonHints(hand);
            ControllerButtonHints.HideAllTextHints(hand);
        }

        promptActive = false;
    }
    #endregion

    #region Controller Prompt
    
    IEnumerator HoldPromptActive(Prompt prompt)
    {
        promptActive = true;

        while (promptActive == true)
        {
            switch (prompt.promptCondition)
            {
                case PromptCondition.None:
                    DisplayPrompt(Player.instance.rightHand, prompt);
                    DisplayPrompt(Player.instance.leftHand, prompt);
                    break;

                case PromptCondition.EmptyHand:
                    foreach (Hand hand in Player.instance.hands)
                    {
                        if (hand.currentAttachedObject == null)
                            DisplayPrompt(hand, prompt);
                        else
                            DisablePrompt(hand, prompt);
                    }
                    break;

                case PromptCondition.InteractingHand:
                    foreach (Hand hand in Player.instance.hands)
                    {
                        if (hand.currentAttachedObject != null)
                            DisplayPrompt(hand, prompt);
                        else
                            DisablePrompt(hand, prompt);
                    }
                    break;

                case PromptCondition.InteractingWithThisInteractable:
                    foreach (Hand hand in Player.instance.hands)
                    {
                        if (interactable != null && interactable.gameObject == hand.currentAttachedObject)
                            DisplayPrompt(hand, prompt);
                        else
                            DisablePrompt(hand, prompt);
                    }
                    break;
            }
            yield return new WaitForSeconds(promptUpdateRate); 
        }
    }


    void DisplayPrompt(Hand hand, Prompt prompt)
    {
        if (hand == null)
            return;

        if (ControllerButtonHints.IsButtonHintActive(hand, prompt.promptAction) == false)
        {
            ControllerButtonHints.ShowButtonHint(hand, prompt.promptAction);

            if (prompt.promptInstructions != "")
                ControllerButtonHints.ShowTextHint(hand, prompt.promptAction, prompt.promptInstructions);
        }
    }

    void DisablePrompt(Hand hand, Prompt prompt)
    {
        if (hand == null)
            return;

        if (ControllerButtonHints.IsButtonHintActive(hand, prompt.promptAction) == true)
        {
            ControllerButtonHints.HideButtonHint(hand, prompt.promptAction);
            ControllerButtonHints.HideTextHint(hand, prompt.promptAction);
        }
    }
    #endregion
}
