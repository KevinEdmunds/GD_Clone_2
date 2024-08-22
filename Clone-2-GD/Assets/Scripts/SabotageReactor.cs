using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class SabotageReactor : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public TMP_Text panelText1;
    public TMP_Text panelText2;
    public Button button1;
    public Button button2;
    public SpriteRenderer reactorGlow1;
    public SpriteRenderer reactorGlow2;
    private bool isButton1Held = false;
    private bool isButton2Held = false;
    public bool isReactorSabotaged = false;
    private float timer = 30.0f; 

    void Start()
    {
        AddEventTrigger(button1.gameObject, EventTriggerType.PointerDown, OnButton1Down);
        AddEventTrigger(button1.gameObject, EventTriggerType.PointerUp, OnButton1Up);
        AddEventTrigger(button2.gameObject, EventTriggerType.PointerDown, OnButton2Down);
        AddEventTrigger(button2.gameObject, EventTriggerType.PointerUp, OnButton2Up);
    }

    void Update()
    {
        if (isReactorSabotaged)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                ImposterWins();
            }

            if (!isButton1Held && !isButton2Held)
            {
                panelText1.text = "HOLD  TO  STOP  MELTDOWN";
                panelText2.text = "HOLD  TO  STOP  MELTDOWN";
                reactorGlow1.color = Color.red;
                reactorGlow2.color = Color.red;
            }
            else if (isButton1Held && !isButton2Held || !isButton1Held && isButton2Held)
            {
                panelText1.text = "WAITING  FOR  SECOND  USER";
                panelText2.text = "WAITING  FOR  SECOND  USER";
                reactorGlow1.color = Color.red;
                reactorGlow2.color = Color.red;
            }
            else if (isButton1Held && isButton2Held)
            {
                panelText1.text = "REACTOR  NOMINAL";
                panelText2.text = "REACTOR  NOMINAL";
                reactorGlow1.color = Color.blue;
                reactorGlow2.color = Color.blue;
                StartCoroutine(CompleteTaskWithDelay());
            }
        }
    }

    public void ActivateReactorSabotage()
    {
        isReactorSabotaged = true;
        timer = 30.0f; 
        panel1.SetActive(true);
        panel2.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        panelText1.text = "HOLD  TO  STOP  MELTDOWN";
        panelText2.text = "HOLD  TO  STOP  MELTDOWN";
        reactorGlow1.color = Color.red;
        reactorGlow2.color = Color.red;
    }

    private void OnButton1Down(BaseEventData eventData)
    {
        isButton1Held = true;
        Debug.Log("Reactor 1 Held down");
    }

    private void OnButton1Up(BaseEventData eventData)
    {
        isButton1Held = false;
    }

    private void OnButton2Down(BaseEventData eventData)
    {
        isButton2Held = true;
        Debug.Log("Reactor 2 Held down");
    }

    private void OnButton2Up(BaseEventData eventData)
    {
        isButton2Held = false;
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType type, System.Action<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null) trigger = obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((data) => action((BaseEventData)data));
        trigger.triggers.Add(entry);
    }

    private IEnumerator CompleteTaskWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        CompleteTask();
    }

    private void CompleteTask()
    {
        isReactorSabotaged = false;
        panel1.SetActive(false);
        panel2.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        reactorGlow1.color = Color.red;
        reactorGlow2.color = Color.red;
        timer = 30.0f; 
        Debug.Log("Reactor Sabotage stopped");
    }

    private void ImposterWins()
    {
        isReactorSabotaged = false;
        panel1.SetActive(false);
        panel2.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        reactorGlow1.color = Color.red;
        reactorGlow2.color = Color.red;
        Debug.Log("Imposter Wins!");
       
    }
}
