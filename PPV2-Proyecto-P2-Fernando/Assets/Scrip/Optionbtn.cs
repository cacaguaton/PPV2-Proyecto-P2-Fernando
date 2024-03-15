using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//scrip para los botones
public class Optionbtn : MonoBehaviour
{
//numero de boton
    public int OptionID;
    //guardamos el nombre de la respuesta por boton
    public string OptionName;
    // Start is called before the first frame update
    void Start()
    {
    //el boton tiene de hijo un texto, lo busca y le sustituye el texto por la respuesta
    //que va en ese boton
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    public void UptateText()
    {
    //cuando se cambia de pregunta se actualiza 
    //el nombre de cada opcion
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    public void SelectOption()
    {
    //asigna la opcion que el usuario escogio
        LevelManager.Instance.SetPlayerAnswer(OptionID);
        //se verifica si ya se escogio una respuesta
        LevelManager.Instance.CheckPlayerState();
    }
}
