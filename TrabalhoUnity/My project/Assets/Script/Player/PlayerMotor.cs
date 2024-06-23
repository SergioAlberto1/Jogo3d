using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller; // Referência para o CharacterController do jogador
    private Vector3 playerVelocity; // Velocidade atual do jogador
    private bool isGrounded; // Indica se o jogador está no chão

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchTime = 1f;

    private bool crouching = false;
    private bool lerpCrouch = false;
    private float crouchTimer = 0f;
    private bool sprinting = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded; // Verifica se o jogador está no chão usando o CharacterController

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTime / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, crouchHeight, p); // Interpola a altura para a posição de agachamento
            else
                controller.height = Mathf.Lerp(controller.height, standHeight, p); // Interpola a altura para a posição em pé

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // Move o jogador na direção especificada, levando em conta a rotação do jogador
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Aplica a gravidade ao jogador
        playerVelocity.y += gravity * Time.deltaTime;

        // Verifica se o jogador está no chão e ajusta a velocidade vertical
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Move o jogador de acordo com a velocidade vertical
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        // Permite que o jogador salte se estiver no chão
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }
    public void Crouch()
    {
        // Altera agachar e levantar
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    public void Sprint()
    {
        //Altera correr e andar
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else 
            speed = 5;
    }
}
