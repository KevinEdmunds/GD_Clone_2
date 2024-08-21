using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class NetworkCardSwipeTask : NetworkBehaviour
{
    public Slider swipeSlider;
    public SpriteRenderer cardSprite;
    public float swipeSpeed = 1.0f;
    private bool isSwiping = false;
    private Vector2 originalPosition;

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
                CmdStartSwipe();
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
            swipeSlider.value = Mathf.Clamp((mousePos.x - originalPosition.x) / swipeSpeed, swipeSlider.minValue, swipeSlider.maxValue);

            if (swipeSlider.value >= swipeSlider.maxValue)
            {
                isSwiping = false;
                OnSwipeComplete();
            }
        }
    }

    [Command]
    public void CmdStartSwipe()
    {
        RpcStartSwipe();
    }

    [ClientRpc]
    private void RpcStartSwipe()
    {
        isSwiping = true;
        swipeSlider.value = swipeSlider.minValue;
    }

    private void OnSwipeComplete()
    {
        Debug.Log("Swipe Complete!");
       
    }
}