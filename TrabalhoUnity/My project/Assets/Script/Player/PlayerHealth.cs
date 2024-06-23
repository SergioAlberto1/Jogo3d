using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    [Header("Health Bar")]
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration; // quanto tempo a imagem permanece totalmente opaca
    public float fadeSpeed; // com que rapidez a imagem desaparecer�

    private float durationTimer; 


    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); // Garante que o valor de health esteja entre 0 e maxHealth.
        UpdateHealthUI(); // Chama a fun��o para atualizar a interface gr�fica de sa�de.

        // Verifica se o canal alpha (transpar�ncia) da cor de overlay � maior que 0.
        if (overlay.color.a > 0)
        {
            // Se a sa�de for menor que 30, n�o faz mais nada nesta atualiza��o.
            if (health < 30)
                return;

            // Incrementa o contador de tempo de dura��o.
            durationTimer += Time.deltaTime;

            // Se o tempo acumulado (durationTimer) for maior que a dura��o estabelecida (duration).
            if (durationTimer > duration)
            {
                // Desvanece a imagem gradualmente reduzindo o canal alpha.
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed; // Reduz o canal alpha com base na velocidade de desvanecimento (fadeSpeed).
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha); // Atualiza a cor da overlay com o novo canal alpha.
            }
        }
    }
    public void UpdateHealthUI()
    {

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            // Se o preenchimento de fundo (backHealthBar) for maior que a fra��o de sa�de atual (hFraction),
            // significa que a sa�de est� a diminuir.

            frontHealthBar.fillAmount = hFraction; // Ajusta o preenchimento frontal para a nova fra��o de sa�de.
            backHealthBar.color = Color.red; // Muda a cor do preenchimento de fundo para vermelho.

            // Lerp (interpola��o linear) para suavizar a transi��o do preenchimento de fundo.
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            // Se o preenchimento frontal (frontHealthBar) for menor que a  sa�de atual (hFraction),
            // significa que a sa�de est� a aumentar.

            backHealthBar.color = Color.green; // Muda a cor do preenchimento de fundo para verde.
            backHealthBar.fillAmount = hFraction; // Ajusta o preenchimento de fundo para a nova fra��o de sa�de.

            // Lerp (interpola��o linear) para suavizar a transi��o do preenchimento frontal.
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage; // Reduz a sa�de pelo valor do dano recebido.
        lerpTimer = 0f; // Reinicia o temporizador de interpola��o (lerpTimer) para garantir uma transi��o suave na UI de sa�de.
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount; // Aumenta a sa�de pelo valor de cura recebido.
        lerpTimer = 0f;
    }
}