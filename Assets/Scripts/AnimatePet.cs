using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AnimatePet : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform frontWaypoint;

    Vector3 currPlayerPos;
    Vector3 prevPlayerPos;

    Stopwatch watchPos = new();

    float currTime = 0f; 
    float prevTime = 0f;
    float period = 1f;

    bool faceFront = false;
    float degreesPerSecond = 240f;

    bool up = true;
    float bobRate = 0.00006f;

    float petLocalYPos = 0f;
    float petLocalXPos = 0f;
    float petLocalZPos = 0f;
    Vector3 originalPos;

    float distFromPlayer = 0f;
    float maxDistFromPlayer = 2.0f;
    float minDistFromPlayer = 1.54f;
    float walkRate = 0.5f;
   
    // Start is called before the first frame update
    void Start()
    {
        watchPos.Start();

        //Initialise with player's starting position
        currPlayerPos = player.position;
        prevPlayerPos = currPlayerPos;

        //Store the original pet position wrt the player capsule
        petLocalYPos = transform.localPosition.y;
        petLocalZPos = transform.localPosition.z;
        petLocalXPos = transform.localPosition.x;
        originalPos = transform.localPosition;
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

        //CHECK DISTANCE
        distFromPlayer = Vector3.Distance (transform.position, frontWaypoint.position);
        UnityEngine.Debug.Log(distFromPlayer);


        //CHANGE DIRECTION PET FACES & WALKS
        //Euler Angles: 145 to face character; 0 to face front

        //01 FACE FRONT & CLOSE GAP
        if (faceFront)
        {   
            //Face Front
            if (transform.localEulerAngles.y > 5 || transform.localEulerAngles.y < -5) 
            {
                transform.Rotate(new Vector3(0, -degreesPerSecond, 0) * Time.deltaTime);
            }

            //Close Gap
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime);

        }

        //02 WALK AHEAD & TURN AROUND
        else if (!faceFront)
        {   
            //WALK AHEAD
            transform.localPosition = Vector3.Lerp(transform.localPosition, frontWaypoint.localPosition, walkRate*Time.deltaTime);
            
            
            //TURN AROUND
            
            if ((transform.localEulerAngles.y < 140 || transform.localEulerAngles.y > 150) && distFromPlayer < 1.0f)
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        }

        


        //BOBBING ANIMATION
        if (transform.localPosition.y >= petLocalYPos + 0.03f) up = false;
        else if (transform.localPosition.y <= petLocalYPos - 0.03f) up = true;

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
