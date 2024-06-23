using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : BaseState
{
    private float TempoMov;
    private float losePlayerTimer;
    private float DispararTem;
    public override void Enter()
    {
    
    }

    public override void Exit()
    {
    
    }

    public override void Perform()
    {
        if (enemy.PodeVerPlayer()) // Se o inimigo pode ver o jogador
        {
            // Reseta o timer de perda de visão do jogador e incrementa os tempos de movimento e disparo
            losePlayerTimer = 0;
            TempoMov += Time.deltaTime;
            DispararTem += Time.deltaTime;

            // Faz o inimigo olhar para o jogador
            enemy.transform.LookAt(enemy.Player.transform);

            // Se o tempo de disparo for maior que a taxa de tiro, dispara
            if (DispararTem > enemy.TaxaTiro)
            {
                Disparar();
            }

            // Move o inimigo para uma posição aleatória após um tempo aleatório
            if (TempoMov > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                TempoMov = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            // Se o inimigo não vê o jogador por mais de 8 segundos, muda para o estado de patrulha
            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Disparar()
    {
        // Obtém a referência do cano da arma
        Transform Cano = enemy.Cano;

        // Instancia uma nova bala
        GameObject Tiro = GameObject.Instantiate(Resources.Load("Prefabs/Tiro") as GameObject, Cano.position, enemy.transform.rotation);

        // Calcula a direção do disparo em direção ao jogador
        Vector3 shootDirection = (enemy.Player.transform.position - Cano.transform.position).normalized;

        // Adiciona força ao rigidbody da bala
        Tiro.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40;

        Debug.Log("Disparar");
        DispararTem = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
