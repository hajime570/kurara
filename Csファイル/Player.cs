using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //------�}�E�X�Ŏ��_����ɕK�v�ϐ�--

    //�J����������ϐ�(���̃Q�[���Ȃ�Main�̃J����������)
    public GameObject Cam;

    //�J�����ƃL�����N�^�[�̃N�I�[�^�j�I��
    Quaternion CameraRot;
    Quaternion PlayerRot;

    //�}�E�X���x
    [System.NonSerialized]
    private float Sensityvty = 3.0f;

    //��ʂ���J�[�\�����������ǂ����̊֐�
    bool CursolLock = true;

    //�p�x�̐����p�ϐ�
    float minX = -90f, maxX = 90f;

    //----------------------------------

    //-----�v���C���[�p�̕ϐ�-----------

    //�ړ��ʂ̕ϐ�
    public float Speed = 0.5f;

    //���W(�������̎��Ɏg�����ߕK�v�Ȃ��Ȃ���������Ă���)
    private float x, z;

    //----------------------------------


    // Start is called before the first frame update
    void Start()
    {
        
        //�A�b�v�f�[�g����֐����ɐ錾���Ă���
        CameraRot = Cam.transform.localRotation;
        PlayerRot = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X�̑�����i�[����
        float xRot = Input.GetAxis("Mouse X") * Sensityvty;
        float yRot = Input.GetAxis("Mouse Y") * Sensityvty;
        //�쐬�����N�I�[�^�j�I���ϐ���
        //Quarenion.Eular���g���ăN�I�[�^�j�I�����쐬
        CameraRot *= Quaternion.Euler(-yRot, 0, 0);
        PlayerRot *= Quaternion.Euler(0, -xRot, 0);
        //�쐬�����p�x�����֐����Ă�
        CameraRot = ClampRotation(CameraRot);

        //�v���C���[�̈ړ��X�V
        PlayerUpdate();

        //�J�[�\�����o�������邩�ǂ���
        UpdateCursorLock();

        //�X�V�������W�𔽉f������
        Cam.transform.localRotation = CameraRot;
        transform.localRotation = PlayerRot;
    }

    public void PlayerUpdate()
    { 
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * Speed;
        z = Input.GetAxisRaw("Vertical") * Speed;

        transform.position += Cam.transform.forward * z + Cam.transform.right * x;
    }

    //�J�[�\�����o�������邩�ǂ���
    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CursolLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            CursolLock = true;
        }


        if (CursolLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!CursolLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //�p�x�����֐�
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

}
