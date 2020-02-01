using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region UI
    [EnumNamedArray(typeof(EPanel))]
    public List<GameObject> panels;
    EPanel currentActivePanel = EPanel.None;

    public Button gameplayLeftButton,gameplayRightButton;
    public Button menuPlayButton;
    public Button menuCreditsButton;
    public Button menuExitButton;
    public Button gameplayPauseButton;
    public Button gamePlayResumeButton;
    public Button gamePlayExitButton;
    #endregion


    #region Gameplay

    public float rotationTime = 1f;
    public GameObject currentObject;
    float currentYRotation;
    bool canRotate = true;
    public bool inputEnabled = true;
    Tween rotationTween;
    public System.Action OnRotationCompleted;

    #endregion

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

        gameplayLeftButton.onClick.RemoveAllListeners();
        gameplayRightButton.onClick.RemoveAllListeners();
        menuPlayButton.onClick.RemoveAllListeners();
        menuCreditsButton.onClick.RemoveAllListeners();
        menuExitButton.onClick.RemoveAllListeners();
        gameplayPauseButton.onClick.RemoveAllListeners();
        gamePlayResumeButton.onClick.RemoveAllListeners();
        gamePlayExitButton.onClick.RemoveAllListeners();


        gameplayLeftButton.onClick.AddListener(RotateLeft);
        gameplayRightButton.onClick.AddListener(RotateRight);

        menuPlayButton.onClick.AddListener(OnMenuPlayClicked);
        menuCreditsButton.onClick.AddListener(OnMenuCreditsClicked);
        menuExitButton.onClick.AddListener(OnExitClicked);
        gameplayPauseButton.onClick.AddListener(OnGamePlayPauseClicked);
        gamePlayResumeButton.onClick.AddListener(OnGameplayResumeClicked);
        gamePlayExitButton.onClick.AddListener(OnExitClicked);

        DisableAllPanels();
        ActivatePanel(EPanel.MenuPanel);
    }

    void DisableAllPanels()
    {
        for (int i = 0; i < 7; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
    }

    void ActivatePanel(EPanel panel)
    {
        if(currentActivePanel != EPanel.None)
        {
            panels[(int)currentActivePanel].gameObject.SetActive(false);
        }
        if(panel != EPanel.None)
        {
            panels[(int)panel].gameObject.SetActive(true);
        }
    }

    public void GetCurrentGameObject()
    {
        currentObject = PlayerController.instance.control.gameObject;
        currentYRotation = currentObject.transform.rotation.eulerAngles.y;
    }

    public void RotateLeft()
    {      
        if (currentObject != null)
        {
            inputEnabled = false;
            gameplayLeftButton.interactable = false;
            gameplayRightButton.interactable = false;
            currentYRotation -= 90f;
            Quaternion quaternion = Quaternion.Euler(0, currentYRotation, 0);
            rotationTween =  currentObject.transform.DORotateQuaternion(quaternion, rotationTime).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                gameplayLeftButton.interactable = true;
                gameplayRightButton.interactable = true;
            if (OnRotationCompleted != null)
            {
                OnRotationCompleted();
            }
            });
        }
    }

    public void RotateRight()
    {       
        if (currentObject != null)
        {
            inputEnabled = false;
            gameplayLeftButton.interactable = false;
            gameplayRightButton.interactable = false;
            currentYRotation += 90f;
            Quaternion quaternion = Quaternion.Euler(0, currentYRotation, 0);
            rotationTween = currentObject.transform.DORotateQuaternion(quaternion, rotationTime).SetEase(Ease.InOutSine).OnComplete(()=> 
            {
                gameplayLeftButton.interactable = true;
                gameplayRightButton.interactable = true;
            if(OnRotationCompleted != null)
            {
                OnRotationCompleted();
            }
            });
        }
    }

    void OnMenuPlayClicked()
    {
        
    }

    void OnMenuCreditsClicked()
    {
        ActivatePanel(EPanel.CreditsPanel);
    }

    void OnExitClicked()
    {
        Application.Quit();
    }

    void OnGamePlayPauseClicked()
    {
        ActivatePanel(EPanel.PausePanel);
        inputEnabled = false;
    }

    void OnGameplayResumeClicked()
    {
        ActivatePanel(EPanel.GameplayPanel);
        inputEnabled = true;
    }
}

public enum EPanel
{
    GameplayPanel = 0,
    GameOverPanel,
    LevelCompletedPanel,
    GameCompletedPanel,
    MenuPanel,
    CreditsPanel,
    PausePanel,
    None
}
