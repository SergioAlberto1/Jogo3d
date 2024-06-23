using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    public bool useEvents;
    [SerializeField]
    public string promptMessage;
    public virtual string OnLook()
    {
        return promptMessage;
    }
    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }
    protected virtual void Interact()
    {

    }
}
