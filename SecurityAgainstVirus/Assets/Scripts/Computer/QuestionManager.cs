using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("List of all the questions with answers")]
    [SerializeField] private Question[] questions;

    public static List<Question> unansweredQuestions;

    /// <summary>
    /// If there are no more unanswered questions (or it is null), the questions will 
    /// once again be added to the unansweredQuestions list
    /// </summary>
    public void SetQuestions()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

    }
}
