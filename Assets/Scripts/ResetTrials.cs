using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrials : MonoBehaviour
{
    public int trialNum;
    public string trialName;
    public List<string> trials;

    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;

        //if you want to log the time when the game ended, to compare with the time the game started, use this:
        //Tinylytics.AnalyticsManager.LogCustomMetric("game ended: ", "time:" + System.DateTime.Now);

    }

    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
    }
}
