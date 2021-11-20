using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileSystem /* : MonoBehaviour */ // 상속하면 사용불가
{
    public static void Save<T>(T _data, string _filePath = "")
    {
        string filePath = _filePath;
        if(filePath == "") 
        {
            // 현재는 아이템 데이터 저장을 위해 경로 고정
            filePath = "Assets/Script/FileSystem/ItemSaveData.bin";
        }
        BinaryFormatter format = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        Debug.Log("File saved!");

        format.Serialize(stream, _data);
        stream.Close();
    }
}
