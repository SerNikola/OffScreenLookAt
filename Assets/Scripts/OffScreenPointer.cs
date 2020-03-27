using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenPointer : MonoBehaviour
{
    public float margin = 5f;
    [HideInInspector]
    public Transform target;


    Vector3 screenPos;
    Vector2 onScreenPos;
    float max;
    Camera mainCamera;
    RectTransform rectTrans;
    Canvas canvas;

    void Start()
    {
        rectTrans = transform as RectTransform;
        mainCamera = Camera.main;
        UpdatePosition();
        UpdateRotation();
    }

    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }
    void UpdatePosition()
    {
        screenPos = mainCamera.WorldToViewportPoint(target.position);
        
        if(screenPos.z <0)
        {
            screenPos *= -1;
        }



        onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping

        //calculate new Position
        float newX = onScreenPos.x * Screen.width;
        float newY = onScreenPos.y * Screen.height;

        //Clamp to keep inside screen
        float pivotDiffX = rectTrans.sizeDelta.x * rectTrans.pivot.x;
        float pivotDiffY = rectTrans.sizeDelta.y * rectTrans.pivot.y;
        float clampedX = Mathf.Clamp(newX, margin + pivotDiffX, Screen.width - pivotDiffX - margin);
        float clampedY = Mathf.Clamp(newY, margin + pivotDiffY, Screen.height - pivotDiffY - margin);
        rectTrans.anchoredPosition = new Vector2(clampedX, clampedY);
    }
    void UpdateRotation()
    {
        var targetPosLocal = mainCamera.transform.InverseTransformPoint(target.position);
        var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }
}
