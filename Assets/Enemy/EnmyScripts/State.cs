using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected GameObject owner; 

    public State(GameObject owner)
    {
        this.owner = owner;
    }

    public virtual void Enter() { }   
    public virtual void Update() { }  
    public virtual void Exit() { } 
}
