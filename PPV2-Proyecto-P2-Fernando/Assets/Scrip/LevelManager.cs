using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{//iNSTANCIA DE LA CLASE 
    public static LevelManager Instance;
    [Header("Level Data")]
    //espacio para aaignar el scriptableobject
    public Subject Lesson;

    [Header("User Interface")]
    //titulo de la pregunta
    public TMP_Text QuestionTxt;
    //vidas
    public TMP_Text livesTXt;
    //espacio para asignar los botones
    public List<Optionbtn> Options;
    //boton para verificar respuesta 
    public GameObject CheckButton;
    //
    public GameObject AnswerContainer;
    
    //para poner si es buena
    public Color Green;
    //o mala
    public Color Red;

    [Header("GameConfiguration")]
    //cantidad de preguntas 
    public int questionAmount = 0;
    //pregunta actual
    public int currentQuestion = 0;
    //la pregunta 
    public string question;
    //la respuesta
    public string correctAnswer;
    //respuesta para dar a entender que no a respondido
    //y despues cbiar por la respuesta dada por el usuario 
    public int answerFromPlayer = 9;
    //asignacion de vidas
    public int lives = 5;

    [Header("Current Lesson")]
    //leccion actual 
    public Leccion currentLesson;


    //()PATRON SINGLETO ES UN PATRON DE DISEÑO, ENCARGADO DE CREAR UNA INSTANCIA DE LA CLASE 
    //PARA SER REFERENCIA DA EN OTRA CLASE SIN LA NECESIDAD DE DECLARAR LAS VARIABLES

    private void Awake()
    {
    //si no esta instanciado se instancia
        if (Instance != null)
        {
            return;
        }
        else
        {
    //y si ya esta instanciado se le asigna este
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Establecemos la cantidad de preguntas en la leccin
        questionAmount = Lesson.leccionList.Count;
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
            currentLesson = Lesson.leccionList[currentQuestion];
            //Establecemos la pregunta
            question = currentLesson.lessons;
            //Establecemos la respuesta correcta
            correctAnswer = currentLesson.options[currentLesson.correctAnswer];
            //Establecemos la pregunta en UI
            QuestionTxt.text = question;
            // Establecemos las opciones
            for (int i = 0; i < currentLesson.options.Count; i++) 
            {
            //dependiendo el nimero de opciones se les asigna a cada boton
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
//pasamos a la siguiente pregunta 
public void NextQuestion()
    {
    //si el usuario ya contesto
        if (CheckPlayerState())
        {
            //Revisamos si la respuesta es correcta o no
            bool isCorrect = currentLesson.options[answerFromPlayer] == correctAnswer;
//el que dice al usuario si es correcta o incorrecta dentro del game
            AnswerContainer.SetActive(true);

            //si es correcta el cuadro es verde
            if(isCorrect)
            {
                AnswerContainer.GetComponent<Image>().color = Green;
                Debug.Log("Respuesta correcta.  " + question + ": " + correctAnswer);
            }
            else
            {
            //si la respuesta es incorrecta el cuadro es rojo
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
    //guardamos la respuesta del usuario 
        answerFromPlayer = _answer;
    }

    public bool CheckPlayerState()
    {
    //si el usuario ya contesto el numero sera diferente de 9
        if(answerFromPlayer != 9)
        {
        //si ya contesto se activa el blton para verificar la respuesta 
            CheckButton.GetComponent<Button>().interactable = true;
            CheckButton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else
        {
        // si no se a contestado el boton esta desactivado 
            CheckButton.GetComponent<Button>().interactable = false;
            CheckButton.GetComponent<Image>().color = Color.grey;
            return false;
        }
    }
}
