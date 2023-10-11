using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCBubbleManager : MonoBehaviour
{
    [SerializeField] private float displaySpeed = 0.05f;

    [Header("Dialogue Text")]
    [SerializeField] private TextMeshProUGUI npcDialogueText;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] npcDialogueSentences;

    [Header("Bubbles")]
    [SerializeField] private GameObject shopBubble;

    private bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
    }

    private void startDialogue()
    {
        StartCoroutine(TypeShopNPCDialogue());
    }

    private IEnumerator TypeShopNPCDialogue()
    {
        if(hasStarted == false)
        {
            npcDialogueText.text = "";
            hasStarted = true;
            char[] charSentence = npcDialogueSentences[0].ToCharArray();
            foreach (char letter in charSentence)
            {
                npcDialogueText.text += letter;
                yield return new WaitForSeconds(displaySpeed);
            }
            if (npcDialogueSentences.Length > 1)
            {
                yield return new WaitForSeconds(1f);
                npcDialogueText.text = "";
                char[] charSentence2 = npcDialogueSentences[1].ToCharArray();
                foreach (char letter in charSentence2)
                {
                    npcDialogueText.text += letter;
                    yield return new WaitForSeconds(displaySpeed);
                }
            }
            if (GameManager.Instance.sceneState == Scenes.WORLD || GameManager.Instance.sceneState == Scenes.VILLAGE)
            {
                Invoke("hideShopBubble", 2.0f);
            }
            else
            {
                Invoke("hideDialogueBox", 2.0f);
            }
        }
    }
    //ShopBubble handles npc rat dialogue
    private void showShopBubble()
    {
        shopBubble.SetActive(true);
    }

    private void hideShopBubble()
    {
        shopBubble.SetActive(false);
        npcDialogueText.text = "";
        hasStarted = false;
    }
    //general dialogue box showing/hiding
    private void showDialogueBox()
    {
        GameManager.Instance.dialogueBox.SetActive(true);
        npcDialogueText = GameManager.Instance.dialogueBox.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void hideDialogueBox()
    {
        GameManager.Instance.dialogueBox.SetActive(false);
        npcDialogueText.text = "";
        hasStarted = false;
    }


}
