using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour,IDynaObj
{
    #region data
    public PlayerData data;
    #endregion

    #region represent
    public bool isGameOver = false;
    public bool isMoveing = false;

    private Transform playerModel; // 玩家模型
    private Transform bulletMdoel; // 子弹模型
    private int speed; // 子弹射速
    private Transform shotPos; // 子弹位置
    private float lastDown; // 上一次按下时间
    private float lastFire; // 上一次射击时间

    #endregion

    /// <summary>
    /// 玩家创建
    /// </summary>
    public void DynaCreate()
    {
        playerModel = Instantiate(data.playerPrefab, this.transform, false).transform;
        shotPos = playerModel.Find("ShotPos");

        isGameOver = false;
        isMoveing = false;
        speed = data.speed;
    }

    /// <summary>
    /// 射击
    /// </summary>
    private void Update()
    {
        if (isGameOver)
            return;
        if (isMoveing)
            return;
        if (EventSystem.current.IsPointerOverGameObject()) // UGUI与射线有碰撞则return
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            lastDown = Time.time;
        }
        if (Input.GetButton("Fire1")&&Time.time-lastFire>0.2f)
        {
            bulletMdoel = Instantiate(data.bulletPrefab).transform;
            bulletMdoel.SetParent(this.transform, false);
            bulletMdoel.position = shotPos.position;
            bulletMdoel.rotation = shotPos.rotation;
            var v = shotPos.forward * speed;
            bulletMdoel.GetComponent<Rigidbody>().AddForce(v, ForceMode.Impulse);
            bulletMdoel.GetComponent<AudioSource>().pitch = Mathf.Lerp(1, 3, (Time.time - lastDown) / 2);
            lastFire = Time.time;
        }
    }

    public void DynaDestroy()
    {
        if (playerModel)
        {
            Destroy(playerModel.gameObject);
        }
        shotPos = null;
        playerModel = null;
        bulletMdoel = null;
    }

}
