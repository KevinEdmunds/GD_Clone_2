using UnityEngine;

public class RingClickHandler : MonoBehaviour
{
    public int ringNumber;
    public CalibrateDistributorTask calibrateDistributorTask;

    void OnMouseDown()
    {
        calibrateDistributorTask.OnRingClick(ringNumber);
    }
}
