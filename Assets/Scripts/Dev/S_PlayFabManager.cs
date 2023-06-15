using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class S_PlayFabManager : MonoBehaviour
{
    int StatValueMinutes;
    int StatValueSeconds;
    int StatValueMS;
    int StatValue;
    string Affichage;
    int StockageNbrPlayer;
    int StartPositionValueExtend = 0;
    bool _isActive = true;
    public GameObject rowPrefab;
    private Transform rowsParent;
    public Transform rowsParent1;
    public Transform rowsParent2;
    public Transform rowsParent3;
    public Transform rowsParent4;

    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject leaderboardWindow;

    [Header("Display name windows")]
    public TMP_InputField nameInput;
    public GameObject AlreadyUsed;
    public GameObject InvalidName;

    [Header("Own rank")]
    public GameObject OwnRankText;
    private Transform TransformOwnRank;
    public Transform TransformOwnRank1;
    public Transform TransformOwnRank2;
    public Transform TransformOwnRank3;
    public Transform TransformOwnRank4;

    [SerializeField] S_GestionnaireScene GestionnaireScene;
    

    string loggedInPlayFabId;

    //ban words//
    public TextAsset _bannedWordsText;
    private string _bannedWords;
    private string[] _bannedWordsList;
    //fin ban words//

    public void Start(){
        if(name == null){
            nameWindow.SetActive(true);
        }
        else{
            nameWindow.SetActive(false);
        } 
        rowsParent = rowsParent1;
        var request = new LoginWithCustomIDRequest { 
            CustomId = SystemInfo.deviceUniqueIdentifier, 
            CreateAccount = true, 
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true}
                };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        //ban words//
        _bannedWords = _bannedWordsText.text;
        _bannedWordsList = _bannedWords.Split(",");
        //fin ban words//
    }

    public void Continue()
    {
        if(name != null){
            GestionnaireScene.LoadNewScene(3);
        }
    }

#region Login
    void OnLoginSuccess(LoginResult result)
    {

        loggedInPlayFabId = result.PlayFabId;
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null){
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if(name == null)
            nameWindow.SetActive(true);


    }

    void OnLoginFailure(PlayFabError error)
    {
        AlreadyUsed.SetActive(true);
        nameWindow.SetActive(true);

        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    //ban words//
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
    //fin ban words//

    public void SubmitNameButton(){
        UpdateUserTitleDisplayNameRequest request;
        if (CompareToBannedWords(nameInput.text))
        {
            request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameInput.text,
            };
        }
        else 
        {
            InvalidName.SetActive(true);
            return; 
        }

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnLoginFailure);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Updated display name");
        AlreadyUsed.SetActive(false);
        InvalidName.SetActive(false);
        nameWindow.SetActive(false);

    }
#endregion
#region SendLeaderboard
    public void SendLeaderboardRun1(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate{
                    StatisticName = "Leaderboard1",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLoginFailure);
    }

    public void SendLeaderboardRun2(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate{
                    StatisticName = "Leaderboard2",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLoginFailure);
    }
    public void SendLeaderboardRun3(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate{
                    StatisticName = "Leaderboard3Bis",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLoginFailure);
    }
#endregion
#region GetLeaderboard
    public void GetLeaderBoard1(){
            var request = new GetLeaderboardRequest{
            StatisticName = "Leaderboard1",
            StartPosition = StartPositionValueExtend,
            MaxResultsCount = 100
        };
        rowsParent = rowsParent1;
        TransformOwnRank = TransformOwnRank1;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLoginFailure);
        
    }

    public void GetLeaderBoard2(){
        var request = new GetLeaderboardRequest{
            StatisticName = "Leaderboard2",
            StartPosition = StartPositionValueExtend,
            MaxResultsCount = 100
        };
        rowsParent = rowsParent2;
        TransformOwnRank = TransformOwnRank2;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLoginFailure);
    }

    public void GetLeaderBoard3(){
        var request = new GetLeaderboardRequest{
            StatisticName = "Leaderboard3Bis",
            StartPosition = StartPositionValueExtend,
            MaxResultsCount = 100
        };
        rowsParent = rowsParent3;
        TransformOwnRank = TransformOwnRank3;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLoginFailure);
    }

    public void GetLeaderBoard4(){
        var request = new GetLeaderboardRequest{
            StatisticName = "Leaderboard34",
            StartPosition = StartPositionValueExtend,
            MaxResultsCount = 100
        };
        rowsParent = rowsParent4;
        TransformOwnRank = TransformOwnRank4;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLoginFailure);
    }

    public void GetLeaderboardAroundPlayer1(){
        var request = new GetLeaderboardAroundPlayerRequest{
            StatisticName = "Leaderboard1",
            MaxResultsCount = 3
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardAroundPlayerGet, OnLoginFailure);
    }

    public void GetLeaderboardAroundPlayer2(){
        var request = new GetLeaderboardAroundPlayerRequest{
            StatisticName = "Leaderboard2",
            MaxResultsCount = 3
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardAroundPlayerGet, OnLoginFailure);
    }

    public void GetLeaderboardAroundPlayer3(){
        var request = new GetLeaderboardAroundPlayerRequest{
            StatisticName = "Leaderboard3Bis",
            MaxResultsCount = 3
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardAroundPlayerGet, OnLoginFailure);
    }

    public void GetLeaderboardAroundPlayer4(){
        var request = new GetLeaderboardAroundPlayerRequest{
            StatisticName = "Leaderboard4",
            MaxResultsCount = 3
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderBoardAroundPlayerGet, OnLoginFailure);
    }

    


