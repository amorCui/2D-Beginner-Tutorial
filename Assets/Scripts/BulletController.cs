using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BulletController : MonoBehaviour {

    //[Header("�ӵ�")]
    //[Tooltip("�ӵ�Prefab")]
    //public GameObject bullet;

    Rigidbody2D _rigidbody2D;

    [Header("�ӵ�����")]
    [Tooltip("")]
    public float force = 500.0f;

    [Header("�ӵ��ƶ���󳤶�")]
    [Tooltip("�ӵ��ƶ�������󳤶������ӵ�ʵ��")]
    public float maxDestoryLength = 20.0f;

    // �ӵ���ʼλ��
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start() {
        startPosition = this.transform.position;
    }

    /// <summary>
    /// ���ؽű�ʵ��ʱ����
    /// </summary>
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {

        float dist = Vector3.Distance(startPosition, transform.position);
        Debug.Log($"transform.position:{transform.position} ===> {dist} =====> {dist > maxDestoryLength}" );
        //�ӵ��ƶ�������󳤶ȣ������ӵ�ʵ��
        if ( dist > maxDestoryLength ) {
            Debug.Log($"Destory bullet");
            Destroy(gameObject);
        }
    }

    public void Launcher(Vector2 vector2) {
        _rigidbody2D.AddForce(vector2 * force);
    }

    /// <summary>
    /// 2d ��ײ�¼�
    /// </summary>
    /// <param name="collision">��ײ�Ķ���</param>
    private void OnCollisionEnter2D(Collision2D collision) {

        collision.collider.GetComponent<EnemyController>()?.Fix();

        Debug.Log($"�ӵ���ײ����{collision.gameObject}");
        // ��ײ֮���ӵ���ʧ
        Destroy(gameObject);

    }
}
