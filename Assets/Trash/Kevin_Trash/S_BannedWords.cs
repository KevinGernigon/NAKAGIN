using UnityEngine;

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
