using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDist : MonoBehaviour
{
    public MeshFilter mMesh;
    [SerializeField] private int leftMouthTop=263;
    [SerializeField] private int leftMouthBottom=269;
    [SerializeField] private int rightMouthTop=12;
    [SerializeField] private int rightMouthBottom=18;
    [SerializeField] private int mouthTop=306;
    [SerializeField] private int mouthBottom=461;
    private float leftLong;
    private float rightLong;
    private float mouthLong;
    [SerializeField] private Vector2 joyClamp = new Vector2(-0.003f,0.001f);
    [SerializeField] private Vector2 mouthClamp = new Vector2(0.006f,0.04f);
    [SerializeField] private Vector2 blinkSpeed = new Vector2(0.2f,0.1f);
    private int blinkCount = 0; // 1 or 2
    [SerializeField] float doubleRate = 0.3f;
    private int blinkState = 0; // -1,up 0,normal 1,down
    [SerializeField] private Vector2 blinkInterval = new Vector2(5f,6f);
    private float nextBlink = 6f;
    public float mBlink {get;set;}
    public float mJoy {get;set;}
    public float mA {get;set;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
        Vector3[] test = mMesh.mesh.vertices;
        leftLong = test[leftMouthTop].y-test[leftMouthBottom].y;
        rightLong = test[rightMouthTop].y-test[rightMouthBottom].y;
        mouthLong = Vector3.Distance(test[mouthTop],test[mouthBottom]);

        mJoy = (Mathf.Clamp((leftLong+rightLong)/2f,joyClamp.x,joyClamp.y)-joyClamp.x) / (joyClamp.y-joyClamp.x);
        mA = (Mathf.Clamp(mouthLong,mouthClamp.x,mouthClamp.y)-mouthClamp.x) / (mouthClamp.y-mouthClamp.x);
    }
    void Blink(){
        nextBlink -= Time.deltaTime;
        if(nextBlink<0f){
            blinkCount = Random.Range(1,3);
            nextBlink = Random.Range(blinkInterval.x,blinkInterval.y);
        }

        switch (blinkState){
            case 0:
                if(blinkCount != 0){
                    blinkCount --;
                    blinkState = 1;
                }
                mBlink = 0f;
                break;
            case 1:
                mBlink = mBlink + (Time.deltaTime/blinkSpeed.x);
                if(mBlink>1f){
                    blinkState = -1;
                    mBlink = 1f;
                }
                break;
            case -1:
                mBlink = mBlink - (Time.deltaTime/blinkSpeed.y);
                if(mBlink<0f){
                    blinkState = 0;
                    mBlink = 0f;
                }
                break;
        }
    }
}
