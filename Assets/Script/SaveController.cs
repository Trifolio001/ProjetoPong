using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public Color colorPlayer = Color.white;
    public Color colorEnemy = Color.white;
    public string namePlayer;
    public string nameEnemy;
    //0 = player, 1 = IA Facil, 2 = IA medio, 3 = IA dificil
    public int IdentyPlayer = 0;
    public int IdentyEnemy = 0;

    public int Points = 0;
    public string saveWinnerKey = "SavedWinner";
    public string saveWinnerStatistc = "Nenhuma Informação";

    private static SaveController _instance;
    // Propriedade estática para acessar a instância
    public static SaveController Instance
    {
        get
        {
            if (_instance == null)
            {
                // Procure a instância na cena
                _instance = FindObjectOfType<SaveController>();
                // Se não encontrar, crie uma nova instância
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SaveController).Name);
                    _instance = singletonObject.AddComponent<SaveController>();
                }
            }
            return _instance;
        }
    }

    public string GetName(bool isPlayer)
    {
        return isPlayer ? namePlayer : nameEnemy;
    }

    private void Awake()
    {
        // Garanta que apenas uma instância do Singleton exista
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        // Mantenha o Singleton vivo entre as cenas
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveWinner(string winner, string StatistcInfo)
    {
        PlayerPrefs.SetString(saveWinnerKey, winner);
        PlayerPrefs.SetString(saveWinnerStatistc, StatistcInfo);
    }
    public string GetLastWinner()
    {
        return PlayerPrefs.GetString(saveWinnerKey);
    }

    public string GetLastStatistc()
    {
        return PlayerPrefs.GetString(saveWinnerStatistc);
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
