using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    // objeto para patron singleto
    public static SaveSystem Instance;

    public Leccion data;
    public SubjectContainer subject;
    //()PATRON SINGLETO ES UN PATRON DE DISEÑO, ENCARGADO DE CREAR UNA INSTANCIA DE LA CLASE 
    //PARA SER REFERENCIA DA EN OTRA CLASE SIN LA NECESIDAD DE DECLARAR LAS VARIABLES
    //Osea que que hace que sea referenciado el objeto en lugar de crear otro en otros scrips
    private void Awake()
    {
    // si está asignad lo deja así
        if (Instance != null)
        {
            return;
        }
        //pero si no esta asignado lo asigna 
        else
        {
            Instance = this;
        }
        subject = LoadFromJSON<SubjectContainer>(PlayerPrefs.GetString("SelectedLesson"));
    }
    //lo que se activa al iniciar el juego
    private void Start()
    {
        /*
        //creamos el archivo 
        CreateFile("Posicion",".txt");
        //buscamos el archivo y lo mostramos en consola
        Debug.Log(ReadFile("Posicion", ".txt"));*/

        //SaveToJSON("LeccionDummy", data);

        
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
            //escribe la fecha
            string content = "login Date: " + System.DateTime.Now + "\n";
            //escribe la posision 
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
    //Esta lee el archivo
    public string ReadFile(string _fileName, string _extension)
    {
        //1.- Acceder al path del archivo
        string path = Application.dataPath + "/StreamingAssets/" + _fileName + _extension;
        // 2.- Si el archivo existe, dame su info
        string data = "";
        if (File.Exists(path)) 
        {
        //asigna la informacion dentro del archivo a la variable data
            data = File.ReadAllText(path);
        }
        //regresa data ya con la información 
        return data;
        
    }
    //Este convierte de un fichero a un archivo JSON
    public void SaveToJSON(string _fileName, object _data)
    {
    //si el onbjeto no tiene información 
        if (_data != null)
        {
            //convierte el string a tipo JSON, ponemos verdadero para facilitar su lectura
            string JSONData = JsonUtility.ToJson(_data, true);
            //si los nuenros de la cadena son diferentes a 0
            if(JSONData.Length != 0)
            {
                //imprime en consola
                Debug.Log("JSON STRING: " + JSONData);
                //Le asigna nombre al JSON
                string fileName = _fileName + ".json";
                //busca el archivo original 
                string filePath = Path.Combine(Application.dataPath + "/StreamingAssets/", fileName);
                //Sobre escribe en el archivo original 
                File.WriteAllText(filePath, JSONData);
                //dice donde se almaceno
                Debug.Log("JSON almacenado en la direccion: " + filePath);
            }
            //si la cadena es 0
            else 
            {
            //manda esta advertencia 
                Debug.LogWarning("ERROR - FileSystem: JSONData is empty, check for local variable [string JSONData]");
            }
        }
        //si ya tiene informacion
        else
        {
            // manda una advertencia 
            Debug.LogWarning("ERROR - FileSystem: _data is null, check for param [object _data]");
        }
    }

    public T LoadFromJSON<T>(string _fileName) where T: new()
    {
        T Dato = new T();
        string path = Application.dataPath + "/StreamingAssets/" + _fileName + ".json";
        string JSONSData = "";
        if (File.Exists(path))
        {
            JSONSData = File.ReadAllText(path);
            Debug.Log("JSON STRING: " + JSONSData);
        }
        if (JSONSData.Length != 0)
        {
            JsonUtility.FromJsonOverwrite(JSONSData, Dato);
        }
        else
        {
            Debug.LogWarning("ERROR - FileSystem: JSONData is empty, check for local variable [string JSONData]");
        }
        return Dato;
    }
}
