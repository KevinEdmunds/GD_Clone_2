using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TextChat : NetworkBehaviour
{
    public InputField textInput;
    public Button sendButton;
    public GameObject textObject;
    public int messageLength;
    public GameObject textContainer;
    public List<GameObject> spawnedTexts;

    [ClientRpc]
    public void ClearChat()
    {
        foreach(GameObject message in spawnedTexts)
        {
            Destroy(message);
        }
        spawnedTexts.Clear();
    }

    [Command(requiresAuthority =false)]
    public void UploadMessage(string message)
    {
        SentMessageToClients(message);
    }

    public void GetMessage()
    {       
        if (!string.IsNullOrWhiteSpace(textInput.text))
        {
            string input = textInput.text;
            UploadMessage(input);
            textInput.text = "";
        }
    }

    [ClientRpc]
    public void SentMessageToClients(string text)
    {
        GameObject newTextObj = Instantiate(textObject, textContainer.transform);
        newTextObj.GetComponent<Text>().text = text;
    }
}
