using UnityEngine;
using UnityEditor;
using System.IO;

public class SoundNameReader : MonoBehaviour
{
    [MenuItem("Tools/Print Sound Names")]
    static void PrintSoundNames()
    {
        // Đường dẫn tới thư mục chứa các file âm thanh
        string folderPath = "Assets/Sound/Voice";

        // Lấy danh sách các tệp âm thanh trong thư mục
        string[] soundFiles = Directory.GetFiles(folderPath, "*.ogg", SearchOption.AllDirectories);

        string nameSound = string.Empty;

        // Lặp qua từng tên tệp và hiển thị tên trong Debug
        foreach (string filePath in soundFiles)
        {
            // Lấy tên tệp từ đường dẫn
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            nameSound += "\n" + fileName + ",";
        
        }
        Debug.Log(nameSound);

    }
}
