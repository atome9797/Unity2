using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceSwitch : MonoBehaviour
{
    ARFaceManager arFaceManager;

    public Material[] materials;

    private int switchCount = 0;

    void Start()
    {
        arFaceManager = GetComponent<ARFaceManager>();

        arFaceManager.facePrefab.GetComponent<MeshRenderer>().material = materials[0];
    }

    void SwitchFaces()
    {
        //추적 가능한 얼굴들을 for문 돌릴예정
        foreach(ARFace face in arFaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = materials[switchCount];
        }

        switchCount = (switchCount + 1) % materials.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SwitchFaces();
        }
    }
}
