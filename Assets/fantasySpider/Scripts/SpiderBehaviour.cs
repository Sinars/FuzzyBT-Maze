using System.Collections.Generic;
using UnityEngine;
using BT;
using BT.Composites;
using BT.Decorator;
using BT.Action;
using BT.Condition;
using Cinemachine;
using System.IO;
using System;
using NT;

public class SpiderBehaviour : MonoBehaviour {

    public TextAsset textAsset;

    private BehaviourTree bt;

    private GameObject enemy;

    public GameObject webProjectile;

    public GameObject area;

    public float criticalHealth;

    public float mediumHealth;

    public string taunt;

    public string walk;

    public string attack1;

    public string attack2;

    public float attackDistance;

    public float stoppingDistance;

    public float playerDistance;

    public float fireRate;

    public float retreatSpeed;
    
    private OutcomeEquals att1;
    private OutcomeEquals att2;
    private OutcomeEquals att3;

    private OutcomeEquals move;
    //private string fileName = "Assets/Settings/AiOutcome.ini";

    private TrainedNN nt;

    // end mock

    // Use this for initialization
    void Start () {
        enemy = GameManagement.GM.player;
        nt = new TrainedNN(textAsset);
        

        CreateTree();
    }

    private void CreateTree() {
        bt = new BehaviourTree(CreateSpiderBT(), nt);
        
    }


    private PrioritySelector CreateSpiderBT() {
        Node idleMotion = IdleMotion();


        PrioritySelector attackSelector = AttackPattern();
        //PlayerAround around = new PlayerAround(attackSelector, gameObject, enemy, playerDistance);
        return new PrioritySelector(new List<Node> { attackSelector, idleMotion });
    }

    private Node IdleMotion() {
        MoveToPositionInArea moveToPosition = new MoveToPositionInArea(gameObject, area);
        PlayAnimation walkAnimation = new PlayAnimation(gameObject, walk);
        //PlayerNotAround playerNotAround = new PlayerNotAround(moveToPosition, gameObject, enemy, playerDistance);
        Sequence idle =  new Sequence(new List<Node> { moveToPosition, walkAnimation });
        move = new OutcomeEquals(idle, 3);
        return move;
    }

    private PrioritySelector AttackPattern() {
        PrioritySelector goodHealth = GoodHealthPattern();
        //HealthAbove healthAbove = new HealthAbove(goodHealth, enemy, mediumHealth);
        att3 = new OutcomeEquals(goodHealth, 2);
        Selector lowHealth = LowHealthPattern();
        att1 = new OutcomeEquals(lowHealth, 1);

        //HealthBelow critical = new HealthBelow(lowHealth, enemy, criticalHealth);


        Sequence mediumHealthPattern = MediumHealthPattern();
        //HealthBelow medium = new HealthBelow(mediumHealthPattern, enemy, mediumHealth);
        att2 = new OutcomeEquals(mediumHealthPattern, 0);

        return new PrioritySelector(new List<Node> { att3, att2, att1});
    }

    private Sequence MediumHealthPattern() {
        ChargePlayer charge = new ChargePlayer(gameObject, enemy, attackDistance);
        //PlayerNotAround nextToPlayer = new PlayerNotAround(charge, gameObject, stoppingDistance);
        ActionNode attackPlayer = Attack(attack1);
        return new Sequence(new List<Node> { charge, attackPlayer });
    }

    private Selector LowHealthPattern() {
        //PlayAnimation shootWeb = new PlayAnimation(gameObject, taunt);
        ShootWeb web = new ShootWeb(gameObject, webProjectile, enemy);

        //PlayerNotDisabled playerDisabled = new PlayerNotDisabled(web, enemy);
        NoCooldown noCooldown = new NoCooldown(web, fireRate);
        //PlayerAround playerAround = new PlayerAround(noCooldown, gameObject, enemy, playerDistance);

        //Sequence webSequence = new Sequence(new List<Node> { noCooldown, web });

        ChargePlayer charge = new ChargePlayer(gameObject, enemy, attackDistance);
        ActionNode attackPlayer = Attack(attack1);
        //PrioritySelector attackTarget = new PrioritySelector(new List<Node> { webSequence, attackPlayer });
        Sequence attackSequence = new Sequence(new List<Node> { charge, attackPlayer });

        Selector selector = new Selector(new List<Node> { noCooldown, attackSequence});
        return selector;
    }

    private ActionNode Attack(string attack) {
        PlayAnimation attackAnimation1 = new PlayAnimation(gameObject, attack);
        //PlayerAround nextToPlayer = new PlayerAround(attackAnimation1, gameObject, enemy, stoppingDistance);
        //Repeater repeatAttack = new Repeater(attackAnimation1);
        return attackAnimation1;

    }


    private PrioritySelector GoodHealthPattern() {
        //PlayAnimation walkAnimation = new PlayAnimation(gameObject, walk);
        Retreat retreat = new Retreat(gameObject, enemy, retreatSpeed);
        //PlayerAround playerAround = new PlayerAround(retreat, gameObject, enemy, playerDistance);
        //Sequence walkAround = new Sequence(new List<Node> { walkAnimation, playerAround });

        ActionNode repeatAttack = Attack(attack2);

        return new PrioritySelector(new List<Node> { repeatAttack, retreat});

    }

    private int getDecision(float[] outcome) {
        float max = outcome[0];
        int index = 0;
        for (int i = 0; i < outcome.Length;i++)
            if (max < outcome[i]) {
                index = i;
                max = outcome[i]; 
            }
        return index;

    }


    // Update is called once per frame
    void Update () {
        const float stdDevDistance = 16.2786465f;
        const float meanDistance = 9.766235f;
        const float meanHealth = 82.3567047f;
        const float stdDevHealth = 99.1204f;
        Vector3 spiderPos = transform.position;
        Vector3 playerPos = enemy.transform.position;
        float distance = Vector3.Distance(spiderPos, playerPos);
        float usedDistance = (distance - meanDistance) / stdDevDistance;

        float playerLife = (enemy.GetComponent<PlayerHealth>().CurrentHealth - meanHealth) / stdDevHealth;
        float playerStatus = Convert.ToSingle(enemy.GetComponent<PlayerStatus>().HasEffect(Effect.ROOTED));
        playerStatus = playerStatus == 1 ? 1 : -1;
        //att1.setGiven(decision);
        //att2.setGiven(decision);
        //att3.setGiven(decision);
        //move.setGiven(decision);
        bt.Run(new float[] { usedDistance, playerStatus, playerLife }, getDecision);
    }
}
