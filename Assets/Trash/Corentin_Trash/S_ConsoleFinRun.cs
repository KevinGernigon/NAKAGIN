using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ConsoleFinRun : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;

    [SerializeField] private S_GenerateurEnergetique _GenerateurEnergetique;

    [SerializeField] private bool _consoleActive = false;
    [SerializeField] private bool _OnTrigger = false;


    [SerializeField] private GameObject _interfaceConsole;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
    }


    private void Start()
    {
        _interfaceConsole.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

    }



    private void Update()
    {
        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && !_consoleActive && _OnTrigger)
        {
            _consoleActive = true;
            _GenerateurEnergetique.ChargeUp();

            _interfaceConsole.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _OnTrigger = true;

            _referenceInterface._InputManager.DesactiveJetpackInput();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _OnTrigger = false;
            _referenceInterface._InputManager.ActiveJetpackInput();

        }
    }



}
