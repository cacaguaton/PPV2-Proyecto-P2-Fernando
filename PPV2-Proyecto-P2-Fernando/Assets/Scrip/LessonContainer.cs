using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LessonContainer : MonoBehaviour
{
//modificamos el numero de lecciones y en que leccion se encuentra
    [Header("GameObject Configuration")]
    public int Lection = 0;
    public int CurrentLession = 0;
    public int TotalLessions = 0;
    //ponemos que la leccion no esta completada para no tener problemas 
    public bool AreAllLessionsComplete = false;

    [Header("UI Configuration")]
//la usamos para cambiar la pregunta en el UI
    public TMP_Text StageTitle;
    public TMP_Text LessonStage;

    [Header("External GameObject Configuration")]
    //para poder referenciar el objeto que tiene los botones
    public GameObject lessonContainer;

//referenciamos las preguntas que tiene nuestro scriptableobject
    [Header("Lesson Data")]
    public ScriptableObject lesson;



    // Start is called before the first frame update
    void Start()
    {
    //si cuenta con lessonContainer
        if (lessonContainer != null)
        {
        //va actualizar el Ui par evitar estar actualizando todo el tiempo 
            OnUptateUI();
        }
        else
        {
        //manda mensaje si no tiene lo requerido
            Debug.LogWarning("GameObject Nulo, revisa las variables de tipo GameObject lessonContainer");
        }
    }
//desarrollamos nuestro actualizador de ui
    public void OnUptateUI()
    {
    
        //si ya le asignanos dentro del inspector los textos 
        if (StageTitle != null || LessonStage != null)
        {
        //actualiza los textos a los de la leccion actual
            StageTitle.text = "Leccion " + Lection;
            LessonStage.text = "Leccion " + CurrentLession + " de " + TotalLessions;
        }
        else
        {
        //avisa que no seles que no se les asigno un objeto de tipo texto en el inspector 
            Debug.LogWarning("GameObject Nulo, revisa las variables de tipo TMP_Text");
        }
    }

    //Este metodo activa/desactiva la ventana de lessonContainer
    public void EnableWindows()
    {
        OnUptateUI();

        if(lessonContainer.activeSelf)
        {
            // Desactiva el objeto si está activo
            lessonContainer.SetActive(false);
        }
        else
        {
            // Activa el objeto si está desactivado
            lessonContainer.SetActive(true);
        }
    }

}
