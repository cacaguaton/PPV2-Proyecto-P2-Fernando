using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creamos una clase para almacenar tener de referencia los datos
//que vamosa utilizar 
//usamos serializable para que se va en el insupector
[System.Serializable]
public class Leccion
{
    //almacenamos el numero de pregunta
    public int ID;
    //almacenamis la pregunta
    public string lessons;
    //almacenamis las opciones de respuesta
    public List<string> options;
    //almacenamos la respuesta correcta 
    public int correctAnswer;
}
