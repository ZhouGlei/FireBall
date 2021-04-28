using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Lober lober; // 抛物线
    private Transform player; // 玩家组件
    private void Start()
    {
        lober = GetComponent<Lober>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Archs")
        {
            print("你碰到了圆环");
            // 处理游戏结束，播放抛物线动画
            lober.endPos = player.position;
            lober.GetComponent<Rigidbody>().isKinematic = true;
            lober.enabled = true;
            lober.onHitTarget += () =>
            {
                TTUIPage.ShowPage<FailedUIPage>(player);
                Destroy(this.gameObject); // 子弹销毁
            };
            player.GetComponent<Player>().isGameOver = true;
        }
        else if(collision.gameObject.tag == "Bricks")
        {
            print("你碰到了砖块");
            //处理砖块效果
            collision.transform.parent.GetComponent<Tower>().DropAndDown();
            Destroy(this.gameObject);
        }
    }
}
