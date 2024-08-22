using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;

    public CardLayoutManager layoutManager;

    public Vector3 deskPosition;

    private List<CardDataSO> drawDeck = new();   //抽牌堆

    private List<CardDataSO> discardDeck = new();   //弃牌堆

    private List<Card> handCardObjectList = new();   //当前手牌（每回合）

    //测试用
    private void Start()
    {
        InitializeDeck();

        DrawCard(3);
    }

    public void InitializeDeck()
    {
        drawDeck.Clear();

        foreach (var entry in cardManager.currentLibrary.cardLibraryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }

        ShuffleDeck();
    }

    [ContextMenu("测试抽牌")]

    public void TestDrawCard()
    {
        DrawCard(1);
    }

    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);
                }
                ShuffleDeck();
            }

            CardDataSO currentCardData = drawDeck[0];

            drawDeck.RemoveAt(0);

            var card = cardManager.GetCardObject().GetComponent<Card>();

            //初始化
            card.Init(currentCardData);

            card.transform.position = deskPosition;

            handCardObjectList.Add(card);

            var delay = i * 0.2f;

            SetCardLayout(delay);
        }
    }

    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            Card currentCard = handCardObjectList[i];

            CardTransform cardTransform = layoutManager.GetCardTransform(i, handCardObjectList.Count);

            //currentCard.transform.SetPositionAndRotation(cardTransform.pos, cardTransform.rotation);

            currentCard.isAnimating = true;

            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () =>
            {
                currentCard.transform.DOMove(cardTransform.pos, 0.5f).onComplete = () => currentCard.isAnimating = false;

                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };

            //设置卡牌排序
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;

            currentCard.UpdatePositionRotation(cardTransform.pos, cardTransform.rotation);
        }
    }

    //洗牌
    private void ShuffleDeck()
    {
        discardDeck.Clear();

        //更新UI显示数量
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];

            int randomIndex = Random.Range(i, drawDeck.Count);

            drawDeck[i] = drawDeck[randomIndex];

            drawDeck[randomIndex] = temp;
        }
    }

    //弃牌逻辑，函数事件
    public void DiscardCard(Card card)
    {
        discardDeck.Add(card.cardData);

        handCardObjectList.Remove(card);

        cardManager.DiscardCard(card.gameObject);

        SetCardLayout(0f);
    }
}
