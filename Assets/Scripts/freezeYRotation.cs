using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeYRotation : MonoBehaviour
{   
    private Vector3 startRotation;

    void Awake() 
    {
        startRotation = transform.rotation.eulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            newRotation.x,
            startRotation.y,
            newRotation.z
        );

    }

}
