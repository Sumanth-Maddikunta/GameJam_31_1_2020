using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Button leftButton,rightButton;

    public System.Action OnRotationCompleted;

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

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButton.onClick.AddListener(RotateLeft);
        rightButton.onClick.AddListener(RotateRight);

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
            OnRotationCompleted();
        }
    }

    public void RotateRight()
    {       
        if (currentObject != null && (rotationTween.IsComplete() || rotationTween == null))
        {
            currentYRotation += 90f;
            Quaternion quaternion = Quaternion.Euler(0, currentYRotation, 0);
            rotationTween = currentObject.transform.DORotateQuaternion(quaternion, rotationTime).SetEase(Ease.InOutSine);
            OnRotationCompleted();

        }
    }
}
