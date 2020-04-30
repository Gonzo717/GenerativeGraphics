#region Description

// The script performs a direct translation of the skeleton using only the position data of the joints.
// Objects in the skeleton will be created when the scene starts.

#endregion
    using System.Collections.Generic;
    using UnityEngine;

    public class NativeAvatar : MonoBehaviour
    {
		//public ParticleSystem[] connectionParticles;

		public ParticleSystem[] connectionParticleSystem;

        string message = "";
        public nuitrack.JointType[] typeJoint;
        GameObject[] CreatedJoint;
        public GameObject PrefabJoint;

        public nuitrack.JointType[,] typeConnection = new nuitrack.JointType[,]
        {
        {nuitrack.JointType.Neck,           nuitrack.JointType.Head},
        {nuitrack.JointType.LeftCollar,     nuitrack.JointType.Neck},
        {nuitrack.JointType.LeftCollar,     nuitrack.JointType.LeftShoulder},
        {nuitrack.JointType.LeftCollar,     nuitrack.JointType.RightShoulder},
        {nuitrack.JointType.LeftCollar,     nuitrack.JointType.Torso},
        {nuitrack.JointType.Waist,          nuitrack.JointType.Torso},
        {nuitrack.JointType.Waist,          nuitrack.JointType.LeftHip},
        {nuitrack.JointType.Waist,          nuitrack.JointType.RightHip},
        {nuitrack.JointType.LeftShoulder,   nuitrack.JointType.LeftElbow},
        {nuitrack.JointType.LeftElbow,      nuitrack.JointType.LeftWrist},
        {nuitrack.JointType.LeftWrist,      nuitrack.JointType.LeftHand},
        {nuitrack.JointType.RightShoulder,  nuitrack.JointType.RightElbow},
        {nuitrack.JointType.RightElbow,     nuitrack.JointType.RightWrist},
        {nuitrack.JointType.RightWrist,     nuitrack.JointType.RightHand},
        {nuitrack.JointType.LeftHip,        nuitrack.JointType.LeftKnee},
        {nuitrack.JointType.LeftKnee,       nuitrack.JointType.LeftAnkle},
        {nuitrack.JointType.RightHip,       nuitrack.JointType.RightKnee},
        {nuitrack.JointType.RightKnee,      nuitrack.JointType.RightAnkle}
        };
        GameObject[] CreatedConnection;
        public GameObject PrefabConnection;

        void Start(){
            CreatedJoint = new GameObject[typeJoint.Length];
            for (int q = 0; q < typeJoint.Length; q++){
                CreatedJoint[q] = Instantiate(PrefabJoint);
                CreatedJoint[q].transform.SetParent(transform);
            }


            CreatedConnection = new GameObject[typeConnection.GetLength(0)];
			connectionParticleSystem = new ParticleSystem[typeConnection.GetLength (0)];
            for (int q = 0; q < typeConnection.GetLength(0); q++){

                if (PrefabConnection != null)
				{
                    GameObject connection = Instantiate(PrefabConnection, transform, true);
					connection.SetActive(true);
                    CreatedConnection[q] = connection;
					// create particle system here
					ParticleSystem ps = new ParticleSystem();
					connectionParticleSystem [q] = ps;
                }
                // CreatedConnection[q] = Instantiate(PrefabJoint);
                // CreatedConnection[q].transform.SetParent(transform);
            }
            message = "Skeleton created";
        }

        void Update(){
            if (CurrentUserTracker.CurrentUser != 0){
                nuitrack.Skeleton skeleton = CurrentUserTracker.CurrentSkeleton;
                message = "Skeleton found";

                for (int q = 0; q < typeJoint.Length; q++){
                    nuitrack.Joint joint = skeleton.GetJoint(typeJoint[q]);
                    Vector3 newPosition = 0.001f * joint.ToVector3();
                    CreatedJoint[q].transform.localPosition = newPosition;
                }
				for (int i = 0; i < typeConnection.GetLength(0); i++){

                    nuitrack.Joint startJoint = skeleton.GetJoint(typeConnection[i, 0]);
                    nuitrack.Joint endJoint = skeleton.GetJoint(typeConnection[i, 1]);

                    CreatedConnection[i].transform.position = startJoint.ToVector3();
                    CreatedConnection[i].transform.right = endJoint.ToVector3() - startJoint.ToVector3();
                    float distance = Vector3.Distance(endJoint.ToVector3(),  startJoint.ToVector3());
                    CreatedConnection[i].transform.localScale = new Vector3(distance, 1f, 1f);
				Debug.Log (CreatedConnection [i].transform.position.ToString());
					connectionParticleSystem[i].transform.position = CreatedConnection[i].transform.position;
                }
            }
            else{
                message = "Skeleton not found";
            }
        }

        // Display the message on the screen
        void OnGUI(){
            GUI.color = Color.red;
            GUI.skin.label.fontSize = 50;
            GUILayout.Label(message);
        }
    }
