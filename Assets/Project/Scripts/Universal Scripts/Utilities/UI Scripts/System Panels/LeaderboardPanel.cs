using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

namespace Onion_AI
{
    public class LeaderboardPanel : MonoBehaviour
    {
        [Header("Pages Information")]
        private int totalPageIndex;
        private int currentPageIndex;
        [SerializeField] private int playerPerPage;

        [Header("UI Buttons")]
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button closeButton;

        [Header("Player Entries")]
        [SerializeField] private TextMeshProUGUI pageText;
        [SerializeField] private LeaderboardItem leaderboardItem;
        [SerializeField] private RectTransform leaderboardItemContainers;
        [SerializeField] private List<LeaderboardItem> leaderboardItemsList = new();

        private void OnEnable()
        {
            PopulateItemList();
        }

        private void Start()
        {
            UIEventsStaticClass.AddButtonListener(nextButton, NextPage);
            UIEventsStaticClass.AddButtonListener(prevButton, PrevPage);
            UIEventsStaticClass.AddButtonListener(closeButton, ClosePanel);

            totalPageIndex = 0;
            currentPageIndex = 1;
            pageText.text = "Pages: -- / --";
            
            LoadPage();
        }

        #region Display Page

        private void NextPage()
        {
            currentPageIndex++;
            LoadPage();
        }

        private void PrevPage()
        {
            currentPageIndex--;
            LoadPage();
        }

        private async void LoadPage()
        {
            nextButton.interactable = false;
            prevButton.interactable = false;

            try
            {
                GetScoresOptions getScoresOptions = new GetScoresOptions();

                getScoresOptions.Limit = playerPerPage;
                getScoresOptions.Offset = (currentPageIndex - 1) * playerPerPage;
                var scores = await LeaderboardsService.Instance.GetScoresAsync("SpaceKombat_PlayerRank", getScoresOptions);

                ReloadItemList(scores.Results);
                totalPageIndex = Mathf.CeilToInt(scores.Total / scores.Limit);
            }
            catch (System.Exception exception)
            {
                print(exception.Message);
            }

            pageText.text = $"Page: {currentPageIndex} / {totalPageIndex}";
            prevButton.interactable = currentPageIndex >= 1 && totalPageIndex >= 1;
            nextButton.interactable = currentPageIndex <= totalPageIndex && totalPageIndex >= 1;
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
            print("closed");
        }

        #endregion

        #region LeaderboardItem Controllers

        private void ReloadItemList(List<LeaderboardEntry> leaderboardEntries)
        {
            for(int i = 0; i < leaderboardEntries.Count; i++)
            {
                leaderboardItemsList[i].Initialize(leaderboardEntries[i]);
            }
        }

        private void PopulateItemList()
        {
            if(leaderboardItemsList.Count == playerPerPage)
            {
                return;
            }

            for(int i = 0; i < playerPerPage; i++)
            {
                LeaderboardItem item = Instantiate(leaderboardItem, leaderboardItemContainers);
                
                if(leaderboardItemsList.Contains(item) != true)
                {
                    leaderboardItemsList.Add(item);
                }
            }
        }

        #endregion
    }
}
