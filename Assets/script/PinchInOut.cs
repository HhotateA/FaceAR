using UnityEngine;
using System.Collections;

public class PinchInOut : MonoBehaviour {

    //public Camera camera;
    public GameObject go;

    [SerializeField] private float vMin = 1.0f;
    [SerializeField] private float vMax = 5.0f;

    //直前の2点間の距離.
    private float backDist = 0.0f;
    [SerializeField] private float v = 1.5f;

    // Update is called once per frame
    void Start(){
        go = this.gameObject;
        //v = go.transform.localScale.x;
    }
    void Update () {
        // マルチタッチかどうか確認
        if (Input.touchCount >= 2)
        {
            // タッチしている２点を取得
            Touch t1 = Input.GetTouch (0);
            Touch t2 = Input.GetTouch (1);

            //2点タッチ開始時の距離を記憶
            if (t2.phase == TouchPhase.Began)
            {
                backDist = Vector2.Distance (t1.position, t2.position);
            }
            else if (t1.phase == TouchPhase.Moved && t2.phase == TouchPhase.Moved)
            {
                // タッチ位置の移動後、長さを再測し、前回の距離からの相対値を取る。
                float newDist = Vector2.Distance (t1.position, t2.position);
                v = v + (newDist - backDist) / 10000.0f;
                v=Mathf.Clamp(v,vMin,vMax);
                go.transform.localScale = new Vector3(v, v, v);
            }
        }
    }
}