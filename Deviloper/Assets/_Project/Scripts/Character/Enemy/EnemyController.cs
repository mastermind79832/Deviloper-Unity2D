using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Service.Character;
using Deviloper.Core;

namespace Deviloper.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]    
    public class EnemyController : MonoBehaviour,IDamageable
    {
		public EnemyTypeSO enemyBaseStats;

		private float m_Health;
		private float m_Speed;
		private float m_Damage;
		private int m_Level;

		private Rigidbody2D m_Rb;
		private Transform player;

		private void Start()
		{
			m_Rb = GetComponent<Rigidbody2D>();
			player = CharacterService.Instance.GetPlayerTransform();
		}

		public void SetStats(int stageLevel)
		{
			m_Level = 1 + stageLevel / 5;
			m_Health = enemyBaseStats.baseHealth + (m_Level * 2f);
			m_Speed = enemyBaseStats.baseSpeed;
			m_Damage = enemyBaseStats.baseDamage + (m_Level * 3f);
		}

		private void FixedUpdate()
		{
			MoveToPlayer();
		}

		private void MoveToPlayer()
		{
			m_Rb.MovePosition(
				Vector2.MoveTowards(transform.position, player.position, m_Speed * Time.deltaTime));
		}

		public void TakeDamage(float damage)
		{
			m_Health -= damage;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.CompareTag("Finish"))
			{
				CharacterService.Instance.EnemyDeath(this);
				Destroy(gameObject);
			}
		}
	}
}
