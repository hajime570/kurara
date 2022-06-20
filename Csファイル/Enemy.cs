using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    /*//ポイント
    public Transform[] Points;
    //中継点
    [SerializeField] private int DestPoint = 0;
    //ナビメッシュ
    private NavMeshAgent agent;

    //スピード
    [SerializeField] private float Speed = 2.0f;

    //追いかけるプレイヤーの座標格納
    Vector3 PlayerPos;
    //プレイヤーを格納するゲームオブジェクト
    [SerializeField] GameObject Player_;

    //距離
    private float Distanse;

    //追跡距離
    [SerializeField] private float TrackingRange = 3.0f;
    //終了
    [SerializeField] private float QuitRange = 5.0f;
    //追跡フラグ
    [SerializeField] private bool Tracking = false;


    // Start is called before the first frame update
    void Start()
    {
        //ナビメッシュをコンポーネントする
        agent = GetComponent<NavMeshAgent>();

        //目標に継続的に移動する
        agent.autoBraking = false;

        //次のポイントへ行く
        GoNextPoint();

        //追跡したいオブジェクトを探す
        Player_ = GameObject.Find("Player");

    }

    void GoNextPoint()
    {
        //地点が何も設定されてないときは0を返す
        if (Points.Length == 0)
            return;

        //目標地点に行くように設定
        agent.destination = Points[DestPoint].position;

        //行く地点を設定する
        DestPoint = (DestPoint + 1) % Points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //Playerと敵のオブジェクトを測る
        PlayerPos = Player_.transform.position;

        Distanse = Vector3.Distance(this.transform.position, PlayerPos);

        //追跡フラグがオンになっているなら追う
        if(Tracking == true)
        {
            //距離が終了地点に行くと中止
            if (Distanse > QuitRange)
                Tracking = false;

            //プレイヤーを目標に設定する
            agent.destination = PlayerPos;
        }
        else
        {
            //距離が近づいたら追跡開始
            if (Distanse < TrackingRange)
                Tracking = true;

            //目標を設定する
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoNextPoint();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TrackingRange);

        //quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, QuitRange);
    }*/

     public Transform[] points;
    [SerializeField] int destPoint = 0;
    private NavMeshAgent agent;

    Vector3 playerPos;
    GameObject player;
    float distance;
    [SerializeField] float trackingRange= 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] bool tracking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;

        GotoNextPoint();

        //追跡したいオブジェクトの名前を入れる
        player = GameObject.Find("Player");
    }


    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返します
        if (points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        //Playerとこのオブジェクトの距離を測る
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
        

        if (tracking)
        {
            //追跡の時、quitRangeより距離が離れたら中止
            if (distance > quitRange)
                tracking = false;

            //Playerを目標とする
            agent.destination = playerPos;
        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            if (distance < trackingRange)
                tracking = true;


            // エージェントが現目標地点に近づいてきたら、
            // 次の目標地点を選択します
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    void OnDrawGizmosSelected()
    {
        //trackingRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);

        //quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, quitRange);
    }
}
