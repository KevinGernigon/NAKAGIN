using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class S_GestionnaireManager : Manager                                              // Permet de charger dans chaque scenes des varriable identique donn�e au manager list�                                    //Batterie, Pouvoire recup , Setting
{
    private static S_GestionnaireManager Instance;                                              // Creation d'une instance de manager ( Manager supreme )
    
    private S_ReferenceInterface _referenceInterface;


    [SerializeField]
    private List<Manager> _managers;                                                      //Listes des diff�rents managers

    private void Awake()                                                                 //Awake est appel� lorsque l'instance de script est en cours de chargement.
    {
        if (S_GestionnaireManager.Instance == null)                                             //Si aucune Instance existe deja 
        {
            S_GestionnaireManager.Instance = this;                                              //L'instance devient l'instance supreme 

            DontDestroyOnLoad(this.gameObject);                                          // Elle ne peut pas etre d�truite au changement de sc�ne
            _managers = GetComponents<Manager>().ToList();                               // Elle r�cuperer en paramettre tout les managers de la listes

        }
        else                                                                             // sinon
        {
            Destroy(this.gameObject);                                                    // L'instance s'autod�truit car elle existe deja 
        }
    }


    public static T GetManager<T>() where T : Manager                                                                           // T fait r�f�rence � aucun type valide ( il s'agit simplement du nom d'un param�tre non d�finit pass� � la m�thode )      //where designe une contrainte de type g�n�rique                                         
    {   
        T returnedValue = null;
        returnedValue = Instance._managers.Find((currentManager) => currentManager.GetType() == typeof(T)) as T ;
        return returnedValue;
    }
}


