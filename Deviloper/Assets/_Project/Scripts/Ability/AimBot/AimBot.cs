using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Service.Character;
using Deviloper.Character;
using Deviloper.Core;

namespace Deviloper.Ability.Aimbot
{
    public class AimBot : AbilityController 
    {
		[SerializeField] private ProjectileController m_ProjectilePrefab;
		[SerializeField] private float m_ProjectileSpeed;
		[SerializeField] private float m_ProjectileDamage;
		[SerializeField] private float m_FireInterval;
		[SerializeField] private float m_Range;

		[SerializeField] private Transform m_ProjectileCollection;
		private ObjectPool<ProjectileController> m_ProjectilePool;

		private float m_Timer;
		private List<EnemyController> m_Enemies;
		private Transform m_Player;

		protected override void Start()
		{
			m_AbilityName = "AimBot";
			m_Detail_1 = "Damage";
			m_Detail_2 = "Fire Inteval";
			m_Detail_3 = "Range";
			base.Start();
			m_Player = CharacterService.Instance.GetPlayerTransform();
			m_ProjectilePool = new();
		}

		private void Update()
		{
			IncreaseTimer();
			if (m_Timer > m_FireInterval)
				Fire();
		}
		private void IncreaseTimer()
		{
			m_Timer += Time.deltaTime;
		}

		private void Fire()
		{
			m_Timer = 0;
			m_Enemies = CharacterService.Instance.GetEnemyList();
			if (m_Enemies.Count <= 0)
				return;
			EnemyController Enemy = GetNearestEnemy();
			if (Enemy == default)
				return;

			if (IsEnemyInRange(Enemy))
				CreateProjectile(Enemy);
		}

		private bool IsEnemyInRange(EnemyController Enemy)
		{
			return Vector2.Distance(Enemy.transform.position, transform.position) <= m_Range && !Enemy.isDead;
		}

		private ProjectileController ProjectileFactory()
		{
			ProjectileController projectile;
			if (m_ProjectilePool.IsEmpty())
			{
				projectile = Instantiate(m_ProjectilePrefab, transform.position, transform.rotation);
				projectile.OnDisableBullet = m_ProjectilePool.SetItem;
			}
			else
			{
				projectile = m_ProjectilePool.GetItem();
				projectile.transform.position = transform.position;
				projectile.gameObject.SetActive(true);
			}

			return projectile;
		}

		private void CreateProjectile(EnemyController Enemy)
		{
			ProjectileController projectile = ProjectileFactory();
			Vector2 predictDirection = GetRequiredDirection(Enemy);
			projectile.SetProperties(m_ProjectileSpeed, m_ProjectileDamage, predictDirection);
			projectile.transform.SetParent(m_ProjectileCollection);
		}

		/* dont change name.. naming according to equation
		 * E	 : enemy
		 * Es	 : enemy Speed
		 * Edir	 : Enemy Direction
		 * EAd	 : Enemy->Aimbot distance
		 * EAdir : Enemy->Aimbot direction
		 * Ed	 : Predicted distance
		 * P	 : Projectile
		 * Ps	 : Projectile Speed
		 * Pdir	 : required projectile direction
		 * Pd	 : predicted projectile distance till predicted enemy distance
		 * z	 : angle between Edir and EAdir
		 * r	 : ratio between Es and Ps
		 * R	 : vector point of collision
		*/
		private Vector2 GetRequiredDirection(EnemyController E)
		{
			Vector2 Pdir = Vector2.zero;	
			float Es = E.GetSpeed();
			float Ps = m_ProjectileSpeed;
			float EAd = (E.transform.position - transform.position).magnitude;
			Vector2 Edir = (m_Player.position - E.transform.position).normalized;
			float r = Es / Ps;
			Vector2 EAdir = (transform.position - E.transform.position).normalized;
			float z = Vector2.Angle(Edir,EAdir) * Mathf.Deg2Rad;
			float Pd = QuadraticEquation(
				1 -r,
				EAd * r * Mathf.Cos(z),
				- Mathf.Pow(EAd,2f));
			if (Pd == Mathf.Infinity)
				Pdir = E.transform.position;
			else
			{
				float Ed = Pd * r;
				Vector2 R = Edir * Ed + (Vector2)E.transform.position;
				Pdir = (R - (Vector2)transform.position).normalized;
			}
			return Pdir;
		}
		/*
		 * To find solution of
		 * ax^2 + bx + c = 0
		 */
		private float QuadraticEquation(float a, float b, float c)
		{
			float d = Mathf.Pow(b, 2f) - 4f * a * c;
			if (d < 0)
				return Mathf.Infinity;

			float x1 = (-b + Mathf.Sqrt(d)) / 2f * a;
			float x2 = (-b - Mathf.Sqrt(d)) / 2f * a;

			return Mathf.Max(x1,x2);
		}

		private EnemyController GetNearestEnemy()
		{
			int nearestIndex = 0;
			while(m_Enemies[nearestIndex].isDead)
			{
				nearestIndex++;
				if (nearestIndex >= m_Enemies.Count)
					return default;
			}

			float nearestDist = Vector2.Distance(transform.position, m_Enemies[nearestIndex].transform.position);

			for (int i = nearestIndex; i < m_Enemies.Count; i++)
			{
				if (m_Enemies[i].isDead)
					continue;

				float dist = Vector2.Distance(transform.position, m_Enemies[i].transform.position);	
				if (dist < nearestDist)
				{
					nearestIndex = i;
					nearestDist = dist;
				}
			}
			return m_Enemies[nearestIndex];
		}

		protected override void UpdateUI()
		{
			m_UpgradeUI.RefreshUI(m_Level, m_ProjectileDamage, m_FireInterval, m_Range, m_UpgradeAmount);
		}

		protected override void Upgrade()
		{
			m_ProjectileDamage += 0.2f;
			m_FireInterval -= 0.1f;
			m_Range += 0.3f;
		}
	}
}
