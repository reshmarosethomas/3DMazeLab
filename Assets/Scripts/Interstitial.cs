using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Interstitial : MonoBehaviour
{
    public int trialNum;
    public string trialName;
    public List<string> trials;
    public float lastTimeTaken;
    public float bestTimeTaken;

    public TextMeshProUGUI message;
    public TextMeshProUGUI heading;
    public TextMeshProUGUI lastTime;
    public TextMeshProUGUI bestTime;

    // Start is called before the first frame update
    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;
        lastTimeTaken = GlobalControl.Instance.lastTimeTaken;
        bestTimeTaken = GlobalControl.Instance.bestTimeTaken;

        MessagePlayer();
    }

    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
        GlobalControl.Instance.lastTimeTaken = lastTimeTaken;
        GlobalControl.Instance.bestTimeTaken = bestTimeTaken;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            newTrial();
        }
    }

    void MessagePlayer()
    {   
        //Add text with best time info, and challenge player to beat it
        UnityEngine.Debug.Log("INTERSTITIAL: Last Time Taken: " + lastTimeTaken.ToString());
        UnityEngine.Debug.Log("INTERSTITIAL: Best Time Taken: " + bestTimeTaken.ToString());

        int _trialNumberForHumans = trialNum; //This is because trialNum starts at 0 (as do all array values) but people don't start counting with zero...mostly...
        message.text = "Round " + _trialNumberForHumans + "/5";
        
        //Convert Last Time Taken to Readable Format
        float seconds1 = Mathf.Floor(lastTimeTaken);
        float minutes1 = Mathf.Floor(seconds1/60);   
        float milliseconds1 = Mathf.Floor((lastTimeTaken - seconds1)*100);
            seconds1 = seconds1%60; 
        
        //Convert BEST Time Taken to Readable Format
        float seconds2 = Mathf.Floor(bestTimeTaken);
        float minutes2 = Mathf.Floor(seconds2/60);   
        float milliseconds2 = Mathf.Floor((lastTimeTaken - seconds2)*100);
            seconds2 = seconds2%60; 

        if (trialNum <= 1)
        {
            heading.text = "You've completed Practice! Ready to play?";
            lastTime.text = "Time Taken: " + minutes1.ToString() + "m :" + seconds1.ToString() + "s :" + milliseconds1.ToString() + "ms";
            bestTime.text = "";
        }

        else 
        {
            heading.text = "Can you beat your best score?";
            lastTime.text = "Time Taken: " + minutes1.ToString() + "m :" + seconds1.ToString() + "s :" + milliseconds1.ToString() + "ms";
            bestTime.text = "Best Time: " + minutes2.ToString() + "m :" + seconds2.ToString() + "s :" + milliseconds2.ToString() + "ms";
        }
        
    }

    void newTrial()
    {
        int actualTrialNum = trialNum + 1;
        //Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + trialName + "_" + actualTrialNum.ToString() + "_" + "TrialStartTime", "Start " + System.DateTime.Now);
        SceneManager.LoadScene(trialName);
    }
}
