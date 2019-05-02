using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubstractingTime : MonoBehaviour {
    public Text timeLeft;
    private float _ActualTime;
    private int minutes = 0;
	// Use this for initialization
	void Start () {
        _ActualTime = 0f;
    }

    // Update is called once per frame
    void Update () {
        if(Time.fixedTime >= 59.5 + (minutes * 59.5))
        {
            minutes++;
        }
            timeLeft.text = (minutes).ToString() + ":" + (Time.fixedTime -minutes*60).ToString("F0");
    }
    
}
