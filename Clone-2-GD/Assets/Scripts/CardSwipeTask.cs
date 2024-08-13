using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSwipeTask : MonoBehaviour
{
    public GameObject taskPanel;
    public Slider swipeSlider;
    public SpriteRenderer cardSprite;
    public float swipeSpeed = 1.0f;
    public float sliderSpeedFactor = 0.5f; 
    public float minSwipeTime = 0.5f; 
    public float maxSwipeTime = 1.5f; 
    public TaskSuccessText textAnimator; 
    private bool isSwiping = false;
    private Vector2 originalPosition;
    private float swipeStartTime;

    void Start()
    {
        originalPosition = cardSprite.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (cardSprite.bounds.Contains(mousePos))
            {
                isSwiping = true;
                swipeStartTime = Time.time;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
            cardSprite.transform.position = originalPosition;
            swipeSlider.value = swipeSlider.minValue;
        }

        if (isSwiping)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cardSprite.transform.position = new Vector2(mousePos.x, cardSprite.transform.position.y);
            swipeSlider.value = Mathf.Clamp((mousePos.x - originalPosition.x) / (swipeSpeed * sliderSpeedFactor), swipeSlider.minValue, swipeSlider.maxValue);

            if (swipeSlider.value >= swipeSlider.maxValue)
            {
                isSwiping = false;
                float swipeTime = Time.time - swipeStartTime;
                if (swipeTime >= minSwipeTime && swipeTime <= maxSwipeTime)
                {
                    OnSwipeComplete();
                }
                else
                {
                    Debug.Log("Swipe Failed: Incorrect Speed");
                }
            }
        }
    }

    private void OnSwipeComplete()
    {
        Debug.Log("Swipe Complete!");
        taskPanel.gameObject.SetActive(false);
        textAnimator.gameObject.SetActive(true); 
        textAnimator.SetText("Task Completed!"); 
        textAnimator.AnimateText(); 
    }

    void ExitTask()
    {
        taskPanel.gameObject.SetActive(false);
    }
}