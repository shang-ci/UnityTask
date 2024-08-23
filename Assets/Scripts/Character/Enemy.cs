using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionDataSO;

    public  EnemyAction currentAction;

    protected Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void OnPlayerTurnBegin()
    {
        {
            var randomIndex = Random.Range(0, actionDataSO.actions.Count);

            currentAction = actionDataSO.actions[randomIndex];
        }
    }
}
