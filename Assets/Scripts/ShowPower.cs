using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class ShowPower : MonoBehaviour {

    Image powerIndicator;

    ScoreManager scManager;
	// Use this for initialization
	void Start ()
	{
	    scManager = ScoreManager.Instance;
        powerIndicator = transform.Find("foreground").GetComponent<Image>();
	    powerIndicator.fillAmount = (float) scManager.power / scManager.maxPower;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    powerIndicator.fillAmount = (float) scManager.power / scManager.maxPower;
	}
}
