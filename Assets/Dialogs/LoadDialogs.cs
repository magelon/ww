using System.IO;
using UnityEngine;

public class LoadDialogs : MonoBehaviour
{
    public Dialog[] dias;

    void Start()
    {
        LoadDialogFromFile("Assets/Dialogs/chapter1.txt");
    }

    void LoadDialogFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        dias = new Dialog[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            if (parts.Length == 2)
            {
                dias[i] = new Dialog();
                dias[i].who = parts[0].Trim(); // Character name
                dias[i].lines = parts[1].Trim(); // Dialogue text
            }
        }
    }
}
