using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Displays a confirmation box with a message and two buttons
public class ConfirmationBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI confirmationText;
    private System.Action confirmAction;
    private System.Action cancelAction;

    // Show the confirmation box with the given message and actions
    public void ShowConfirmationBox(string message, System.Action onConfirm, System.Action onCancel)
    {
        confirmationText.text = message;
        confirmAction = onConfirm;
        cancelAction = onCancel;
        gameObject.SetActive(true);
    }

    // Perform the confirm action and hide the box
    public void OnConfirmClicked()
    {
        confirmAction?.Invoke();
        gameObject.SetActive(false);
    }   

    // Perform the cancel action and hide the box
    public void OnCancelClicked()
    {
        cancelAction?.Invoke();
        gameObject.SetActive(false);
    }
}
