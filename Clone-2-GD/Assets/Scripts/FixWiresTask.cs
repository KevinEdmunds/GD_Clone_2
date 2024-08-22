using UnityEngine;
using System.Collections;

public class FixWiresTask : MonoBehaviour
{
    public GameObject[] leftPositions; // Array size 4
    public GameObject[] rightPositions; // Array size 4
    public GameObject[] wirePrefabs; // Array size 4
    public GameObject panel;

    private void Start()
    {
        // Randomly assign wires to positions
        ShuffleArray(leftPositions);
        ShuffleArray(rightPositions);

        for (int i = 0; i < wirePrefabs.Length; i++)
        {
            GameObject leftWire = Instantiate(wirePrefabs[i], leftPositions[i].transform.position, Quaternion.identity);
            GameObject rightWire = Instantiate(wirePrefabs[i], rightPositions[i].transform.position, Quaternion.identity);

            // Assign the WireDrag script to the instantiated wires
            leftWire.AddComponent<FixWiresDrag>();
            rightWire.AddComponent<FixWiresDrag>();

            // Tag the right wire positions as WireTarget
            rightWire.tag = "WireTarget";
        }
    }

    private void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    public void CheckTaskCompletion()
    {
        bool allConnected = true;
        FixWiresDrag[] wires = FindObjectsOfType<FixWiresDrag>();

        foreach (FixWiresDrag wire in wires)
        {
            if (wire.transform.position != wire.originalPosition && !wire.isDragging)
            {
                allConnected = false;
                break;
            }
        }

        if (allConnected)
        {
            StartCoroutine(CompleteTaskWithDelay());
        }
    }

    private IEnumerator CompleteTaskWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        panel.SetActive(false);
        Debug.Log("Wiring Task Completed!");
    }
}
