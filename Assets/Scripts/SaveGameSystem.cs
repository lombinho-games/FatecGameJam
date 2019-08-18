using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public static class SaveGameSystem
{
    public static bool SaveGame(SaveGame saveGame, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector surrogateSelector = new SurrogateSelector();

        //Adiciona a serialização das classes Vector3 e Quaternion
        Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
        surrogateSelector.AddSurrogate(typeof(Vector3),new StreamingContext(StreamingContextStates.All),vector3SS);
        QuaternionSerializationSurrogate quatSS = new QuaternionSerializationSurrogate();
        surrogateSelector.AddSurrogate(typeof(Quaternion),new StreamingContext(StreamingContextStates.All),quatSS);

        formatter.SurrogateSelector = surrogateSelector;


        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveGame);
            }
            catch (Exception e)
            {
                Debug.LogError("Object of type " + saveGame.GetType() + " could not be serialized");
                Debug.LogError(e);
                return false;
            }
        }

        return true;
    }

    public static SaveGame LoadGame(string name)
    {
        if (!DoesSaveGameExist(name))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector surrogateSelector = new SurrogateSelector();

        //Adiciona a serialização das classes Vector3 e Quaternion
        Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
        surrogateSelector.AddSurrogate(typeof(Vector3),new StreamingContext(StreamingContextStates.All),vector3SS);
        QuaternionSerializationSurrogate quatSS = new QuaternionSerializationSurrogate();
        surrogateSelector.AddSurrogate(typeof(Quaternion),new StreamingContext(StreamingContextStates.All),quatSS);

        formatter.SurrogateSelector = surrogateSelector;

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Open))
        {
            try
            {
                return formatter.Deserialize(stream) as SaveGame;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
    }

    public static bool DeleteSaveGame(string name)
    {
        try
        {
            File.Delete(GetSavePath(name));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static bool DoesSaveGameExist(string name)
    {
        return File.Exists(GetSavePath(name));
    }

    private static string GetSavePath(string name)
    {
        string savepath = Path.Combine(Application.persistentDataPath, name + ".sav");
        return savepath;
    }
}