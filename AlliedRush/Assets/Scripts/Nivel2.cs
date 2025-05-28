using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Nivel2 : MonoBehaviour
{
    public int sceneBuildIndex;
 
    // Level move zoned enter, if collider is a player
    // Move game to another scene
    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if(other.tag == "Player") {
            // Player entered, so move level
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
 
/*using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel2 : MonoBehaviour
{
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("NewLevel")){
        // Cambia "EscenaJuego" por el nombre real de tu escena de juego
        SceneManager.LoadScene("Nivel 2");;
        }
    }
}*/