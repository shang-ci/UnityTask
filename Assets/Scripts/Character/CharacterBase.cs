using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;

public class CharacterBase : MonoBehaviour
{
    public int maxHp;

    public IntVariable hp;

    public IntVariable defence;

    public IntVariable buffRound;

    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); }

    public int MaxHP { get => hp.maxValue; }

    protected Animator animator;

    public bool isDead;

    public GameObject buff;

    public GameObject debuff;

    //力量有关
    public float baseStrength = 1f;

    private float strengthEffect = 0.5f;

    [Header("广播")]
    public ObjectEventSO characterDeadEvent;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        hp.maxValue = maxHp;

        CurrentHP = MaxHP;

        buffRound.currentValue = buffRound.maxValue;

        ResetDefence();
    }

    protected virtual void Update()
    {
        animator.SetBool("isDead", isDead);
    }

    public virtual void TakeDamage(int damage)
    {
        var currentDamage = (damage - defence.currentValue) >= 0 ? (damage - defence.currentValue) : 0;

        var currentDefence = (damage - defence.currentValue) >= 0 ? 0 : (defence.currentValue - damage);

        defence.SetValue(currentDefence);

        if (CurrentHP > currentDamage)
        {
            CurrentHP -= currentDamage;

            //Debug.Log("CurrentHP:" + CurrentHP);

            animator.SetTrigger("hit");
        }
        else
        {
            CurrentHP = 0;

            //当前人物死亡
            isDead = true;

            characterDeadEvent.RaiseEvent(this, this);
        }
    }

    public void UpdateDefence(int amount)
    {
        var value = defence.currentValue + amount;

        defence.SetValue(value);
    }

    public void ResetDefence()
    {
        defence.SetValue(0);
    }

    public void HealHealth(int amount)
    {
        CurrentHP += amount;

        CurrentHP = Mathf.Min(CurrentHP, MaxHP);

        buff.SetActive(true);
    }

    public void SetupStrength(int round, bool isPosition)
    {
        if (isPosition)
        {
            float newStrength = baseStrength + strengthEffect;

            baseStrength = Mathf.Min(newStrength, 1.5f);

            buff.SetActive(true);
        }
        else
        {
            debuff.SetActive(true);

            baseStrength = 1 - strengthEffect;
        }

        var currentRound = buffRound.currentValue + round;

        if (baseStrength == 1)
            buffRound.SetValue(0);
        else
            buffRound.SetValue(currentRound);
    }

    //回合转换事件函数
    public void UpdateStrengthRoung()
    {
        buffRound.SetValue(buffRound.currentValue - 1);

        if (buffRound.currentValue <= 0)
        {
            buffRound.SetValue(0);

            baseStrength = 1;
        }
    }
}
