﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    bool isColliding;
	const int side = 6;
    UnityEngine.Color MissionColor;
    GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    private void OnCollisionEnter(Collision collision)
    {
        if (isColliding) return;
        isColliding = true;
        if (collision.collider.tag == "Tile")
        {
            // 플레이어 이동
            Rigidbody rb = GetComponent<Rigidbody>();
			rb.velocity = new Vector3(0,0,0);
            rb.AddForce(new Vector3(0, 9.836f, 5.679f)*50);

            UnityEngine.UI.Text a = GameObject.Find("GameObject").GetComponent<BounceText>().text;

            //스테이지 클리어 여부 판별
            if (collision.collider.GetComponent<MeshRenderer>().materials[0].color == MissionColor)
                gm.ChangeState("StageClear");

            //게임오버 여부 판별
            if (System.Convert.ToInt32(a.text) == 0)
                gm.ChangeState("GameOver");

            // 플레이어 색깔 갱신
            UnityEngine.Color ChangedColor;
            ChangedColor = (GetComponent<MeshRenderer>().materials[0].color + collision.collider.GetComponent<MeshRenderer>().materials[0].color) / 2.0f;
            GetComponent<MeshRenderer>().materials[0].SetColor("_Color", ChangedColor);

            // 스코프 위치 이동
			GameObject scope = GameObject.Find("Scope");
			scope.transform.position += Vector3.forward * side * 1.9f;

            // 남은 충돌 횟수 ui 갱신
            a.text = (System.Convert.ToInt32(a.text) - 1).ToString();
        }
    }

    void Start()
    {
        GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new UnityEngine.Color(1.0f, 1.0f, 1.0f));
    }

    void Update()
    {
        isColliding = false;
    }
}