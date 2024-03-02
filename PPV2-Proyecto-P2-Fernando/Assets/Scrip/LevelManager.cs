using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    public Subject Lesson;

    [Header("User Interface")]
    public TMP_Text QuestionTxt;
    public List<Optionbtn> Options;

    [Header("GameConfiguration")]
    public int questionAmount = 0;
    public int currentQuestion = 0;
    public string question;
    public string correctAnswer;
    [Header("Current Lesson")]
    public Leccion currentLesson;
    // Start is called before the first frame update
    void Start()
    {
        //Establecemos la cantidad de preguntas en la leccin
        questionAmount = Lesson.leccionList.Count;
        //Carga la primera pregunta 
        LoadQuestion();

    }

    private void LoadQuestion()
    {
        // Aseguramos que la pregunta actual este dentro de los limites
        if(currentQuestion < questionAmount)
        {
            // Establecemos la leccion actual
            currentLesson = Lesson.leccionList[currentQuestion];
            //Establecemos la pregunta
            question = currentLesson.lessons;
            //Establecemos la respuesta correcta
            correctAnswer = currentLesson.options[currentLesson.correctAnswer];
            //Establecemos la pregunta en UI
            QuestionTxt.text = question;
            // Establecemos las opciones
            Options[0].transform.GetComponent<Optionbtn>().OptionName= currentLesson.options[0];

        }
        else
        {
            // Si llegamos al final de las preguntas
            Debug.Log("Fin de las preguntas");
        }
    }

public void NextQuestion()
    {
        if (currentQuestion < questionAmount)
        {
            //Incrementamos el indice de la pregunta actual
            currentQuestion++;
            // Cargar la nueva pregunta
            LoadQuestion();
        }
        else
        {
            //cambio de escena
        }

    }
}
