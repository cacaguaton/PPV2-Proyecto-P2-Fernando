using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creamos la base del scriptableobject
[CreateAssetMenu(fileName = "New Subject", menuName = "ScriptableObjects/New_Lesson", order = 1)]
public class Subject : ScriptableObject
{
//asignamos numero de leccion
    [Header("GameObject Configuration")]
    public int Lesson = 0;
    //le damos la estructura que le pusimos en la leccion y creamos una lista de esta
    [Header("Lession Quest Configuration")]
    public List<Leccion> leccionList;

}
