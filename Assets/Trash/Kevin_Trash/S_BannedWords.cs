using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class S_BannedWords : MonoBehaviour
{
    public TextAsset _bannedWordsText;
    private string _bannedWords;
    private string[] _bannedWordsList;
    // Start is called before the first frame update
    void Start()
    {
        _bannedWords = _bannedWordsText.text;
        _bannedWordsList = _bannedWords.Split(",");
        Debug.Log(_bannedWordsList.Length);
    }

    public bool CompareToBannedWords(string name)
    {
        name = name.ToLower();
        for (int i = 0; i < _bannedWordsList.Length; i++)
        {
            if (name.Contains(_bannedWordsList[i]))
            {
                return false;
            }
        }
        return true;
    }
}
