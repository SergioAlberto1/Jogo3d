using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam; // Obtém a referência para a câmera do componente PlayerLook
        playerUI = GetComponent<PlayerUI>(); // Obtém a referência para o componente PlayerUI
        inputManager = GetComponent<InputManager>(); // Obtém a referência para o componente InputManager
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); 

        // Cria um raio saido do centro da câmera, em direção para frente
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo; // Variável para armazenar informações sobre a colisão

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // Verifica se o objeto atingido possui um componente Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                // Obtém o componente Interactable do objeto atingido
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
