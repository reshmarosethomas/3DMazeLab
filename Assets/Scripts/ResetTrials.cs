using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrials : MonoBehaviour
{
    public int trialNum;
    public string trialName;
    public List<string> trials;
    public float lastTimeTaken;
    public float bestTimeTaken;

    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;
        lastTimeTaken = GlobalControl.Instance.lastTimeTaken;
        bestTimeTaken = GlobalControl.Instance.bestTimeTaken;

        //if you want to log the time when the game ended, to compare with the time the game started, use this:
        //Tinylytics.AnalyticsManager.LogCustomMetric("game ended: ", "time:" + System.DateTime.Now);

    }

    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
        GlobalControl.Instance.lastTimeTaken = lastTimeTaken;
        GlobalControl.Instance.bestTimeTaken = bestTimeTaken;
    }
}
