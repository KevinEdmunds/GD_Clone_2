using UnityEngine;
using TMPro;
using System.Collections;

public class TaskSuccessText : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float pauseDuration = 0.5f;
    public Transform centerTarget; 
    public Transform endTarget; 
    public TextMeshProUGUI completionText;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -Screen.height / 2);
        gameObject.SetActive(false);
    }

    public void AnimateText()
    {
        StartCoroutine(AnimateTextCoroutine());
    }

    private IEnumerator AnimateTextCoroutine()
    {
        
        while (Vector2.Distance(rectTransform.position, centerTarget.position) > 0.1f)
        {
            gameObject.SetActive(true);
            rectTransform.position = Vector2.MoveTowards(rectTransform.position, centerTarget.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

       
        yield return new WaitForSeconds(pauseDuration);

       
        while (Vector2.Distance(rectTransform.position, endTarget.position) > 0.1f)
        {
            rectTransform.position = Vector2.MoveTowards(rectTransform.position, endTarget.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

       
        gameObject.SetActive(false);
    }

    public void SetText(string text)
    {
        completionText.text = text;
    }
}