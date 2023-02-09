using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_PonctualEvent : MonoBehaviour
{

    public UnityEvent startEvents, updateEvent;
    

    void Start()
    {
        startEvents.Invoke();        
    }

    
    void Update()
    {
        updateEvent.Invoke();
    }
}
