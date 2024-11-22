using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeBarControl : MonoBehaviour
{
    private UIDocument timeBarDocument;
    private ProgressBar timeBar;
    private float countdownTime = 6f; // 倒计时总时长
    private float remainingTime; // 剩余时间

    public void InitHealthBar()
    {
        timeBarDocument = GetComponent<UIDocument>();

        timeBar = timeBarDocument.rootVisualElement.Q<ProgressBar>("TimeBar");
    }

    private void Awake()
    {
        // 初始化倒计时时间
        remainingTime = countdownTime;
    }

    private void Update()
    {
        UpdateCountdown();
    }

    private void UpdateCountdown()
    {
        if (remainingTime <= 0) return; // 如果倒计时结束，则返回
        remainingTime -= Time.deltaTime; // 减少剩余时间
        if (remainingTime <= 0) remainingTime = 0; // 确保剩余时间不为负数
        float percentage = remainingTime / countdownTime; // 计算剩余时间的百分比
        // 这里可以添加更新进度条或其他UI元素的代码


            if (percentage < 0.3f)
            {
                timeBar.AddToClassList("lowTime");
            }
            else if (percentage < 0.6f)
            {
                timeBar.AddToClassList("mediumTime");
            }
            else
            {
                timeBar.AddToClassList("highTime");
            }
    }
}
