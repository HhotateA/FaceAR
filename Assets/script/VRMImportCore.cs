using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using UniGLTF;
using VRM;
using RootMotion.FinalIK;

namespace HOTATE.AROsanpo{
    public class VRMImportCore : MonoBehaviour
    {
         public string mURL;
        public GameObject VRMRoot;
        public Transform faceTarget;

        void Start()
        {
        }

        public void LoadVRMfromInput()
        {
            string url = mURL;
            StartCoroutine(URL2VRM(GoogleUrl(url)));
        }
        public void LoadVRMfromURL(string url)
        {
            StartCoroutine(URL2VRM(GoogleUrl(url)));
        }
        public string GoogleUrl(string url)
        {
            if (url == null || url == "") return url;
            if (url.StartsWith("https://drive.google.com/open?id="))
            {
                url = url.Remove(0, 33);
            }
            else if (url.StartsWith("https://drive.google.com/file/d/"))
            {
                url = url.Remove(0, 32);
                if (url.EndsWith("/view?usp=sharing")){
                    url = url.Remove(url.Length - 17);
                }else if(url.EndsWith("/view?usp=drivesdk")){
                    url = url.Remove(url.Length - 18);
                }
            }
            else if (url.StartsWith("https://drive.google.com/uc?id="))
            {
                url = url.Remove(0, 31);
            }
            else
            {
                return url;
            }
            return "https://drive.google.com/uc?id=" + url;
        }
        private IEnumerator URL2VRM(string url)
        {

            UnityWebRequest www = UnityWebRequest.Get(url);
            www.timeout = 10;
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
            Destroy(VRMRoot);
            try {
                VRMRoot = VRMImporter.LoadFromBytes(www.downloadHandler.data);
            }catch {
                Debug.Log("MissingURL");
            }
            VRMRoot.transform.position = new Vector3(0,-10,0);
            VRMRoot.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).parent = faceTarget;
            VRMRoot.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).localPosition = Vector3.zero;
            VRMRoot.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).rotation = faceTarget.rotation;
            /*
            VRMRoot.transform.position = new Vector3(0,-1,-0.1f);
            VRMRoot.transform.Rotate(new Vector3(0,180,0));
            //VRMRoot.transform.parent = faceTarget;
            VRIK vrik = VRMRoot.AddComponent<VRIK>() as VRIK;
            vrik.solver.spine.headTarget = faceTarget;
            */


        }
    }
}