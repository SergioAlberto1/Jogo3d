using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    //Rastrear qual ponto de referencia estamos a dar targeting no momento
    public int waypointIndex;
    public float waitTimer;
    public override void Enter()
    {
    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy.PodeVerPlayer())
        {
            stateMachine.ChangeState(new Atacar());
        }
    }

    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        // ver se waypoints existem
        if (enemy.path.waypoints == null || enemy.path.waypoints.Count == 0)
        {
            Debug.LogWarning("No waypoints found for patrol.");
            return;
        }
        // Implementar Patrol
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                // Certificar-se que o wayPointIndex esta dentro dos limites
                waypointIndex = (waypointIndex + 1) % enemy.path.waypoints.Count;

                // Defenir o destino para o próximo waypoint
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);

                // Redefinir temporizador de espera
                waitTimer = 0;
            }
        }
    }
}
