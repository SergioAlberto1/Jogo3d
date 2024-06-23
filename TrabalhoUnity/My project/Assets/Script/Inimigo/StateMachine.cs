using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    public void Initialise()
    {
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }
    public void ChangeState(BaseState newState)
    {
        //ver activeState != null
        if (activeState != null)
        {
            //limapr o activestate
            activeState.Exit();
        }
        //mudar para um novo state
        activeState = newState;

        if (activeState != null)
        {
            //configurar novo state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Inimigo>();
            activeState.Enter();
        }
    }
}
