using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GenerateurEnergetique : MonoBehaviour
{
   
    private S_ReferenceInterface _referenceInterface;

    

    [Header("Charge Energetique")]

    [SerializeField] private float DefaultCharge = 0f;
    [SerializeField] private float ChargeEnergetique = 0f;
    [SerializeField] private bool _OnTrigger = false;


    [SerializeField] private GameObject _indicateurCharge1;
    [SerializeField] private GameObject _indicateurCharge2;
    [SerializeField] private GameObject _indicateurCharge3;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        
    }


    private void Start()
    {
        ChargeEnergetique = DefaultCharge;
        _indicateurCharge1.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        _indicateurCharge2.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        _indicateurCharge3.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }



    private void Update()
    {
        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && !_referenceInterface._InputManager._jetpackActive && _OnTrigger)
            {
                ReloadBattery();
            }
    
        /* Ici indicateur du niveau de charge UI*/

    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Replacer trigger entrer par un raycast sur le player qui detecte si il regarde la console 
            // permet : Afficher l'ui 
                    //  Désactiver le jetpack 
                    //  Activer la fonction RealodBattery

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

    
    ///////////////////////////////////////////////////////////////
    public void ChargeUp()
    {
        ChargeEnergetique += 1f;

        if(ChargeEnergetique == 1)
            _indicateurCharge1.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        if (ChargeEnergetique == 2)
            _indicateurCharge2.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        if (ChargeEnergetique == 3)
            _indicateurCharge3.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
    }




    private void ReloadBattery()
    {
      if ( _referenceInterface._BatteryManager._nbrBattery < ChargeEnergetique )
      {
            _referenceInterface._BatteryManager._nbrBattery = ChargeEnergetique;
      }
    }



}
