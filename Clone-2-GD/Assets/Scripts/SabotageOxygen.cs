using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SabotageOxygen : MonoBehaviour
{
    public KeypadController keypad1;
    public KeypadController keypad2;
    public float timeLimit = 50.0f;
    private float timer;
    public bool sabotageOxygenActive = false;
    private int correctEntries = 0;
    private bool isCooldown = false; // Add a cooldown flag

    public Button sabotageButton; // Add a reference to the sabotage button
    public CanvasGroup sabotageButtonCanvasGroup; // Add a reference to the CanvasGroup of the sabotage button

    void Update()
    {
        if (sabotageOxygenActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Time's up, imposters win
                Debug.Log("Imposters win!");

                // Add more logic to for when imposters win

               // sabotageOxygenActive = false;
            }
        }
  
    }

    public void StartSabotage()
    {
        if (!isCooldown) // Check if cooldown is active
        {
            Debug.Log("StartSabotage called");
            timer = timeLimit;
            sabotageOxygenActive = true;
            correctEntries = 0;
            keypad1.SetGeneratedCode(keypad1.generatedCode);
            keypad2.SetGeneratedCode(keypad1.generatedCode);
            StartCoroutine(CooldownCoroutine()); // Start the cooldown coroutine
        }
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

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;
        Debug.Log("Is on Cooldown");
        sabotageButtonCanvasGroup.alpha = 0.5f; // Set initial button opacity to half
        sabotageButton.interactable = false; // Disable button interaction

        float elapsedTime = 0f;
        float cooldownDuration = 30f;

        while (elapsedTime < cooldownDuration)
        {
            elapsedTime += Time.deltaTime;
            sabotageButtonCanvasGroup.alpha = Mathf.Lerp(0.5f, 1.0f, elapsedTime / cooldownDuration); // Gradually increase opacity
            yield return null;
        }

        sabotageButtonCanvasGroup.alpha = 1.0f; // Ensure button opacity is fully restored
        sabotageButton.interactable = true; // Enable button interaction
        isCooldown = false;
        Debug.Log("Cooldown finished");
    }
}
