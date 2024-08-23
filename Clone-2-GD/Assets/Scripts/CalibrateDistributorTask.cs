using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CalibrateDistributorTask : MonoBehaviour
{
    public Transform ring1, ring2, ring3;
    public Slider slider1, slider2, slider3;
    public GameObject taskPanel; // Add a reference to the task panel
    public float rotationSpeed = 100f; // Adjust the speed as needed
    public float alignmentRange = 20f; // Adjust the alignment range as needed

    public TaskManager taskManager;

    private bool isRing1Calibrated = false;
    private bool isRing2Calibrated = false;
    private bool isRing3Calibrated = false;

    private float ring1Speed;
    private float ring2Speed;
    private float ring3Speed;

    void Start()
    {
        ring1Speed = rotationSpeed;
        ring2Speed = rotationSpeed;
        ring3Speed = rotationSpeed;
        ResetTask();
    }

    void Update()
    {
        if (!isRing1Calibrated)
        {
            RotateRing(ring1, ring1Speed);
        }
        else if (!isRing2Calibrated)
        {
            RotateRing(ring2, ring2Speed);
        }
        else if (!isRing3Calibrated)
        {
            RotateRing(ring3, ring3Speed);
        }
    }

    void RotateRing(Transform ring, float speed)
    {
        ring.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnRingClick(int ringNumber)
    {
        Debug.Log("Ring " + ringNumber + " clicked");

        switch (ringNumber)
        {
            case 1:
                if (IsAligned(ring1))
                {
                    isRing1Calibrated = true;
                    slider1.value = 1;
                    ring1Speed = 0; // Stop the ring
                }
                else
                {
                    ResetTask();
                }
                break;
            case 2:
                if (IsAligned(ring2))
                {
                    isRing2Calibrated = true;
                    slider2.value = 1;
                    ring2Speed = 0; // Stop the ring
                }
                else
                {
                    ResetTask();
                }
                break;
            case 3:
                if (IsAligned(ring3))
                {
                    isRing3Calibrated = true;
                    slider3.value = 1;
                    ring3Speed = 0; // Stop the ring
                }
                else
                {
                    ResetTask();
                }
                break;
        }

        if (isRing1Calibrated && isRing2Calibrated && isRing3Calibrated)
        {
            StartCoroutine(CompleteTask());
        }
    }

    bool IsAligned(Transform ring)
    {
        // Check if the ring's local rotation is between 0 and 20 degrees
        float angle = ring.localRotation.eulerAngles.z;
        return angle >= -40 && angle <= alignmentRange;
    }

    void ResetTask()
    {
        isRing1Calibrated = false;
        isRing2Calibrated = false;
        isRing3Calibrated = false;

        slider1.value = 0;
        slider2.value = 0;
        slider3.value = 0;

        ring1.rotation = Quaternion.identity;
        ring2.rotation = Quaternion.identity;
        ring3.rotation = Quaternion.identity;

        ring1Speed = rotationSpeed;
        ring2Speed = rotationSpeed;
        ring3Speed = rotationSpeed;
    }

    IEnumerator CompleteTask()
    {
        Debug.Log("Calibrate Distributor Task Complete");
        // Add logic to handle task completion
        yield return new WaitForSeconds(1);
        taskPanel.SetActive(false);
        taskManager.numOfTasksCompleted++;
    }
}
