
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonAvatar : MonoBehaviour
{
    public bool autoProcessing = true;
    [SerializeField] GameObject jointPrefab = null, connectionPrefab = null;

    nuitrack.JointType[] jointsInfo = new nuitrack.JointType[]
    {
        nuitrack.JointType.Head,
        nuitrack.JointType.Neck,
        nuitrack.JointType.LeftCollar,
        nuitrack.JointType.Torso,
        nuitrack.JointType.Waist,
        nuitrack.JointType.LeftShoulder,
        nuitrack.JointType.RightShoulder,
        nuitrack.JointType.LeftElbow,
        nuitrack.JointType.RightElbow,
        nuitrack.JointType.LeftWrist,
        nuitrack.JointType.RightWrist,
        nuitrack.JointType.LeftHand,
        nuitrack.JointType.RightHand,
        nuitrack.JointType.LeftHip,
        nuitrack.JointType.RightHip,
        nuitrack.JointType.LeftKnee,
        nuitrack.JointType.RightKnee,
        nuitrack.JointType.LeftAnkle,
        nuitrack.JointType.RightAnkle
    };

    nuitrack.JointType[,] connectionsInfo = new nuitrack.JointType[,]
    { //Right and left collars are currently located at the same point, that's why we use only 1 collar,
        //it's easy to add rightCollar, if it ever changes
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

    GameObject[] connections;
    Dictionary<nuitrack.JointType, GameObject> joints;

    void Start()
    {
        CreateSkeletonParts();
    }

    void CreateSkeletonParts()
    {
        joints = new Dictionary<nuitrack.JointType, GameObject>();

        for (int i = 0; i < jointsInfo.Length; i++)
        {
            if (jointPrefab != null)
            {
                GameObject joint = Instantiate(jointPrefab, transform, true);
                joint.SetActive(false);
                joints.Add(jointsInfo[i], joint);
            }
        }

        connections = new GameObject[connectionsInfo.GetLength(0)];

        for (int i = 0; i < connections.Length; i++)
        {
            if (connectionPrefab != null)
            {
                GameObject connection = Instantiate(connectionPrefab, transform, true);
                connection.SetActive(false);
                connections[i] = connection;
            }
        }
    }

    void Update()
    {
        if (autoProcessing)
            ProcessSkeleton(CurrentUserTracker.CurrentSkeleton);
    }

    public void ProcessSkeleton(nuitrack.Skeleton skeleton)
    {
        if (skeleton == null)
            return;

        for (int i = 0; i < jointsInfo.Length; i++)
        {
            nuitrack.Joint j = skeleton.GetJoint(jointsInfo[i]);
            if (j.Confidence > 0.5f)
            {
                joints[jointsInfo[i]].SetActive(true);
                joints[jointsInfo[i]].transform.position = new Vector2(j.Proj.X * Screen.width, Screen.height - j.Proj.Y * Screen.height);
            }
            else
            {
                joints[jointsInfo[i]].SetActive(false);
            }
        }

        for (int i = 0; i < connectionsInfo.GetLength(0); i++)
        {
            GameObject startJoint = joints[connectionsInfo[i, 0]];
            GameObject endJoint = joints[connectionsInfo[i, 1]];

            if (startJoint.activeSelf && endJoint.activeSelf)
            {
                connections[i].SetActive(true);

                connections[i].transform.position = startJoint.transform.position;
                connections[i].transform.right = endJoint.transform.position - startJoint.transform.position;
                float distance = Vector3.Distance(endJoint.transform.position, startJoint.transform.position);
                connections[i].transform.localScale = new Vector3(distance, 1f, 1f);
            }
            else
            {
                connections[i].SetActive(false);
            }
        }
    }
}
/*
#region Description

// The script performs a direct translation of the skeleton using only the position data of the joints.
// Objects in the skeleton will be created when the scene starts.

#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSkeletonAvatar : MonoBehaviour
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


    void Start()
    {
        //ps = GetComponent<ParticleSystem>();
        //Defining Joints
        CreatedJoint = new GameObject[typeJoint.Length];
        for (int q = 0; q < typeJoint.Length; q++)
        {
            CreatedJoint[q] = Instantiate(PrefabJoint);
            CreatedJoint[q].transform.SetParent(transform);
        }

        //Defining Connections
        particleSystemGos = new GameObject[typeJoint.Length]; ;
        for (int i = 0; i < typeJoint.Length; i++)
        {
            particleSystemGos[i] = Instantiate(PrefabConnection);
            particleSystemGos[i].transform.SetParent(transform);
        }

        //Defining dissappating effect
        UpwardParticleSystem = new GameObject[7]; ;
        for (int i = 0; i < 7; i++)
        {
            UpwardParticleSystem[i] = Instantiate(PrefabUpward);
            UpwardParticleSystem[i].transform.SetParent(transform);
        }

        //Defining slider information
        if (GameObject.FindGameObjectsWithTag("PS") != null)
        {
            psSlider = GameObject.FindGameObjectsWithTag("PS");

        }

        if (GameObject.FindGameObjectsWithTag("PSU") != null)
        {
            psuSlider = GameObject.FindGameObjectsWithTag("PSU");

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
        Vector3 p1 = CreatedJoint[j1].transform.localPosition;
        Vector3 p2 = CreatedJoint[j2].transform.localPosition;
        Vector3 midpointAtoB = new Vector3((p1.x + p2.x) / 2.0f, (p1.y + p2.y) / 2.0f, (p1.z + p2.z) / 2.0f);
        particleSystemGos[psIndex].transform.position = midpointAtoB;
        particleSystemGos[psIndex].transform.localPosition = midpointAtoB;
    }

    void Update()
    {
        if (CurrentUserTracker.CurrentUser != 0)
        {
            nuitrack.Skeleton skeleton = CurrentUserTracker.CurrentSkeleton;
            message = "Skeleton found";

            for (int q = 0; q < typeJoint.Length; q++)
            {
                nuitrack.Joint joint = skeleton.GetJoint(typeJoint[q]);
                Vector3 newPosition = 0.001f * joint.ToVector3();
                CreatedJoint[q].transform.localPosition = newPosition;

                if (q == 0)
                {
                    UpwardParticleSystem[0].transform.localPosition = newPosition;
                }
                else if (q == 3)
                {
                    UpwardParticleSystem[1].transform.localPosition = newPosition;
                }
                else if (q == 4)
                {
                    UpwardParticleSystem[2].transform.localPosition = newPosition;
                }
                else if (q == 5)
                {
                    UpwardParticleSystem[3].transform.localPosition = newPosition;
                }
                else if (q == 13)
                {
                    UpwardParticleSystem[4].transform.localPosition = newPosition;
                }
                else if (q == 14)
                {
                    UpwardParticleSystem[5].transform.localPosition = newPosition;
                }
                else if (q == 15)
                {
                    UpwardParticleSystem[6].transform.localPosition = newPosition;
                }
            }
            ConnectJoints(0, 1, 0);
            ConnectJoints(1, 2, 1);
            ConnectJoints(3, 4, 3);
            ConnectJoints(4, 5, 4);
            ConnectJoints(6, 7, 6);
            ConnectJoints(7, 8, 7);
            ConnectJoints(9, 10, 9);
            ConnectJoints(10, 12, 10);
            ConnectJoints(13, 14, 13);
            ConnectJoints(14, 15, 14);

            foreach (GameObject ps_clone in psSlider)
            {
                ParticleSystem psssss = ps_clone.GetComponentInChildren<ParticleSystem>();
                var em = psssss.emission;
                em.enabled = true;
                em.rate = Emission.value;
            }
            foreach (GameObject ps_clone in psuSlider)
            {
                ParticleSystem psssss = ps_clone.GetComponentInChildren<ParticleSystem>();
                var em = psssss.emission;
                em.enabled = true;
                em.rate = EmissionUpwards.value;
            }
            //Emission.text = Emission.value.ToString("0.0");
            //EmissionUpwards.text = EmissionUpwards.value.ToString("0.0");
            textComponent.text = Mathf.Round(Emission.value * 100).ToString();
        }
        else
        {
            message = "Skeleton not found";
        }
    }

    // Display the message on the screen
    void OnGUI()
    {

        GUI.color = Color.red;
        GUI.skin.label.fontSize = 50;
        GUILayout.Label(message);

    }


}

*/