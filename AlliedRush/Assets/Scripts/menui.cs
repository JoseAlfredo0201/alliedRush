using UnityEngine;
using UnityEngine.SceneManagement;

public class menui : MonoBehaviour
{
    public void IniciarJuego()
    {
        // Cambia "EscenaJuego" por el nombre real de tu escena de juego
        SceneManager.LoadScene("Nivel");
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // Esto solo se nota en un ejecutable, no en el editor de Unity
    }
}
