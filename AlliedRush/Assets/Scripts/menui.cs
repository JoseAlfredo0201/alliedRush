using UnityEngine;
using UnityEngine.SceneManagement;

public class menui : MonoBehaviour
{
    public void IniciarJuego()
    {
        // Cambia "Nivel" por el nombre real de tu escena si es diferente
        SceneManager.LoadScene("Nivel");
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");

        // Reset PlayerPrefs before quitting
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Only quits in a built game, not in the editor
        Application.Quit();
    }
}
