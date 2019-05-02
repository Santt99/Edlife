using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainingMoney : MonoBehaviour {
    public float timing;
    private float _InitialTiming;
    public Text moneyAvailable, quantity;
    public int coinValue;
	// Use this for initialization
	void Start () {
        _InitialTiming = timing;
	}
	
	// Update is called once per frame
	void Update () {
        timing -= Time.deltaTime;
        if(timing <=0)
        {
            moneyAvailable.text = (int.Parse(moneyAvailable.text) + int.Parse(quantity.text) * coinValue).ToString();
            timing = _InitialTiming;
        }
         
	}
    
}
