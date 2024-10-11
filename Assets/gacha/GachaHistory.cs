using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using UnityEngine.UI;

public class GachaHistory : MonoBehaviour
{
    string filePath;
    public TextMeshProUGUI historyText;  // Change Text to TextMeshProUGUI
    public Button nextButton, previousButton;  // Buttons for navigating through pages

    private List<string> historyEntries;  // List to store all the history entries
    private int currentPage = 0;  // Track the current page
    private const int entriesPerPage = 5;  // Number of results per page

    void Start()
    {
        // Define the path to the file inside the "Gacha" folder under Assets
        string folderPath = Path.Combine(Application.dataPath, "gacha");
        filePath = Path.Combine(folderPath, "GachaResults.txt");

        // Initialize the history entries list
        historyEntries = new List<string>();

        // Load the history from the file
        LoadGachaHistory();

        // Update the page to show the first set of results
        UpdatePage();

        // Add listeners to the buttons
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);
    }

    void LoadGachaHistory()
    {
        if (File.Exists(filePath))
        {
            // Read all lines from the file into the list
            string[] lines = File.ReadAllLines(filePath);
            historyEntries.AddRange(lines);

            // Log for debugging
            Debug.Log("Loaded Gacha History:\n" + string.Join("\n", lines));
        }
        else
        {
            historyText.text = "No gacha history found.";
            Debug.Log("No gacha history found.");
        }
    }

    void UpdatePage()
    {
        // Calculate the start index for the current page
        int start = currentPage * entriesPerPage;
        
        // Clear the display
        historyText.text = "";

        // Display the entries for the current page
        for (int i = start; i < start + entriesPerPage && i < historyEntries.Count; i++)
        {
            historyText.text += historyEntries[i] + "\n";
        }

        // Update button states (disable "Previous" button on the first page and "Next" button on the last page)
        previousButton.interactable = currentPage > 0;
        nextButton.interactable = (start + entriesPerPage) < historyEntries.Count;
    }

    void NextPage()
    {
        // Go to the next page if available
        if ((currentPage + 1) * entriesPerPage < historyEntries.Count)
        {
            currentPage++;
            UpdatePage();
        }
    }

    void PreviousPage()
    {
        // Go to the previous page if available
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }
}
