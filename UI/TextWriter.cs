using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    //make these all public and standardize it
    //use execpetion throw instead;
    public float TimePerCharacter;
    public bool InvisibleCharacters;

    private List<TextWriterSingle> textWriterSingleList;

    public static TextWriter instance;
    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public TextWriterSingle AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd) 
        {
            instance.RemoveWriter(uiText);
        }
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public TextWriterSingle AddWriter(Text uiText, string textToWrite, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd) 
        {
            instance.RemoveWriter(uiText);
        }
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, TimePerCharacter, InvisibleCharacters);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(Text uiText) 
    {
        instance.RemoveWriter(uiText);
    }
    private void RemoveWriter(Text uiText) 
    {
        for (int i = 0; i < textWriterSingleList.Count; i++) 
        {
            if (textWriterSingleList[i].GetUIText() == uiText) 
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

	private void Update()
	{
        for (int i = 0; i < textWriterSingleList.Count; i++) 
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance) 
            { 
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
	}


	public class TextWriterSingle 
    {
        private Text uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;

        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            characterIndex = 0;
        }

        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer < 0)
            {
                timer += timePerCharacter;
                characterIndex++;
                string temptText = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    temptText += "<color=#00000000>" + textToWrite.Substring(0, characterIndex) + "</color>";
                }
                uiText.text = temptText;

                if (characterIndex >= textToWrite.Length)
                {
                    return true;
                }
            }
            return false;
        }

        public Text GetUIText() 
        {
            return uiText;
        }
        public bool IsActive() 
        { 
            return characterIndex < textToWrite.Length;
        }
        public void WriteAllAndDestory() 
        {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            TextWriter.RemoveWriter_Static(uiText);
        }
    }
}
