using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class ComputerScreen : MonoBehaviour
{
    [SerializeField]
    private Text questionTxt;

    [SerializeField]
    private List <Button> answerButtons;

    [SerializeField]
    private Question[] questions;

    private static List<Question> unansweredQuestions;
    private List<string> answers;

    private Question currentQuestion;
    private bool isAnswered = false;

    void Start()
    {
        if(unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        GetRandomQuestion();

        for (int i = 0; i < answerButtons.Count; i++)
        {
            Button localBtn = answerButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        answers = currentQuestion.answers;
        for (int i = 0; i < answers.Count; i++)
        {
            answers[i] = currentQuestion.answers[i];
        }

        SetText();

        unansweredQuestions.RemoveAt(randomQuestionIndex);
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
        Debug.Log("Correct");

        //Open door
    }

    private void WrongAnswer()
    {
        Debug.Log("Wrong");

        //Reduce player health

        Player.playerProps.health -= 10f;
        isAnswered = false;
        Start();
    }
}
