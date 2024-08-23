using UnityEngine;
using System.Collections;

public class EmptyGarbageCafe : MonoBehaviour
{
    public SpriteRenderer leverSprite;
    public GameObject leavesParent;
    public GameObject emptyGarbagePanel;
    public TaskManager taskManager;
    public Animator leavesAnimator; 
    
    public BoxCollider2D chuteCollider;
    public float leverMoveSpeed = 2.0f;
    public float requiredHoldTime = 3.0f; 
    private bool isPullingLever = false;
    private Vector2 originalLeverPosition;
    private float holdTimer = 0.0f;

    void Start()
    {
        originalLeverPosition = leverSprite.transform.position;

        
        if (leavesParent != null)
        {
            leavesAnimator = leavesParent.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (leverSprite.bounds.Contains(mousePos))
            {
                isPullingLever = true;
                chuteCollider.enabled = false; 
                if (leavesAnimator != null)
                {
                    leavesAnimator.SetTrigger("StartAnimation"); 
                    leavesAnimator.speed = 1.0f;
                    StartCoroutine(WaitForAnimationToEnd(leavesAnimator, "LeavesFalling"));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPullingLever = false;
          
            ResetLever();
        }

        if (isPullingLever)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            leverSprite.transform.position = new Vector2(leverSprite.transform.position.x, Mathf.Clamp(mousePos.y, originalLeverPosition.y - 1.0f, originalLeverPosition.y));
            holdTimer += Time.deltaTime;

            if (holdTimer >= requiredHoldTime)
            {
                CompleteTask();
            }
        }
    }

    private void ResetLever()
    {
        
        leverSprite.transform.position = originalLeverPosition;

       
        if (leavesAnimator != null)
        {
            leavesAnimator.speed = 0.0f;
        }

       
        chuteCollider.enabled = true;
    }

    private IEnumerator WaitForAnimationToEnd(Animator animator, string EmptyGarbageChuteLeafFall)
    {
       
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(EmptyGarbageChuteLeafFall))
        {
            yield return null;
        }

        
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(EmptyGarbageChuteLeafFall) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        
        CompleteTask();
    }


    public void CompleteTask()
    {
        Debug.Log("Task Completed!");
        isPullingLever = false;
        emptyGarbagePanel.SetActive(false);

        taskManager.numOfTasksCompleted++;
    }
}