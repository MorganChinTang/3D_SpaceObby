using System.IO;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    static private SaveManager instance = null;
    static public SaveManager Instance { get { return instance; } }

    private GameState gameState = new GameState();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private string GetFullFilePath()
    {
        return Application.persistentDataPath + "/game.json";
    }

    public void Load()
    {
        string filePath = GetFullFilePath();
        if(File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            gameState = JsonUtility.FromJson<GameState>(data);
        }
    }
    public void Save()
    {
        string filePath = GetFullFilePath();
        string data = JsonUtility.ToJson(gameState, true);
        File.WriteAllText(filePath, data);
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetLastCheckpointReached(int index)
    {
        gameState.LastCheckpoint = index;
    }
}
