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

    public TextMeshProUGUI message;
    public TextMeshProUGUI heading;

    // Start is called before the first frame update
    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;

        MessagePlayer();
    }

    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
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
        int _trialNumberForHumans = trialNum + 1; //lol this is because trialNum starts at 0 (as do all array values) but people don't start counting with zero...mostly...
        message.text = "Round " + _trialNumberForHumans + "/3";

        //NEED TO CODE z
    }

    void newTrial()
    {
        int actualTrialNum = trialNum + 1;
        //Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + trialName + "_" + actualTrialNum.ToString() + "_" + "TrialStartTime", "Start " + System.DateTime.Now);

        SceneManager.LoadScene(trialName);

    }
}
