using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int levelNo;
    public GameObject button;
     List<GameObject> questionButtons= new List<GameObject>();
    //public List<Text> questionTexts;
    public Text answerText;
    public GameObject QuestionPanel;
    public List<Level> levels = new List<Level>();

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if(instance!=null)
            {
                Destroy(this);
            }
        }
    }

    private void Start()
    {
        for(int i=0;i<levels[levelNo].questions.Count;++i)
        {
            GameObject quesButton=  Instantiate(button, QuestionPanel.transform) as GameObject;
            quesButton.transform.GetChild(0).GetComponent<Text>().text = levels[levelNo].questions[i];
            QuestionNumber questionNumberScript = quesButton.GetComponent<QuestionNumber>();
            questionNumberScript.questionNo = i;
            quesButton.GetComponent<Button>().onClick.AddListener(() => { QuestionNo(questionNumberScript.questionNo); });
            questionButtons.Add(quesButton);

        }
        answerText.gameObject.SetActive(false);
    }
    
    public void QuestionNo(int no)
    {
        print(no);
        Destroy(questionButtons[no]);
        QuestionPanel.SetActive(false);
        answerText.text = levels[levelNo].answers[no];
        answerText.gameObject.SetActive(true);
    }


    public void OnAnswerclicked()
    {
        answerText.gameObject.SetActive(false);
        QuestionPanel.SetActive(true);
        if(QuestionPanel.transform.childCount==0)
        {
            answerText.text = "start puzzle";
            answerText.gameObject.SetActive(true);
        }

    }

    public int GetCurrentLevel()
    {
        return levelNo;
    }

    public void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("Current_Level", levelNo);
    }

}
[System.Serializable]
public class Level
{
    public List<string> questions= new List<string>();
    public List<string> answers = new List<string>();
}