#endregion
#region OnLeaderBoardGet

    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach(Transform item in rowsParent){
            Destroy(item.gameObject);
        }

        result.Leaderboard.Reverse();
        
        foreach(var item in result.Leaderboard){
            StockageNbrPlayer = result.Leaderboard.Count;
            
            StatValue = item.StatValue;
            while (StatValue != 0){
                if(StatValue - 60000 > 0){
                    StatValue = StatValue - 60000;
                    StatValueMinutes++;
                }
                else if(StatValue - 1000 > 0){
                    StatValue = StatValue - 1000;
                    StatValueSeconds++;
                }
                else{
                    StatValueMS = StatValue;
                    StatValue = 0;
                }
            }
            CalculLeaderboard();
            
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (StartPositionValueExtend + result.Leaderboard.Count - item.Position).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = Affichage;

                if(item.PlayFabId == loggedInPlayFabId){
                    texts[0].color = Color.cyan;
                    texts[1].color = Color.cyan;
                    texts[2].color = Color.cyan;  
                }

            // Debug.Log(string.Format("PLACE : {0} | ID: {1} | VALUE: {2}",
            // item.Position, item.DisplayName, Affichage));
            StatValueSeconds = 0;
            StatValueMinutes = 0;

        }
    }

        void OnLeaderBoardAroundPlayerGet(GetLeaderboardAroundPlayerResult result){

            foreach(Transform item in TransformOwnRank){
                Destroy(item.gameObject);
            }

        result.Leaderboard.Reverse();


        foreach(var item in result.Leaderboard){
            StatValue = item.StatValue;

            while (StatValue != 0){
                if(StatValue - 60000 > 0){
                    StatValue = StatValue - 60000;
                    StatValueMinutes++;
                }
                else if(StatValue - 1000 > 0){
                    StatValue = StatValue - 1000;
                    StatValueSeconds++;
                }
                else{
                    StatValueMS = StatValue;
                    StatValue = 0;
                }
            }
            if(StatValueSeconds>2){
                CalculLeaderboard();
            
                GameObject newGo = Instantiate(OwnRankText, TransformOwnRank);
                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (StartPositionValueExtend + StockageNbrPlayer - item.Position).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = Affichage;
                
                Debug.Log(string.Format("PLACE : {0} | ID: {1} | VALUE: {2}",
                item.Position, item.DisplayName, Affichage));
                StatValueSeconds = 0;
                StatValueMinutes = 0;
            }
            else
                return;
        }
    }
#endregion  

#region CalculLeaderboard
    void CalculLeaderboard(){
            Affichage = null;

            if(StatValueMinutes < 10){
                Affichage = "0" + StatValueMinutes;
            }
            else{
                Affichage = StatValueMinutes.ToString();
            }

            if(StatValueSeconds < 10){
                Affichage = Affichage + ":" + "0" + StatValueSeconds;
            }
            else{
                Affichage = Affichage + ":" + StatValueSeconds;
            }

            if(StatValueMS < 100){
                Affichage = Affichage + ":" + "0" + StatValueMS;
            }
            else if(StatValueMS < 10){
                Affichage = Affichage + ":" + "00" + StatValueMS;
            }
            else Affichage = Affichage + ":" + StatValueMS;

    }
#endregion
    
    
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successfull leaderbord sent");
    }
}