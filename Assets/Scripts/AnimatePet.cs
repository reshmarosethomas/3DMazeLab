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
    float period = 2f;
    bool faceFront = false;
    
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


        //UnityEngine.Debug.Log(transform.localEulerAngles);
        //Euler Angles should change from 145-face character to 0-face front

        if (faceFront && (transform.localEulerAngles.y > 5 || transform.localEulerAngles.y < -5))
        {
            UnityEngine.Debug.Log("Pet face front");
            transform.Rotate(new Vector3(0, -degreesPerSecond, 0) * Time.deltaTime);
        }
        else if (!faceFront && (transform.localEulerAngles.y < 140 || transform.localEulerAngles.y > 150))
        {
            UnityEngine.Debug.Log("Pet face front");
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }
    }
}
