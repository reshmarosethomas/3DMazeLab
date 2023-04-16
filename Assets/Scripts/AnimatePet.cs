using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AnimatePet : MonoBehaviour
{
    public Transform player;
    Vector3 currPlayerPos;
    Vector3 prevPlayerPos;

    Stopwatch watchPos = new();
    float currTime = 0f, prevTime = 0f;
    float period = 1f;
    bool faceFront = false;

    bool up = true;
    float bobRate = 0.00002f;
    
    float degreesPerSecond = 240f;

    // Start is called before the first frame update
    void Start()
    {
        watchPos.Start();
        currPlayerPos = player.position;
        prevPlayerPos = currPlayerPos;
    }

    // Update is called once per frame
    void Update()
    {
        currTime = watchPos.ElapsedMilliseconds/1000;
        currPlayerPos = player.position;

        //SET DIRECTION PET SHOULD FACE
        if (currTime-prevTime >= period)
        {
            if (prevPlayerPos == currPlayerPos)
            {
                //UnityEngine.Debug.Log("Pet face player");
                faceFront = false;

            } else
            {
                //UnityEngine.Debug.Log("Pet face front");
                faceFront = true;
            }

            prevPlayerPos = currPlayerPos;
            prevTime = currTime;
        }


        //CHANGE DIRECTION PET FACES
        //UnityEngine.Debug.Log(transform.localEulerAngles);
        //Euler Angles should change from 145-face character to 0-face front
        if (faceFront && (transform.localEulerAngles.y > 5 || transform.localEulerAngles.y < -5))
        {
            //UnityEngine.Debug.Log("Pet face front");
            transform.Rotate(new Vector3(0, -degreesPerSecond, 0) * Time.deltaTime);
        }
        else if (!faceFront && (transform.localEulerAngles.y < 140 || transform.localEulerAngles.y > 150))
        {
            //UnityEngine.Debug.Log("Pet face front");
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }


        if (transform.localPosition.y >= 0.24f) up = false;
        else if (transform.localPosition.y <= 0.22f) up = true;

        if (up)
        {
            float xPos = transform.localPosition.x;
            float yPos = transform.localPosition.y + bobRate;
            float zPos = transform.localPosition.z;
            transform.localPosition = new Vector3(xPos, yPos, zPos);
        } else if (!up)
        {
            float xPos = transform.localPosition.x;
            float yPos = transform.localPosition.y - bobRate;
            float zPos = transform.localPosition.z;
            transform.localPosition = new Vector3(xPos, yPos, zPos);
        }
    }
}
