using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{//iNSTANCIA DE LA CLASE 
    public static LevelManager Instance;
    [Header("Level Data")]
    public SubjectContainer subject;

    [Header("User Interface")]
    public TMP_Text QuestionTxt;
    public TMP_Text livesTXt;
    public List<Optionbtn> Options;
    public GameObject CheckButton;
    public GameObject AnswerContainer;
    public Color Green;
    public Color Red;

    [Header("GameConfiguration")]
    public int questionAmount = 0;
    public int currentQuestion = 0;
    public string question;
    public string correctAnswer;
    public int answerFromPlayer = 9;
    public int lives = 5;

    [Header("Current Lesson")]
    public Leccion currentLesson;


    //()PATRON SINGLETO ES UN PATRON DE DISEÑO, ENCARGADO DE CREAR UNA INSTANCIA DE LA CLASE 
    //PARA SER REFERENCIA DA EN OTRA CLASE SIN LA NECESIDAD DE DECLARAR LAS VARIABLES

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        subject = SaveSystem.Instance.subject;
        //Establecemos la cantidad de preguntas en la leccin
        questionAmount = subject.leccionList.Count;
        //Carga la primera pregunta 
        LoadQuestion();
        // Check player input
        CheckPlayerState();
    }

    private void LoadQuestion()
    {
        // Aseguramos que la pregunta actual este dentro de los limites
        if(currentQuestion < questionAmount)
        {
            // Establecemos la leccion actual
            currentLesson = subject.leccionList[currentQuestion];
            //Establecemos la pregunta
            question = currentLesson.lessons;
            //Establecemos la respuesta correcta
            correctAnswer = currentLesson.options[currentLesson.correctAnswer];
            //Establecemos la pregunta en UI
            QuestionTxt.text = question;
            // Establecemos las opciones
            for (int i = 0; i < currentLesson.options.Count; i++) 
            {
                Options[i].GetComponent<Optionbtn>().OptionName = currentLesson.options[i];
                Options[i].GetComponent<Optionbtn>().OptionID = i;
                Options[i].GetComponent<Optionbtn>().UptateText();
            }
        }
        else
        {
            // Si llegamos al final de las preguntas
            Debug.Log("Fin de las preguntas");
        }
    }

public void NextQuestion()
    {
        if (CheckPlayerState())
        {
            //Revisamos si la respuesta es correcta o no
            bool isCorrect = currentLesson.options[answerFromPlayer] == correctAnswer;

            AnswerContainer.SetActive(true);

            if(isCorrect)
            {
                AnswerContainer.GetComponent<Image>().color = Green;
                Debug.Log("Respuesta correcta.  " + question + ": " + correctAnswer);
            }
            else
            {
                AnswerContainer.GetComponent<Image>().color = Red;
                Debug.Log("Respuesta Incorrecta.  " + question + ": " + correctAnswer);
                lives--;
            }

            //Actualizamos el contador de vida
            livesTXt.text = lives.ToString();

            //Incrementamos el indice de la pregunta actual
            currentQuestion++;

            //Mostramos el resultado durante un tiempo (puedes usar una corutine o invoke)
            StartCoroutine(ShowResultAndLOadQuestion(isCorrect));

            //Reset answer from player
            answerFromPlayer = 9;
        }
        else
        {
            //Cambio de escena
        }

    }

    private IEnumerator ShowResultAndLOadQuestion(bool isCorrect)
    {
        yield return new WaitForSeconds(2.5f); //Ajusta el tiepo que desead mostrar el resultado
        AnswerContainer.SetActive(false);
        //Cargar la nueva pregunta
        LoadQuestion();

        //Activa el boton despues de mostrar el resultado
        //Puedes hacer esto aqui o en Load Question(), depende de tu estrctura
        //por ejemplo, si el boton esta en el mismo GameObject que el script:
        //GetComponent<Button>().interactible = true;
        CheckPlayerState();
    }

    public void SetPlayerAnswer(int _answer)
    {
        answerFromPlayer = _answer;
    }

    public bool CheckPlayerState()
    {
        if(answerFromPlayer != 9)
        {
            CheckButton.GetComponent<Button>().interactable = true;
            CheckButton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else
        {
            CheckButton.GetComponent<Button>().interactable = false;
            CheckButton.GetComponent<Image>().color = Color.grey;
            return false;
        }
    }
}
