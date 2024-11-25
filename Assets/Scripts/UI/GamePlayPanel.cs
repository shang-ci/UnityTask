using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.Collections; 

public class GamePlayPanel : MonoBehaviour
{
    [Header("事件广播")]
    public ObjectEventSO playerTurnEndEvent;

    private VisualElement roomElement;

    private Label energyAmountLabel, drawAmountLabel, discardAmountLabel, turnLabel;

    private Button endTurnButton;

    private void OnEnable()
    {
        roomElement = GetComponent<UIDocument>().rootVisualElement;

        //添加UI元素和事件处理程序
        energyAmountLabel = roomElement.Q<Label>("EnergyAmount");

        drawAmountLabel = roomElement.Q<Label>("DrawAmount");

        discardAmountLabel = roomElement.Q<Label>("DiscardAmount");

        turnLabel = roomElement.Q<Label>("TurnLabel");

        endTurnButton = roomElement.Q<Button>("EndTurn");

        endTurnButton.clicked += OnEndTurnButtonClicked;

        discardAmountLabel.text = "0";

        energyAmountLabel.text = "0";

        turnLabel.text = "游戏开始";

        StartCoroutine(CheckForInactivity());
    }

    private void OnEndTurnButtonClicked()
    {
        // 在这里处理结束回合的逻辑
        Debug.Log("End Turn Button Clicked");

        playerTurnEndEvent.RaiseEvent(null, this);

            endTurnButton.SetEnabled(false);

        turnLabel.text = "敌方回合";

        turnLabel.style.color = new StyleColor(Color.red);
        
    }

    private IEnumerator CheckForInactivity()
    {
        float inactivityTime = 10f; // 设置无操作时间
        float elapsedTime = 0f;

        while (true)
        {
            if (Input.anyKeyDown || EventSystem.current.IsPointerOverGameObject())
            {
                elapsedTime = 0f; // 重置计时器
            }
            else
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= inactivityTime)
                {
                    OnEndTurnButtonClicked(); // 自动点击结束回合按钮
                    elapsedTime = 0f; // 重置计时器
                }
            }
            yield return null;
        }
    }

    public void UpdateDiscardDeckAmount(int amount)
    {
        discardAmountLabel.text = amount.ToString();
    }

    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();
    }

    public void OnEnemyTurnBegin()
    {
        endTurnButton.SetEnabled(false);

        turnLabel.text = "敌方回合";

        turnLabel.style.color = new StyleColor(Color.red);
    }

    public void OnPlayerTurnBegin()
    {
        endTurnButton.SetEnabled(true);

        turnLabel.text = "玩家回合";

        turnLabel.style.color = new StyleColor(Color.white);
    }
}



//可用代码
// using System;
// using UnityEngine;
// using UnityEngine.UIElements;

// public class GamePlayPanel : MonoBehaviour
// {
//     [Header("事件广播")]
//     public ObjectEventSO playerTurnEndEvent;

//     private VisualElement roomElement;

//     private Label energyAmountLabel, drawAmountLabel, discardAmountLabel, turnLabel;

//     private Button endTurnButton;

//     private void OnEnable()
//     {
//         roomElement = GetComponent<UIDocument>().rootVisualElement;

//         //添加UI元素和事件处理程序
//         energyAmountLabel = roomElement.Q<Label>("EnergyAmount");

//         drawAmountLabel = roomElement.Q<Label>("DrawAmount");

//         discardAmountLabel = roomElement.Q<Label>("DiscardAmount");

//         turnLabel = roomElement.Q<Label>("TurnLabel");

//         endTurnButton = roomElement.Q<Button>("EndTurn");

//         endTurnButton.clicked += OnEndTurnButtonClicked;

//         drawAmountLabel.text = "0";

//         discardAmountLabel.text = "0";

//         energyAmountLabel.text = "0";

//         turnLabel.text = "游戏开始";

//     }

//     private void OnEndTurnButtonClicked()
//     {
//         playerTurnEndEvent.RaiseEvent(null, this);
//     }

//     public void UpdateDrawDeckAmount(int amount)
//     {
//         drawAmountLabel.text = amount.ToString();
//     }

//     public void UpdateDiscardDeckAmount(int amount)
//     {
//         discardAmountLabel.text = amount.ToString();
//     }

//     public void UpdateEnergyAmount(int amount){
//         energyAmountLabel.text = amount.ToString();
//     }

//     public void OnEnemyTurnBegin()
//     {
//         endTurnButton.SetEnabled(false);

//         turnLabel.text = "敌方回合";

//         turnLabel.style.color = new StyleColor(Color.red);
//     }

//     public void OnPlayerTurnBegin()
//     {
//         endTurnButton.SetEnabled(true);

//         turnLabel.text = "玩家回合";

//         turnLabel.style.color = new StyleColor(Color.white);
//     }
// }