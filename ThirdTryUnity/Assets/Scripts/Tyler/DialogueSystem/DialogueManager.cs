using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text _nameText;
    public Text _dialogueText;

    public Queue<string> _sentences;
    // Use this for initialization
    void Start()
    {
        _sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _nameText.text = dialogue._name;

        _sentences.Clear();

        foreach (var sentence in dialogue._sentences)
            _sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";

        foreach (var letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;

            //waits a single frame
            yield return null;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("end");
    }
}
