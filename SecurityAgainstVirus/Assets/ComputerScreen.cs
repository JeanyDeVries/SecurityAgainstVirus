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
    private Button answer1, answer2, 
        answer3, answer4;

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;
    private int correctAnswer;

    void Start()
    {
        if(unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        GetRandomQuestion();
    }

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        questionTxt.text = currentQuestion.question;

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }

    public void CheckIfCorrectAnswer()
    {
        switch(correctAnswer)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    void CorrectAnswer()
    {
        Debug.Log("Correct");

        //Open door
    }

    void WrongAnswer()
    {
        Debug.Log("Wrong");

        //Reduce player health

        Player.playerProps.health -= 10f;
    }
}
