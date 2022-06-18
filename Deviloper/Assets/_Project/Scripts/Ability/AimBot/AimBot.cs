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
        public float fireInterval;
        public ProjectileController projectilePrefab;
        public float projectileDamage;
		public float projectileSpeed;
		public float range;

		public Transform projectileCollection;

		private float timer;
		private List<EnemyController> enemies;
		private Transform player;

		private void Start()
		{
			player = CharacterService.Instance.GetPlayerTransform();
		}

		private void Update()
		{
			IncreaseTimer();
			if (timer > fireInterval)
				Fire();
		}
		private void IncreaseTimer()
		{
			timer += Time.deltaTime;
		}

		private void Fire()
		{
			timer = 0;
			enemies = CharacterService.Instance.GetEnemyList();
			if (enemies.Count <= 0)
				return;
			EnemyController Enemy = GetNearestEnemy();
			if (IsEnemyInRange(Enemy))
				CreateProjectile(Enemy);
		}

		private bool IsEnemyInRange(EnemyController Enemy)
		{
			return Vector2.Distance(Enemy.transform.position, transform.position) <= range && !Enemy.isDead;
		}

		private void CreateProjectile(EnemyController Enemy)
		{
			ProjectileController projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
			Vector2 predictDirection = GetRequiredDirection(Enemy);
			projectile.SetProperties(projectileSpeed, projectileDamage, predictDirection);
			projectile.transform.SetParent(projectileCollection);
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
			float Ps = projectileSpeed;
			float EAd = (E.transform.position - transform.position).magnitude;
			Vector2 Edir = (player.position - E.transform.position).normalized;
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
			float nearestDist = Vector2.Distance(transform.position, enemies[0].transform.position);

			for (int i = 1; i < enemies.Count; i++)
			{
				if (enemies[i].isDead)
					continue;

				float dist = Vector2.Distance(transform.position, enemies[i].transform.position);	
				if (dist < nearestDist)
				{
					nearestIndex = i;
					nearestDist = dist;
				}
			}
			return enemies[nearestIndex];
		}

	}
}
