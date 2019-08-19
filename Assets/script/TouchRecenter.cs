using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;

public class TouchRecenter : MonoBehaviour {
    public Transform HeadTarget; 
    public float height = 0.9f;
    public Transform vrm ; 


    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        /*
        TrackableHit hit;
        if (Frame.Raycast(HeadTarget.position + new Vector3(0f,1f,0f),new Vector3(0f,-1f,0f),out hit,100,TrackableHitFlags.PlaneWithinPolygon)){
            if (hit.Trackable is DetectedPlane)
            {
                height = hit.Pose.position.y;
            }
        }
        vrm.position = new Vector3(vrm.position.x,height,vrm.position.z);
        */
        vrm.position = new Vector3(vrm.position.x,HeadTarget.position.y-height,vrm.position.z);
    }
}