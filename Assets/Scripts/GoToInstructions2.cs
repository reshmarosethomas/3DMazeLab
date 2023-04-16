using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GoToInstructions2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Start_Instructions2();
        }
    }

    void Start_Instructions2()
    {
        SceneManager.LoadScene("Instructions2");
    }
}
