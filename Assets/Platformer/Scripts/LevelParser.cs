using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject flowerPrefab;    
    public GameObject endPrefab;
    public PlayerMovement player;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (player.playerDied)
        {
            player.playerDied = false;
            LoadLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    public void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int rows = 0;
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                switch (letter)
                {
                    case '?':
                    {
                        Instantiate(questionBoxPrefab, new Vector2(column, rows), new Quaternion(0f, 0f, 180f, 0f) ,environmentRoot);
                        break;
                    }
                    case 'x':
                    {
                        Instantiate(rockPrefab, new Vector2(column, rows), quaternion.identity ,environmentRoot);
                        break;
                    }
                    case 'b':
                    {
                        Instantiate(brickPrefab, new Vector2(column, rows), quaternion.identity ,environmentRoot);
                        break;
                    }
                    case 's':
                    {
                        Instantiate(stonePrefab, new Vector2(column, rows), quaternion.identity ,environmentRoot);
                        break;
                    }
                    case 'f':
                    {
                        Instantiate(flowerPrefab, new Vector2(column, rows), quaternion.identity ,environmentRoot);
                        break;
                    }
                    case 'e':
                    {
                        Instantiate(endPrefab, new Vector2(column, rows), quaternion.identity ,environmentRoot);
                        break;
                    }
                }
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
            }

            rows++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
