using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //------マウスで視点操作に必要変数--

    //カメラを入れる変数(このゲームならMainのカメラを入れる)
    public GameObject Cam;

    //カメラとキャラクターのクオータニオン
    Quaternion CameraRot;
    Quaternion PlayerRot;

    //マウス感度
    [System.NonSerialized]
    private float Sensityvty = 3.0f;

    //画面からカーソルを消すかどうかの関数
    bool CursolLock = true;

    //角度の制限用変数
    float minX = -90f, maxX = 90f;

    //----------------------------------

    //-----プレイヤー用の変数-----------

    //移動量の変数
    public float Speed = 0.5f;

    //座標(もしもの時に使うため必要なくなったら消していい)
    private float x, z;

    //----------------------------------


    // Start is called before the first frame update
    void Start()
    {
        
        //アップデートする関数を先に宣言しておく
        CameraRot = Cam.transform.localRotation;
        PlayerRot = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        //マウスの操作を格納する
        float xRot = Input.GetAxis("Mouse X") * Sensityvty;
        float yRot = Input.GetAxis("Mouse Y") * Sensityvty;
        //作成したクオータニオン変数に
        //Quarenion.Eularを使ってクオータニオンを作成
        CameraRot *= Quaternion.Euler(-yRot, 0, 0);
        PlayerRot *= Quaternion.Euler(0, -xRot, 0);
        //作成した角度制限関数を呼ぶ
        CameraRot = ClampRotation(CameraRot);

        //プレイヤーの移動更新
        PlayerUpdate();

        //カーソルを出現させるかどうか
        UpdateCursorLock();

        //更新した座標を反映させる
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

    //カーソルを出現させるかどうか
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

    //角度制限関数
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

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
