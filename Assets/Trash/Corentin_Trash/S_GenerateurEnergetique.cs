using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GenerateurEnergetique : MonoBehaviour
{
   
    private S_ReferenceInterface _referenceInterface;

    

    [Header("Other")]

    
    [SerializeField] private float DefaultCharge = 0f;
    [SerializeField] private float ChargeEnergetique = 0f;

    

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        
    }
    private void Start()
    {
        ChargeEnergetique = DefaultCharge;
    }

    private void Update()
    {
        if (_referenceInterface._InputManager._playerInputAction.Player.Jetpack.triggered && !_referenceInterface._InputManager._jetpackActive)
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

            _referenceInterface._InputManager.DesactiveJetpackInput();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _referenceInterface._InputManager.ActiveJetpackInput();

        }
    }

    
    ///////////////////////////////////////////////////////////////
    
    public void ChargeUp()
    {
        ChargeEnergetique += 1f;
    }

    private void ReloadBattery()
    {
      if ( _referenceInterface._BatteryManager._nbrBattery < ChargeEnergetique )
      {
            _referenceInterface._BatteryManager._nbrBattery = ChargeEnergetique;
      }
      
    }

}
