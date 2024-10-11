using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using UnityEngine.UI;

public class GachaHistory : MonoBehaviour
{
    public TextMeshProUGUI historyText;  // Change Text to TextMeshProUGUI
    public Button nextButton, previousButton;  // Buttons for navigating through pages

    private List<string> historyEntries;  // List to store all the history entries
    private int currentPage = 0;  // Track the current page
    private const int entriesPerPage = 5;  // Number of results per page

    void Start()
    {
        // Initialize the history entries list
        historyEntries = new List<string>();

        // Load the history from PlayerPrefs
        LoadGachaHistory();

        // Update the page to show the first set of results
        UpdatePage();

        // Add listeners to the buttons
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);
    }

    // Load Gacha History from PlayerPrefs instead of a file
    void LoadGachaHistory()
    {
        int resultCounter = PlayerPrefs.GetInt("ResultCounter", 0);  // Get the current number of gacha results

        if (resultCounter == 0)
        {
            historyText.text = "No gacha history found.";
            Debug.Log("No gacha history found.");
            return;
        }

        // Loop through all saved results in PlayerPrefs
        for (int i = 1; i <= resultCounter; i++)
        {
            string key = "GachaResult_" + i;
            string result = PlayerPrefs.GetString(key, "No Result");
            historyEntries.Add(result);  // Add the result to the list
        }

        // Log for debugging
        Debug.Log("Loaded Gacha History from PlayerPrefs:\n" + string.Join("\n", historyEntries));
    }

    // Update the page to show the correct gacha history entries
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

    // Go to the next page
    void NextPage()
    {
        // Go to the next page if available
        if ((currentPage + 1) * entriesPerPage < historyEntries.Count)
        {
            currentPage++;
            UpdatePage();
        }
    }

    // Go to the previous page
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
