using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDist : MonoBehaviour
{
    public MeshFilter mMesh;
    [SerializeField] private int faceTop=97;
    [SerializeField] private int faceBottom=167;
    [SerializeField] private int leftEyeTop=263;
    [SerializeField] private int leftEyeBottom=269;
    [SerializeField] private int rightEyeTop=12;
    [SerializeField] private int rightEyeBottom=18;
    [SerializeField] private int mouthTop=306;
    [SerializeField] private int mouthBottom=461;

    public float faceLong;
    public float leftLong;
    public float rightLong;
    public float mouthLong;
    public Text face;
    public Text right;
    public Text left;
    public Text mouth;
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] test = mMesh.mesh.vertices;
        faceLong = Vector3.Distance(test[faceTop],test[faceBottom]);
        leftLong = Vector3.Distance(test[leftEyeTop],test[leftEyeBottom]);
        rightLong = Vector3.Distance(test[rightEyeTop],test[rightEyeBottom]);
        mouthLong = Vector3.Distance(test[mouthTop],test[mouthBottom]);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] test = mMesh.mesh.vertices;
        faceLong = Vector3.Distance(test[faceTop],test[faceBottom]);
        leftLong = Vector3.Distance(test[leftEyeTop],test[leftEyeBottom]);
        rightLong = Vector3.Distance(test[rightEyeTop],test[rightEyeBottom]);
        mouthLong = Vector3.Distance(test[mouthTop],test[mouthBottom]);
        face.text = faceLong.ToString();
        right.text = rightLong.ToString();
        left.text = leftLong.ToString();
        mouth.text = mouthLong.ToString();
        
    }
}
