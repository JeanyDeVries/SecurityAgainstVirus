using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerScreen : MonoBehaviour
{
    [SerializeField]
    private QuestionManager questionManager;

    [SerializeField]
    private Text questionTxt;

    [SerializeField]
    private List <Button> answerButtons;

    [SerializeField]
    private GameObject door, computer,
        textDisplay;

    private List<string> answers;

    private Question currentQuestion;
    private bool isAnswered = false;
    private bool emissionFinished;

    void Start()
    {
        questionManager.SetQuestions();

        GetRandomQuestion();

        for (int i = 0; i < answerButtons.Count; i++)
        {
            Button localBtn = answerButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    void Update()
    {
        if(emissionFinished && door.GetComponent<DissolveSphere>().value >= 0.99f)
        {
            door.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, QuestionManager.unansweredQuestions.Count);
        currentQuestion = QuestionManager.unansweredQuestions[randomQuestionIndex];

        answers = currentQuestion.answers;
        for (int i = 0; i < answers.Count; i++)
        {
            answers[i] = currentQuestion.answers[i];
        }

        SetText();

        QuestionManager.unansweredQuestions.RemoveAt(randomQuestionIndex);
    }

    void SetText()
    {
        answers = ShuffleList.ShuffleListItems<string>(currentQuestion.answers);

        questionTxt.text = currentQuestion.question;

        for (int i = 0; i < answers.Count; i++)
        {
            answerButtons[i].GetComponent<Text>().text = answers[i];
        }
    }

    void OnClick(Button btn)
    {
        if (!isAnswered)
        {
            isAnswered = true;
            CheckIfCorrectAnswer(btn.GetComponent<Text>().text);
        }
    }

    public void CheckIfCorrectAnswer(string selectedOption)
    {
        if (currentQuestion.correctAnswer == selectedOption)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    private void CorrectAnswer()
    {
        door.GetComponent<DissolveSphere>().enabled = true;
        computer.GetComponent<DissolveSphere>().enabled = true;
        textDisplay.gameObject.SetActive(false);

        emissionFinished = true;
    }

    private void WrongAnswer()
    {
        //Reduce player health
        Player.playerProps.health -= 10f;

        isAnswered = false;
        questionManager.SetQuestions();
        Start();
    }
}
