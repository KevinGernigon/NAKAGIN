using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class S_GestionnaireManager : Manager                                              // Permet de charger dans chaque scenes des varriable identique donnée au manager listé                                    //Batterie, Pouvoire recup , Setting
{
    private static S_GestionnaireManager Instance;                                              // Creation d'une instance de manager ( Manager supreme )
    
    private S_ReferenceInterface _referenceInterface;


    [SerializeField]
    private List<Manager> _managers;                                                      //Listes des différents managers

    private void Awake()                                                                 //Awake est appelé lorsque l'instance de script est en cours de chargement.
    {
        if (S_GestionnaireManager.Instance == null)                                             //Si aucune Instance existe deja 
        {
            S_GestionnaireManager.Instance = this;                                              //L'instance devient l'instance supreme 

            DontDestroyOnLoad(this.gameObject);                                          // Elle ne peut pas etre détruite au changement de scène
            _managers = GetComponents<Manager>().ToList();                               // Elle récuperer en paramettre tout les managers de la listes

        }
        else                                                                             // sinon
        {
            Destroy(this.gameObject);                                                    // L'instance s'autodétruit car elle existe deja 
        }
    }


    public static T GetManager<T>() where T : Manager                                                                           // T fait référence à aucun type valide ( il s'agit simplement du nom d'un paramètre non définit passé à la méthode )      //where designe une contrainte de type générique                                         
    {   
        T returnedValue = null;
        returnedValue = Instance._managers.Find((currentManager) => currentManager.GetType() == typeof(T)) as T ;
        return returnedValue;
    }
}


