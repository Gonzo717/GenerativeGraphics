using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {

	public ParticleSystem particleSystem;

	void Start(){
		//particleSystem.emissionRate = 0.0f;
		//float val = 1.0f;
		particleSystem.startSize = 1.0f;

	}

	public void Slider_Changed(float newValue){
	//	particleSystem.emissionRate = newValue;
		particleSystem.startSize = newValue;

	}

	public void Button_Click()
	{
		Debug.Log ("hello, world");
	}
}
