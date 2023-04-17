using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    //TRIAL, SCENE & PROLIFIC
    public int trialNum;
    public string trialName;
    public List<string> trials;
    string prolificID;
    private string sceneName;

    //TRIAL TIMER
    public float trialTimer = 0;
    private bool timerIsActive = true;

    //TO COUNT GEMS
    public static int gemsCollected = 0;
    public TextMeshProUGUI gemCount;
    private int gemGoal_Lobby = 5;
    private int gemGoal_Trial = 15;

    //FOR HEAT MAP
    public string[] positions = new string[1000];
    string heatMapData;
    int posIndex = 0;
    float currTime = 0f, prevTime = 0f;
    float period = 0.250f;
    public Transform player;   

    public int inFullScreen;

    void Awake()
    {
        inFullScreen = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trials[trialNum];
        trials = GlobalControl.Instance.trials;

        prolificID = SaveProlificID.prolificID;

        UnityEngine.Debug.Log("ProlificID:" + prolificID);
        UnityEngine.Debug.Log("Trial Name:" + trialName);
        UnityEngine.Debug.Log("Trial Number:" + trialNum.ToString());

        //player = GameObject.Find("PlayerCapsule").GetComponent<Transform>();
        gemsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive)
        {
            trialTimer += Time.deltaTime;
            currTime = trialTimer;

            if (currTime - prevTime >= period) {
                prevTime = currTime;
                positions[posIndex] = player.position.ToString();
                //UnityEngine.Debug.Log(positions[posIndex]);
                posIndex++;
            }
        }

        if (Screen.fullScreen == false)
        {
            inFullScreen = 0;
        }

    }

    public void updateGemCount()
    {
        gemsCollected++;
        if (trialNum == 0)
        {
            gemCount.text = "GEMS COLLECTED: " + gemsCollected.ToString() + "/5";

        } else
        {
            gemCount.text = "GEMS COLLECTED: " + gemsCollected.ToString() + "/15";
        }
        
        ResetRound();
    }

    public void ResetRound()
    {
        
        UnityEngine.Debug.Log("Trial Name:" + trialNum);
        if ((gemsCollected >= gemGoal_Lobby && trialNum == 0) || (gemsCollected >= gemGoal_Trial && trialNum > 0))
        {
            UnityEngine.Debug.Log("Reset Round If Statement");
            trialNum++;

            int tempTrialNum = trialNum; //so that tinylytics doesn't mess with trialNum
            string tempTrialName = trialName;

            //Log all Tinylytics at Round End

            //1. Time Taken for Round
            Tinylytics.AnalyticsManager.LogCustomMetric(prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "TimeTaken", trialTimer.ToString());

            //2. Log Score
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "GemsCollected", gemsCollected.ToString());

            //3. Heat Map
            heatMapData = string.Join("_", positions);
            UnityEngine.Debug.Log(heatMapData);
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "PlayerHeatMap", heatMapData);

            //5. ExitedFullScreen?
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "InFullScreen", inFullScreen.ToString());

            //6.Log Trial End
            //Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + trialName + "_" + tempTrialNum.ToString() + "_" + "TrialEndTime", "End " + System.DateTime.Now);

            //Debug.Log(blinkyDists);
            UnityEngine.Debug.Log("Round Over!");
            UnityEngine.Debug.Log("Time Taken: " + trialTimer);
            SaveGame();
            newTrial();

            timerIsActive = false;

        }

    }

    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
    }

    void newTrial()
    {
        if (trialNum < trials.Count)
        {   
            trialName = trials[trialNum];
            
            SaveGame();
            sceneName = "InterstitialA"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.
            StartCoroutine(WaitForSceneLoad());
        }
        else
        {
            endGame();
        }
    }

    void endGame()
    {
        //if you want to know how lond the entire set of trials took, you can add your tinyLytics call here
        sceneName = "ending"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
