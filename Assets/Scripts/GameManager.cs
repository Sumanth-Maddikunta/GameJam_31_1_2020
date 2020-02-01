using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }       
    }

    public float rotationTime = 1f;
    public GameObject currentObject;
    float currentYRotation;
    bool canRotate = true;
    Tween rotationTween;

    
    public void GetCurrentGameObject()
    {
        currentObject = PlayerController.instance.control.gameObject;
    }

    public void RotateLeft()
    {      
        if (currentObject != null && (rotationTween.IsComplete() || rotationTween == null))
        {
            currentYRotation -= 90f;
            Quaternion quaternion = Quaternion.Euler(0, currentYRotation, 0);
            rotationTween =  currentObject.transform.DORotateQuaternion(quaternion, rotationTime).SetEase(Ease.InOutSine);
        }
    }

    public void RotateRight()
    {       
        if (currentObject != null && (rotationTween.IsComplete() || rotationTween == null))
        {
            currentYRotation += 90f;
            Quaternion quaternion = Quaternion.Euler(0, currentYRotation, 0);
            rotationTween = currentObject.transform.DORotateQuaternion(quaternion, rotationTime).SetEase(Ease.InOutSine);
        }
    }
}
