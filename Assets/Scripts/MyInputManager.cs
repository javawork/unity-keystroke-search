using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MyInputManager : MonoBehaviour
{
    public GameObject scrollViewContent;
    public GameObject buttonTemplate;
    private List<GameObject> buttons = new List<GameObject>();
    
    void Start()
    {
        ReadTextFile("data.txt");
    }

    void ReadTextFile(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            var count = 0;
            while (!reader.EndOfStream)
            {
                count++;
                string itemText = reader.ReadLine();
                GameObject newButton = Instantiate(buttonTemplate, scrollViewContent.transform);
                newButton.GetComponentInChildren<Text>().text = $"{count:000}_{itemText}";
                buttons.Add(newButton);
            }
        }
    }
    
    void OnChangeInputField(string value)
    {
        foreach (var button in buttons)
        {
            var buttonText = button.GetComponentInChildren<Text>().text;
            int indexOfUnderscore = buttonText.IndexOf('_');
            string outputString = buttonText.Substring(indexOfUnderscore + 1);

            bool isValueEmpty = string.IsNullOrEmpty(value);
            bool isOutputStringMatch = outputString.StartsWith(value, StringComparison.OrdinalIgnoreCase);

            bool shouldSetActive = isValueEmpty || isOutputStringMatch;

            if (button.activeSelf != shouldSetActive)
            {
                button.SetActive(shouldSetActive);
            }
        }
    }
}
