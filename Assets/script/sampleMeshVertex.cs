using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleMeshVertex : MonoBehaviour
{
    public MeshFilter mMesh;
    public int num=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] test = mMesh.mesh.vertices;
        this.transform.position  = test[num];
    }
}
