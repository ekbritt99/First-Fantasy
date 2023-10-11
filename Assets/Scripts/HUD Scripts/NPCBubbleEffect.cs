using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCBubbleEffect : MonoBehaviour
{
    private static NPCBubbleEffect instance;

    private List<NPCBubbleEffectSingle> NPCBubbleEffectSingleList;

    private void Awake()
    {
        instance = this;
        NPCBubbleEffectSingleList = new List<NPCBubbleEffectSingle>();
    }

    public static NPCBubbleEffectSingle AddWriter_Static(TextMeshPro uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete)
    {
        if (removeWriterBeforeAdd)
        {
            instance.RemoveWriter(uiText);
        }
        return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    private NPCBubbleEffectSingle AddWriter(TextMeshPro uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
    {
        NPCBubbleEffectSingle NPCBubbleEffectSingle = new NPCBubbleEffectSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        NPCBubbleEffectSingleList.Add(NPCBubbleEffectSingle);
        return NPCBubbleEffectSingle;
    }

    public static void RemoveWriter_Static(TextMeshPro uiText)
    {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(TextMeshPro uiText)
    {
        for (int i = 0; i < NPCBubbleEffectSingleList.Count; i++)
        {
            if (NPCBubbleEffectSingleList[i].GetUIText() == uiText)
            {
                NPCBubbleEffectSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < NPCBubbleEffectSingleList.Count; i++)
        {
            bool destroyInstance = NPCBubbleEffectSingleList[i].Update();
            if (destroyInstance)
            {
                NPCBubbleEffectSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    /*
     * Represents a single TextWriter instance
     * */
    public class NPCBubbleEffectSingle
    {

        private TextMeshPro uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;
        private Action onComplete;

        public NPCBubbleEffectSingle(TextMeshPro uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.onComplete = onComplete;
            characterIndex = 0;
        }

        // Returns true on complete
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                // Display next character
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    // Entire string displayed
                    if (onComplete != null) onComplete();
                    return true;
                }
            }

            return false;
        }

        public TextMeshPro GetUIText()
        {
            return uiText;
        }

        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            if (onComplete != null) onComplete();
            NPCBubbleEffect.RemoveWriter_Static(uiText);
        }


    }
}
