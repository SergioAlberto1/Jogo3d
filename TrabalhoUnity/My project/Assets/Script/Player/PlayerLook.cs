using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; 
    private float xRotation = 0f; 

    private float xSensitivity = 30f; // Sensibilidade do movimento do mouse no eixo X
    private float ySensitivity = 30f; // Sensibilidade do movimento do mouse no eixo Y

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x; 
        float mouseY = input.y; 

        // Calcula a rota��o da c�mera ao redor do eixo X
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); 

        // Aplica a rota��o calculada � transforma��o local da c�mera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);


        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    private void Start()
    {
        // Configura o cursor para ficar travado dentro da janela do jogo e invis�vel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
