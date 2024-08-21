using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SabotageOxygen : MonoBehaviour
{
    public KeypadController keypad1;
    public KeypadController keypad2;
    public float timeLimit = 30f;
    private float timer;
    private bool sabotageOxygenActive = false;
    private int correctEntries = 0;

    void Update()
    {
        if (sabotageOxygenActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Time's up, imposters win
                Debug.Log("Imposters win!");
                sabotageOxygenActive = false;
            }
        }
    }

    public void StartSabotage()
    {
        Debug.Log("StartSabotage called"); 
        timer = timeLimit;
        sabotageOxygenActive = true;
        correctEntries = 0; 
        keypad1.SetGeneratedCode(keypad1.generatedCode);
        keypad2.SetGeneratedCode(keypad1.generatedCode); 
    }

    public void CheckCodes()
    {
        Debug.Log("CheckCodes called"); 
        Debug.Log("Keypad1 Input Code: " + keypad1.inputCode); 
        Debug.Log("Keypad2 Input Code: " + keypad2.inputCode); 
        Debug.Log("Generated Code: " + keypad1.generatedCode);

        if (keypad1.inputCode == keypad1.generatedCode)
        {
            correctEntries++;
            keypad1.inputCode = ""; 
            keypad1.UpdateInputText();
        }

        if (keypad2.inputCode == keypad2.generatedCode)
        {
            correctEntries++;
            keypad2.inputCode = ""; 
            keypad2.UpdateInputText();
        }

        if (correctEntries >= 2)
        {
            // Both codes are correct, sabotage stopped
            Debug.Log("Sabotage Stopped!");
            sabotageOxygenActive = false;
            correctEntries = 0; 
        }
    }
}