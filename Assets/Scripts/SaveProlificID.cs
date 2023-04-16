using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveProlificID : MonoBehaviour
{
    public TMP_InputField _inputField;
    public static string prolificID;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("InStart");
        _inputField = GameObject.Find("ProlificIDInput").GetComponent<TMP_InputField>();

    }


    public void InputName()
    {
        prolificID = _inputField.text;
    }

    // Update is called once per frame
    void Update()
    {
        prolificID = _inputField.text;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(prolificID);
            //Tinylytics.AnalyticsManager.LogCustomMetric("Prolific ID", prolificID);
            Screen.fullScreen = true;
            Start_Opening();
        }
    }

    void Start_Opening()
    {
        SceneManager.LoadScene("Instructions1");
    }
}