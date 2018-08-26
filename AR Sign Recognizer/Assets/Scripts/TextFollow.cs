using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour {


    public GameObject targetObject;
    public Camera cameraAR;
    private Vector3 targetObjectPos;
    private RectTransform rt;
    private RectTransform canvasRT;
    private Vector3 targetObjectScreenPos;

    // Use this for initialization
    void Start()
    {
        targetObjectPos = targetObject.transform.position;

        rt = GetComponent<RectTransform>();
        canvasRT = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        ReloadLocation();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadLocation();
    }

    private void ReloadLocation()
    {
        targetObjectScreenPos = cameraAR.WorldToViewportPoint(targetObject.transform.TransformPoint(targetObjectPos));
        rt.anchorMax = targetObjectScreenPos;
        rt.anchorMin = targetObjectScreenPos;
    }
}
