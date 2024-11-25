using UnityEngine;
using UnityEngine.UIElements;

public class TimeBarControl : MonoBehaviour
{
    private UIDocument timeBarDocument;
    private ProgressBar timeBar;
    private float countdownTime = 6f; // 倒计时总时长
    private float remainingTime; // 剩余时间
    public CharacterBase currentCharacter;

    [Header("Elements")]
    public Transform timeBarTransform;
    private VisualElement defenceElement;
    private Label defenceAmountLabel;

    // 初始化进度条
    public void InitHealthBar()
    {
        Debug.Log("Initializing Health Bar...");
        timeBarDocument = GetComponent<UIDocument>();

        if (timeBarDocument == null)
        {
            Debug.LogError("UIDocument component not found!");
            return;
        }

        timeBar = timeBarDocument.rootVisualElement.Q<ProgressBar>("TimeBar");
        if (timeBar == null)
        {
            Debug.LogError("ProgressBar with name 'TimeBar' not found!");
            return;
        }

        timeBar.highValue = countdownTime;
        remainingTime = countdownTime; // 初始化倒计时时间
        Debug.Log($"TimeBar initialized with high value: {countdownTime}");
    }

    private void Awake()
    {
        Debug.Log("Awake called, initializing health bar...");
        InitHealthBar(); // 确保在 Awake 中调用初始化方法
    }

    private void Update()
    {
        UpdateCountdown();
    }

    // 更新倒计时逻辑
    private void UpdateCountdown()
    {
        if (remainingTime <= 0) return; // 如果倒计时结束，则返回
        remainingTime -= Time.deltaTime; // 减少剩余时间
        if (remainingTime < 0) remainingTime = 0; // 确保剩余时间不为负数
        float percentage = remainingTime / countdownTime; // 计算剩余时间的百分比
        timeBar.value = percentage; // 更新进度条的值
        Debug.Log($"Remaining Time: {remainingTime}, Percentage: {percentage}");
        UpdateProgressBar(percentage);
    }

    // 根据剩余时间更新进度条样式
    private void UpdateProgressBar(float percentage)
    {
        Debug.Log($"Updating progress bar with percentage: {percentage}");
        timeBar.RemoveFromClassList("lowTime");
        timeBar.RemoveFromClassList("mediumTime");
        timeBar.RemoveFromClassList("highTime");

        if (percentage < 0.3f)
        {
            timeBar.AddToClassList("lowTime");
            Debug.Log("Added lowTime class to progress bar");
        }
        else if (percentage < 0.6f)
        {
            timeBar.AddToClassList("mediumTime");
            Debug.Log("Added mediumTime class to progress bar");
        }
        else
        {
            timeBar.AddToClassList("highTime");
            Debug.Log("Added highTime class to progress bar");
        }
    }
}



// using UnityEngine;
// using UnityEngine.UIElements;

// public class TimeBarControl : MonoBehaviour
// {
//     private UIDocument timeBarDocument;
//     private ProgressBar timeBar;
//     private float countdownTime = 6f; // 倒计时总时长
//     private float remainingTime; // 剩余时间
//     public CharacterBase currentCharacter;

//     [Header("Elements")]
//     public Transform timeBarTransform;
//     private VisualElement defenceElement;
//     private Label defenceAmountLabel;

//     public void InitHealthBar()
//     {
//         timeBarDocument = GetComponent<UIDocument>();

//         timeBar = timeBarDocument.rootVisualElement.Q<ProgressBar>("TimeBar");

//         timeBar.highValue = countdownTime;


//         if (timeBarDocument == null)
//         {
//             Debug.LogError("UIDocument component not found!");
//             return;
//         }

//         timeBar = timeBarDocument.rootVisualElement.Q<ProgressBar>("TimeBar");
//         if (timeBar == null)
//         {
//             Debug.LogError("ProgressBar with name 'TimeBar' not found!");
//         }
//     }


//     private void Awake()
//     {
//         // 初始化倒计时时间
//         remainingTime = countdownTime;
//     }

//     private void Update()
//     {
//         UpdateCountdown();
//     }

//     private void UpdateCountdown()
//     {
//         if (remainingTime <= 0) return; // 如果倒计时结束，则返回
//         remainingTime -= Time.deltaTime; // 减少剩余时间
//         if (remainingTime <= 0) remainingTime = 0; // 确保剩余时间不为负数
//         float percentage = remainingTime / countdownTime; // 计算剩余时间的百分比
//         timeBar.value = percentage; // 更新进度条的值
//         UpdateProgressBar(percentage);
//     }


//     private void UpdateProgressBar(float percentage)
//     {
//         timeBar.RemoveFromClassList("lowTime");
//         timeBar.RemoveFromClassList("mediumTime");
//         timeBar.RemoveFromClassList("highTime");

//         if (percentage < 0.3f)
//         {
//             timeBar.AddToClassList("lowTime");
//         }
//         else if (percentage < 0.6f)
//         {
//             timeBar.AddToClassList("mediumTime");
//         }
//         else
//         {
//             timeBar.AddToClassList("highTime");
//         }
//     }
// }

// using Unity.Mathematics;
// using UnityEngine;
// using UnityEngine.UIElements;

// public class TimeBarControl : MonoBehaviour
// {
//     private UIDocument timeBarDocument;
//     private ProgressBar timeBar;
//     private float countdownTime = 6f; // 倒计时总时长
//     private float remainingTime; // 剩余时间
//     public CharacterBase currentCharacter;

//     [Header("Elements")]

//     public Transform timeBarTransform;

//     //private UIDocument timeBarDocument;

//     //private ProgressBar timeBar;

//     private VisualElement defenceElement;

//     private Label defenceAmountLabel;

//     public void InitHealthBar()
//     {
//         timeBarDocument = GetComponent<UIDocument>();

//         timeBar = timeBarDocument.rootVisualElement.Q<ProgressBar>("TimeBar");
//     }

//     private void Awake()
//     {
//         // 初始化倒计时时间
//         remainingTime = countdownTime;
//     }

//     private void Update()
//     {
//         UpdateCountdown();
//     }

//     private void UpdateCountdown()
//     {
//         if (remainingTime <= 0) return; // 如果倒计时结束，则返回
//         remainingTime -= Time.deltaTime; // 减少剩余时间
//         if (remainingTime <= 0) remainingTime = 0; // 确保剩余时间不为负数
//         float percentage = remainingTime / countdownTime; // 计算剩余时间的百分比
//         // 这里可以添加更新进度条或其他UI元素的代码


//             if (percentage < 0.3f)
//             {
//                 timeBar.AddToClassList("lowTime");
//             }
//             else if (percentage < 0.6f)
//             {
//                 timeBar.AddToClassList("mediumTime");
//             }
//             else
//             {
//                 timeBar.AddToClassList("highTime");
//             }
//     }
// }
