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
        cam = GetComponent<PlayerLook>().cam; // Obt�m a refer�ncia para a c�mera do componente PlayerLook
        playerUI = GetComponent<PlayerUI>(); // Obt�m a refer�ncia para o componente PlayerUI
        inputManager = GetComponent<InputManager>(); // Obt�m a refer�ncia para o componente InputManager
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); 

        // Cria um raio saido do centro da c�mera, em dire��o para frente
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo; // Vari�vel para armazenar informa��es sobre a colis�o

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // Verifica se o objeto atingido possui um componente Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                // Obt�m o componente Interactable do objeto atingido
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
