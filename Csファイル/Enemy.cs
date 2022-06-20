using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//�I�u�W�F�N�g��NavMeshAgent�R���|�[�l���g��ݒu
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    /*//�|�C���g
    public Transform[] Points;
    //���p�_
    [SerializeField] private int DestPoint = 0;
    //�i�r���b�V��
    private NavMeshAgent agent;

    //�X�s�[�h
    [SerializeField] private float Speed = 2.0f;

    //�ǂ�������v���C���[�̍��W�i�[
    Vector3 PlayerPos;
    //�v���C���[���i�[����Q�[���I�u�W�F�N�g
    [SerializeField] GameObject Player_;

    //����
    private float Distanse;

    //�ǐՋ���
    [SerializeField] private float TrackingRange = 3.0f;
    //�I��
    [SerializeField] private float QuitRange = 5.0f;
    //�ǐՃt���O
    [SerializeField] private bool Tracking = false;


    // Start is called before the first frame update
    void Start()
    {
        //�i�r���b�V�����R���|�[�l���g����
        agent = GetComponent<NavMeshAgent>();

        //�ڕW�Ɍp���I�Ɉړ�����
        agent.autoBraking = false;

        //���̃|�C���g�֍s��
        GoNextPoint();

        //�ǐՂ������I�u�W�F�N�g��T��
        Player_ = GameObject.Find("Player");

    }

    void GoNextPoint()
    {
        //�n�_�������ݒ肳��ĂȂ��Ƃ���0��Ԃ�
        if (Points.Length == 0)
            return;

        //�ڕW�n�_�ɍs���悤�ɐݒ�
        agent.destination = Points[DestPoint].position;

        //�s���n�_��ݒ肷��
        DestPoint = (DestPoint + 1) % Points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //Player�ƓG�̃I�u�W�F�N�g�𑪂�
        PlayerPos = Player_.transform.position;

        Distanse = Vector3.Distance(this.transform.position, PlayerPos);

        //�ǐՃt���O���I���ɂȂ��Ă���Ȃ�ǂ�
        if(Tracking == true)
        {
            //�������I���n�_�ɍs���ƒ��~
            if (Distanse > QuitRange)
                Tracking = false;

            //�v���C���[��ڕW�ɐݒ肷��
            agent.destination = PlayerPos;
        }
        else
        {
            //�������߂Â�����ǐՊJ�n
            if (Distanse < TrackingRange)
                Tracking = true;

            //�ڕW��ݒ肷��
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoNextPoint();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TrackingRange);

        //quitRange�͈̔͂�����C���[�t���[���Ŏ���
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

        // autoBraking �𖳌��ɂ���ƁA�ڕW�n�_�̊Ԃ��p���I�Ɉړ����܂�
        //(�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă�
        // ���x�����Ƃ��܂���)
        agent.autoBraking = false;

        GotoNextPoint();

        //�ǐՂ������I�u�W�F�N�g�̖��O������
        player = GameObject.Find("Player");
    }


    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if (points.Length == 0)
            return;

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        //Player�Ƃ��̃I�u�W�F�N�g�̋����𑪂�
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
        

        if (tracking)
        {
            //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
            if (distance > quitRange)
                tracking = false;

            //Player��ڕW�Ƃ���
            agent.destination = playerPos;
        }
        else
        {
            //Player��trackingRange���߂Â�����ǐՊJ�n
            if (distance < trackingRange)
                tracking = true;


            // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
            // ���̖ڕW�n�_��I�����܂�
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    void OnDrawGizmosSelected()
    {
        //trackingRange�͈̔͂�Ԃ����C���[�t���[���Ŏ���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);

        //quitRange�͈̔͂�����C���[�t���[���Ŏ���
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, quitRange);
    }
}
