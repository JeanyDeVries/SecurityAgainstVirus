using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    private Question[] questions;

    public static List<Question> unansweredQuestions;

    public void SetQuestions()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

    }
}
