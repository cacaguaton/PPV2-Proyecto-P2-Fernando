using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    // objeto para patron singleto
    public static SaveSystem Instance;

    //()PATRON SINGLETO ES UN PATRON DE DISEÑO, ENCARGADO DE CREAR UNA INSTANCIA DE LA CLASE 
    //PARA SER REFERENCIA DA EN OTRA CLASE SIN LA NECESIDAD DE DECLARAR LAS VARIABLES
    //Osea que que hace que sea referenciado el objeto en lugar de crear otro en otros scrips
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
    private void Start()
    {
        CreateFile("Posicion",".txt");
        Debug.Log(ReadFile("Posicion", ".txt"));
    }

    //Clase que crea el archivo
    public void CreateFile(string _name, string _extension)
    {
        //1.-Definir el path del archivo
        string path = Application.dataPath + "/" + _name + _extension;
        //2.- Revisamos, si, el archivo e el path NO existe
        if (!File.Exists(path))
        {
            //3.- Creamos contenido
            string content = "login Date: " + System.DateTime.Now + "\n";
            string position = "x: " + gameObject.transform.position.x + 
                ", y: " + gameObject.transform.position.y;
            //4.- Almacenamos la informacion
            File.AppendAllText(path, position);
        }
        else
        {
            //Mandamos un aviso, si ya existe un archivo con ese nombre
            Debug.LogWarning("Atencion: Estas tratando de crear un archivo con el mismo nombre " +
                "[  " + _name + _extension + "], verifica tu informacion");
        }
    }

    public string ReadFile(string _fileName, string _extension)
    {
        //1.- Acceder al path del archivo
        string path = Application.dataPath + "/Resources/" + _fileName + _extension;
        // 2.- Si el archivo existe, dame su info
        string data = "";
        if (File.Exists(path)) 
        {
            data = File.ReadAllText(path);
        }
        return data;
        
    }

    public void SaveToJSON(string _fileName, object _data)
    {
        if (_data != null)
        {
            string JSONData = JsonUtility.ToJson(_data, true);
            
            if(JSONData.Length != 0)
            {
                Debug.Log("JSON STRING: " + JSONData);
                string fileName = _fileName + ".json";
                string filePath = Path.Combine(Application.dataPath + "/Resources/JSONS/", fileName);
                File.WriteAllText(filePath, JSONData);
                Debug.Log("JSON almacenado en la direccion: " + filePath);
            }
            else 
            {
                Debug.LogWarning("ERROR - FileSystem: JSONData is empty, check for local variable [string JSONData]");
            }
        }
        else
        {
            Debug.LogWarning("ERROR - FileSystem: _data is null, check for param [object _data]");
        }
    }

}
