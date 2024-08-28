using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool;

    public List<CardDataSO> cardDataList;//游戏中所有可能出现的卡牌

    [Header("卡牌库")]

    public CardLibrarySO newGameCardLibrary;

    public CardLibrarySO currentLibrary;



    private int previousIndex;

    private void Awake()
    {
        InitializeCardDataList();

        foreach (var item in newGameCardLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(item);
        }
    }

    private void OnDisable()
    {
        currentLibrary.cardLibraryList.Clear();
    }

    #region 获取卡牌

    //初始化获取所有项目卡牌资源
    private void InitializeCardDataList()
    {
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new List<CardDataSO>(handle.Result);
        }
        else
        {
            Debug.LogError("No CardData Found!");
        }
    }
    #endregion

    //抽卡时调用的函数获得卡牌GameObj
    public GameObject GetCardObject()
    {
        var cardObj = poolTool.GetObjectFromPool();

        cardObj.transform.localScale = Vector3.zero;

        return cardObj;
    }

    public void DiscardCard(GameObject cardObj)
    {
        poolTool.ReturnObjectToPool(cardObj);
    }

    public CardDataSO GetNewCardData()
    {
        var randomIndex = 0;

        do
        {
            randomIndex = Random.Range(0, cardDataList.Count);
        } while (previousIndex == randomIndex);

        previousIndex = randomIndex;

        return cardDataList[randomIndex];
    }

    //解锁添加新卡牌
    public void UnlockCard(CardDataSO newCardData)
    {
        var newCard = new CardLibraryEntry
        {
            cardData = newCardData,
            amount = 1,
        };

        if (currentLibrary.cardLibraryList.Contains(newCard))
        {
            var target = currentLibrary.cardLibraryList.Find(t => t.cardData == newCardData);

            target.amount++;
        }
        else
        {
            currentLibrary.cardLibraryList.Add(newCard);
        }
    }
}
