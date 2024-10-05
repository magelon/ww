using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // For TextMeshPro

public class DisplayDialogs : MonoBehaviour
{
    // Array of dialogues
    public Dialog[] dias;
    private int index;

    // UI elements
    public TMP_Text text;  // TextMeshPro component
    public Image image;

    // Reference to the dialogue file (TextAsset)
    public TextAsset dialogFile;

    void Start()
    {
        // Load dialogues from the TextAsset
        if (dialogFile != null)
        {
            LoadDialogFromTextAsset(dialogFile);
        }
        else
        {
            Debug.LogError("No dialogue file assigned!");
        }

        // Start with the first dialog
        index = 0;
        if (dias.Length > 0)
        {
            DisplayCurrentDialog();
            Invoke("PauseGame", 2f); // Pause after 2 seconds
            HighlightCharacter(dias[0].who); // Initial highlight
        }
        else
        {
            Debug.LogError("No dialogues loaded!");
        }
    }

    private void OnDisable()
    {
        // Resume the game when this object is disabled
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        // Pause the game to keep the dialog visible
        Time.timeScale = 0;
    }

    public void ButtonHandler(GameObject g)
    {
        if (g.name == "next")
        {
            index++;
            // If the dialog has reached the end, disable the dialog display
            if (index >= dias.Length)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                DisplayCurrentDialog();
                HighlightCharacter(dias[index].who);
            }
        }
    }

    private void DisplayCurrentDialog()
    {
         if (dias != null && index >= 0 && index < dias.Length)
        {
        if (dias[index] != null)
        {
            text.text = dias[index].who + ": " + dias[index].lines;
        }
        else
        {
            Debug.LogError("Dialog entry is null at index " + index);
        }
        }
        else
        {
            Debug.LogError("Dialog array is null or index out of bounds");
        }
    }

    private void HighlightCharacter(string characterName)
    {
        Sprite itemSprite = Resources.Load<Sprite>("sumPrefabs/illistrate/" + characterName);
        if (itemSprite != null)
        {
            image.sprite = itemSprite;
            // Modify the alpha value of the image color
            Color col = image.color;
            col.a = 1f;  // Set alpha to 1 (fully opaque)
            image.color = col;  // Assign the modified color back to the image
        }
    }

    // Method to load dialogues from a TextAsset
    private void LoadDialogFromTextAsset(TextAsset textAsset)
    {
        if (textAsset == null)
        {
        Debug.LogError("TextAsset is null");
        return;
        }

        string[] lines = textAsset.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
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
        else
        {
            Debug.LogWarning("Incorrect dialogue format on line " + (i + 1));
        }
        }

        if (dias.Length == 0)
        {
        Debug.LogError("No dialogs loaded from the text asset");
        }
    }
}
