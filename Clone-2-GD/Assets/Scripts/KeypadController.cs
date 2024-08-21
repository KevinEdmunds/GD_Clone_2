using UnityEngine;
using TMPro;
using System.Collections;

public class KeypadController : MonoBehaviour
{
    public TMP_Text inputText;
    public TMP_Text codeDisplayText; 
    public string inputCode = "";
    public string generatedCode { get; private set; }
    private int maxInputLength = 12;

    public SabotageOxygen sabotageOxygen;

    void Start()
    {
        GenerateRandomCode(); 
    }

    public void OnNumberButtonPressed(string number)
    {
        if (inputCode.Length < maxInputLength)
        {
            inputCode += number;
            UpdateInputText();
        }
    }

    public void OnEraseButtonPressed()
    {
        inputCode = "";
        UpdateInputText();
    }

    public void OnConfirmButtonPressed()
    {
        if (inputCode == generatedCode)
        {
            
            Debug.Log("Code correct!");
            sabotageOxygen.CheckCodes();
            StartCoroutine(DisplayCodeAcceptedAndDeactivate());
        }
        else
        {
            
            Debug.Log("Code incorrect!");
            inputCode = "Wrong Code"; 
            UpdateInputText();
            StartCoroutine(ClearInputAfterDelay());
        }
    }

    public void UpdateInputText()
    {
        inputText.text = inputCode;
    }

    private IEnumerator ClearInputAfterDelay()
    {
        yield return new WaitForSeconds(1); 
        inputCode = "";
        UpdateInputText();
    }

    private IEnumerator DisplayCodeAcceptedAndDeactivate()
    {
        inputText.text = "CODE  ACCEPTED"; 
        yield return new WaitForSeconds(1); 
        gameObject.SetActive(false); 
    }

    private void GenerateRandomCode()
    {
        generatedCode = "";
        for (int i = 0; i < 5; i++)
        {
            generatedCode += Random.Range(0, 10).ToString();
        }
        codeDisplayText.text = generatedCode; 
        Debug.Log("Generated Code: " + generatedCode); 
    }

    public void SetGeneratedCode(string code)
    {
        generatedCode = code;
        codeDisplayText.text = generatedCode; 
        Debug.Log("SetGeneratedCode called. Generated Code: " + generatedCode); 
        Debug.Log("Code Display Text: " + codeDisplayText.text); 
    }
}