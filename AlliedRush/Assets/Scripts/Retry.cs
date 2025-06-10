using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void RetryGame()
    {
        // Cambia "EscenaJuego" por el nombre real de tu escena de juego
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");

        // Reset PlayerPrefs before quitting
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        Application.Quit();

        // Esto solo se nota en un ejecutable, no en el editor de Unity
    }
}
