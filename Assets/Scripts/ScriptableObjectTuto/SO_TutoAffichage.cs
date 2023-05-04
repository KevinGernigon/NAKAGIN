using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TextTutoriel_SO", menuName = "My Game/TextTutoriel_SO")]
public class SO_TutoAffichage : ScriptableObject
{
    public string _prefixTextAffiche;
    public string _radicalTextAffiche;
    public string _suffixTextAffiche;
    public int _nbrAction;
    public InputActionReference[] _actionRef;
    public string[] _nameButtonGamepad;
    public Sprite[] _ImageGamepad;
    public Sprite[] _ImageKeyboard;

    public bool _ImageMode;

};
