#region Description

// The script performs a direct translation of the skeleton using only the position data of the joints.
// Objects in the skeleton will be created when the scene starts.

#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerativeGraphicUI : MonoBehaviour
{
	
	public GameObject[] psSlider = new GameObject[0];
	public GameObject[] psuSlider = new GameObject[0];

	public ParticleSystem ps;
	string message = "";
	public nuitrack.JointType[] typeJoint;
	GameObject[] CreatedJoint;
	public GameObject PrefabJoint;

	public GameObject[] particleSystemGos;
	public GameObject PrefabConnection;

	public GameObject[] UpwardParticleSystem;
	public GameObject PrefabUpward;

	public Slider Emission;
	public Slider EmissionUpwards;

	Text textComponent;


	void Start(){
		//ps = GetComponent<ParticleSystem>();
		//Defining Joints
		CreatedJoint = new GameObject[typeJoint.Length];
        for (int q = 0; q < typeJoint.Length; q++){
            CreatedJoint[q] = Instantiate(PrefabJoint);
            CreatedJoint[q].transform.SetParent(transform);
        }

        //Defining Connections
        particleSystemGos = new GameObject[typeJoint.Length];;
        for (int i = 0; i < typeJoint.Length; i++) {
            particleSystemGos[i] = Instantiate(PrefabConnection);
            particleSystemGos[i].transform.SetParent(transform);
        }

        //Defining dissappating effect
        UpwardParticleSystem = new GameObject[7];;
        for (int i = 0; i < 7; i++) {
            UpwardParticleSystem[i] = Instantiate(PrefabUpward);
            UpwardParticleSystem[i].transform.SetParent(transform);
        }

        //Defining slider information
        if (GameObject.FindGameObjectsWithTag ("PS") != null) {
            psSlider = GameObject.FindGameObjectsWithTag ("PS");
            
        }

        if (GameObject.FindGameObjectsWithTag ("PSU") != null) {
            psuSlider = GameObject.FindGameObjectsWithTag ("PSU");
            
        }
        textComponent = GetComponent<Text>();
        message = "Skeleton created";

	}

	// // uses slider to changee how long particles exist for
	// public void Particle_Lifetime_Changed(float newValue){
	// 	for (int i = 0; i < particleSystemGos.Length; i++) {
	// 		ParticleSystem ps = particleSystemGos[i].GetComponent<ParticleSystem>();
	// 		ParticleSystem cj = CreatedJoint[i].GetComponent<ParticleSystem>();
	// 		cj.startLifetime = newValue;
	// 		ps.startLifetime  = newValue;
	// //	}
	// }
	private void ConnectJoints(int j1, int j2, int psIndex)
	{
		Vector3 p1 = CreatedJoint [j1].transform.localPosition;
		Vector3 p2 = CreatedJoint [j2].transform.localPosition;
		Vector3 midpointAtoB = new Vector3((p1.x+p2.x)/2.0f,(p1.y+p2.y)/2.0f,(p1.z+p2.z)/2.0f);
		particleSystemGos[psIndex].transform.position = midpointAtoB;
		particleSystemGos[psIndex].transform.localPosition = midpointAtoB;
	}

	void Update(){
		if (CurrentUserTracker.CurrentUser != 0){
			nuitrack.Skeleton skeleton = CurrentUserTracker.CurrentSkeleton;
			message = "Skeleton found";

			for (int q = 0; q < typeJoint.Length; q++){
				nuitrack.Joint joint = skeleton.GetJoint(typeJoint[q]);
				Vector3 newPosition = 0.001f * joint.ToVector3();
				CreatedJoint[q].transform.localPosition = newPosition;

                if(q == 0){
                    UpwardParticleSystem[0].transform.localPosition = newPosition;
                }
                else if(q == 3){
                    UpwardParticleSystem[1].transform.localPosition = newPosition;
                }
                else if(q == 4){
                    UpwardParticleSystem[2].transform.localPosition = newPosition;
                }
                else if(q == 5){
                    UpwardParticleSystem[3].transform.localPosition = newPosition;
                }
                else if(q == 13){
                    UpwardParticleSystem[4].transform.localPosition = newPosition;
                }
                else if(q == 14){
                    UpwardParticleSystem[5].transform.localPosition = newPosition;
                }
                else if(q == 15){
                    UpwardParticleSystem[6].transform.localPosition = newPosition;
                }
			}
			ConnectJoints (0, 1, 0);
			ConnectJoints (1, 2, 1);
			ConnectJoints (3, 4, 3);
			ConnectJoints (4, 5, 4);
			ConnectJoints (6, 7, 6);
			ConnectJoints (7, 8, 7);
			ConnectJoints (9, 10, 9);
			ConnectJoints (10, 12, 10);
			ConnectJoints (13, 14, 13);
			ConnectJoints (14, 15, 14);

			foreach (GameObject ps_clone in psSlider){
	        	ParticleSystem psssss = ps_clone.GetComponentInChildren<ParticleSystem>();
	        	var em = psssss.emission;
	        	em.enabled = true;
	        	em.rate = Emission.value;
        	}
        	foreach (GameObject ps_clone in psuSlider){
	        	ParticleSystem psssss = ps_clone.GetComponentInChildren<ParticleSystem>();
	        	var em = psssss.emission;
	        	em.enabled = true;
	        	em.rate = EmissionUpwards.value;
        	}
        	//Emission.text = Emission.value.ToString("0.0");
        	//EmissionUpwards.text = EmissionUpwards.value.ToString("0.0");
        	textComponent.text = Mathf.Round(Emission.value * 100).ToString();
		}
		else{
			message = "Skeleton not found";
		}
	}

	// Display the message on the screen
	void OnGUI(){
        /*
		GUI.color = Color.red;
		GUI.skin.label.fontSize = 50;
		GUILayout.Label(message);
        */
	
	}


}
