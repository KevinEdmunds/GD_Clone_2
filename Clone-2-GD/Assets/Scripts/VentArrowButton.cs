using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentArrowButton : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchVent()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().TransportPlayerVent(ref target);
    }
}
