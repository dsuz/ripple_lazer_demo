﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾を飛ばすクラス。
/// Fire() を呼ぶと、弾の大きさを変化させながら、指定した距離を指定した速度で飛ぶ。
/// 指定した距離を飛んだら、弾は消える。
/// </summary>
[RequireComponent(typeof(DrawingCircleController))]
public class BulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶ距離。この距離を飛んだら弾は消える</summary>
    [SerializeField] private float m_distance = 10f;
    
    /// <summary>弾が飛ぶ速度</summary>
    [SerializeField] private float m_speed = 10f;

    /// <summary>弾が発射された時の最初の大きさ</summary>
    [SerializeField] private float m_startRadius = 0.15f;
    
    /// <summary>弾が消える直前の最後の大きさ。</summary>
    [SerializeField] private float m_endRadius = 0.3f;
    
    /// <summary>弾が発射された時間</summary>
    private float m_startTime;
    
    /// <summary>弾が飛んだ距離</summary>
    private float m_journeyLength;
    
    /// <summary>弾が発射された場所</summary>
    private Vector3 m_startPosition;
    
    /// <summary>弾が最後に到達する場所。ここに向かって飛び、ここに来たら消える</summary>
    private Vector3 m_endPosition;
    
    /// <summary>弾の大きさを操作するための弾に追加された DrawingCircleController クラス</summary>
    private DrawingCircleController m_circle;

    private void Start()
    {
        m_startTime = Time.time;
        m_startPosition = transform.position;
        m_circle = GetComponent<DrawingCircleController>();
        m_circle.m_radius = m_startRadius;
    }

    /// <summary>
    /// direction で指定された方向に弾を飛ばす
    /// </summary>
    /// <param name="direction"></param>
    public void Fire(Vector3 direction)
    {
        m_endPosition = transform.position + direction * m_distance;
        m_journeyLength = Vector3.Distance(transform.position, m_endPosition);
    }

    void Update()
    {
        // Fire() が呼ばれて m_journeyLength が計算されていたら、弾の大きさを変えながら m_endPosition まで弾を飛ばす
        if (m_journeyLength > 0)
        {
            float distCovered = (Time.time - m_startTime) * m_speed;
            float fracJourney = distCovered / m_journeyLength;
            transform.position = Vector3.Lerp(m_startPosition, m_endPosition, fracJourney);
            m_circle.m_radius = Mathf.Lerp(m_startRadius, m_endRadius, fracJourney);
        }

        if (transform.position.Equals(m_endPosition)) Destroy(this.gameObject);
    }
}
