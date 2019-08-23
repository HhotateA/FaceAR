using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using VRM;

public class VRMCatcher : MonoBehaviour
{
    private GameObject _VRM;
    [SerializeField] private Transform _Headtarget;
    [SerializeField] private Transform _LeftTarget;
    [SerializeField] private Transform _RightTarget;
    [SerializeField] private Transform _TrackingRoot;
    [SerializeField] private BaseDist mBaseDist;
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private Transform mainC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_VRM){
            try{
                _VRM = transform.Find("VRM").gameObject;
                Debug.Log("read");
                IKSetter(_VRM);
            }catch{}
        }
    }
    void IKSetter(GameObject vrmRoot){
        vrmRoot.AddComponent<PinchInOut>();
        VRMFirstPerson vrmFirstPerson = vrmRoot.GetComponent<VRMFirstPerson>() as VRMFirstPerson;
        TouchRecenter vrmRecenter = vrmRoot.AddComponent<TouchRecenter>() as TouchRecenter;
        vrmRecenter.HeadTarget = _TrackingRoot;
        vrmRecenter.height = vrmFirstPerson.FirstPersonBone.transform.position.y + vrmFirstPerson.FirstPersonOffset.y;
        vrmRecenter.vrm = vrmRoot.transform;
        vrmRecenter.mBaseDist = mBaseDist;
        vrmRecenter.animatorController = animatorController;
        vrmRoot.GetComponent<VRMLookAtHead>().Target = mainC.transform;
        VRIK vrmIK = vrmRoot.AddComponent<VRIK>() as VRIK;
        vrmIK.solver.spine.headTarget = _Headtarget;
        vrmIK.solver.leftArm.target = _LeftTarget;
        vrmIK.solver.rightArm.target = _RightTarget;
        vrmIK.solver.leftArm.positionWeight = 0f;
        vrmIK.solver.leftArm.rotationWeight = 0f;
        vrmIK.solver.leftArm.shoulderRotationWeight = 0f;
        vrmIK.solver.leftArm.shoulderTwistWeight = 0f;
        vrmIK.solver.leftArm.bendGoalWeight = 0f;
        vrmIK.solver.rightArm.positionWeight = 0f;
        vrmIK.solver.rightArm.rotationWeight = 0f;
        vrmIK.solver.rightArm.shoulderRotationWeight = 0f;
        vrmIK.solver.rightArm.shoulderTwistWeight = 0f;
        vrmIK.solver.rightArm.bendGoalWeight = 0f;
    }
    /*
    IEnumerator IKSetting(){
        return ;
    }
    */
}
