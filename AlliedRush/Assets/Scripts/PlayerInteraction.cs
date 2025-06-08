using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask interactableLayer;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Vector2 position = transform.position;
            Vector2 direction = Vector2.right * transform.localScale.x; // assumes right-facing
            RaycastHit2D hit = Physics2D.Raycast(position, direction, interactRange, interactableLayer);

            Debug.DrawRay(position, direction * interactRange, Color.green, 1f);

            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.OnAction(gameObject);
                }
            }
            else
            {
                Debug.Log("No interactable object detected.");
            }
        }
    }
}

