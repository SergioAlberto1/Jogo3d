using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tiro : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Obtém o componente IDamageable do objeto com o qual ocorreu a colisão.
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();

        // Verifica se o componente IDamageable foi encontrado.
        if (damageable != null)
        {
            // Chama o método TakeDamage do componente IDamageable, passando o valor 10 como dano.
            damageable.TakeDamage(10);

            // Exibe uma mensagem de log indicando que ocorreu um acerto no objeto colidido.
            Debug.Log("Hit " + collision.transform.name);
        }

        // Destroi o próprio objeto (o tiro) após a colisão.
        Destroy(gameObject);
    }
}