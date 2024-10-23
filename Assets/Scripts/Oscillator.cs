using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float peroid = 2f;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (peroid <= Mathf.Epsilon) {return;}
        float cycles = Time.time / peroid; // contiunally growing over time 

        const float tau = Mathf.PI * 2;  // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1 

        movementFactor = (rawSinWave + 1f) / 2f; // recalcualted to go from 0 to 1 its cleaner (starting location isnt middle) 

        Vector3 offset = movementVector * movementFactor; 
        transform.position = startingPos + offset;
    }
}
