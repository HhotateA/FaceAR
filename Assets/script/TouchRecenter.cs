﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;
using UnityEngine.UI;
using VRM;

public class TouchRecenter : MonoBehaviour {
    public Transform HeadTarget;
    public float height {set;get;}
    public Transform vrm {set;get;} 
    public BaseDist mBaseDist;
    public RuntimeAnimatorController animatorController;
    private VRMBlendShapeProxy mVRMBlendShapeProxy;
    private Animator mAnimator;
    private float mSorrow;
    private float mAngry;
    private float mFun;
    private float mJoy;
    private float shotTime = 0f;
    public VRMCatcher mVRMCatcher;
    public PinchInOut mPinchiInOut;


    // Use this for initialization
    void Start () {
        mJoy = 1f;
        mVRMBlendShapeProxy = this.GetComponent<VRMBlendShapeProxy>();
        mAnimator = this.GetComponent<Animator>();
        mAnimator.runtimeAnimatorController = animatorController;
        StartCoroutine(UpdataShape());
    }
    
    // Update is called once per frame
    void Update () {
        height = mPinchiInOut.avatarHeight;
        vrm.position = new Vector3(HeadTarget.position.x,HeadTarget.position.y-height,HeadTarget.position.z);
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
            mVRMBlendShapeProxy.AccumulateValue(new BlendShapeKey(BlendShapePreset.A), mBaseDist.mA);
            mVRMBlendShapeProxy.AccumulateValue(new BlendShapeKey(BlendShapePreset.Blink), mBaseDist.mBlink);
            mVRMBlendShapeProxy.AccumulateValue(new BlendShapeKey(BlendShapePreset.Joy), mJoy*mBaseDist.mJoy);
            mVRMBlendShapeProxy.AccumulateValue(new BlendShapeKey(BlendShapePreset.Angry), mAngry);
            mVRMBlendShapeProxy.AccumulateValue(new BlendShapeKey(BlendShapePreset.Sorrow), mSorrow);
            mVRMBlendShapeProxy.AccumulateValue(new BlendShapeKey(BlendShapePreset.Fun), mFun);
            mVRMBlendShapeProxy.Apply();

    }
    IEnumerator UpdataShape(){
        while(true){

            if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended){
                mAnimator.SetInteger ("Random", Random.Range(0,2));
                switch((int)Input.touchCount){
                    case 1:
                        yield return new WaitForSeconds(0.5f);
                        mJoy = 1f;
                        mSorrow = 0f;
                        mAngry = 0f;
                        mFun = 0f;
                        break;
                    case 2:
                        yield return new WaitForSeconds(0.5f);
                        mJoy = 0f;
                        mSorrow = 0f;
                        mAngry = 0f;
                        mFun = 1f;
                        break;
                    case 3:
                        yield return new WaitForSeconds(0.5f);
                        mJoy = 0f;
                        mSorrow = 0f;
                        mAngry = 1f;
                        mFun = 0f;
                        break;
                    case 4:
                        yield return new WaitForSeconds(0.5f);
                        mJoy = 0f;
                        mSorrow = 1f;
                        mAngry = 0f;
                        mFun = 0f;
                        break;
                }
            }
            yield return mVRMCatcher.screenShot;
        }
    }
}