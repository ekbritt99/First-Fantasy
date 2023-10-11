using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSpeechBubbles : MonoBehaviour
{
    private SpriteRenderer backgroundSR;
    private TextMeshPro txtMeshPro;


    public static void Create(GameObject bubblePrefab, Transform parent, Vector3 localPosition, string text)
    {
        GameObject npcBubbleTransform = Instantiate(bubblePrefab, parent);
        npcBubbleTransform.transform.localPosition = localPosition;

        npcBubbleTransform.GetComponent<NPCSpeechBubbles>().Setup(text);
    }

    private void Awake()
    {
        backgroundSR = transform.Find("Background").GetComponent<SpriteRenderer>();
        txtMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        Setup("Hello World");
    }

    private void Setup(string text)
    {
        txtMeshPro.SetText(text);
        txtMeshPro.ForceMeshUpdate();
        Vector2 txtSize = txtMeshPro.GetRenderedValues(false);
        Vector2 backgroundPadding = new Vector2(-0.4f, 0.5f);
        backgroundSR.size = txtSize + backgroundPadding;

        NPCBubbleEffect.AddWriter_Static(txtMeshPro, text, 0.05f, true, true, () => { });
    }
}
