using System.Collections;
using UnityEngine;
using Deviloper.Service.Character;
using Deviloper.Core;
using Deviloper.Pickup;
using Deviloper.Stronghold;

namespace Deviloper.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour,IDamageable
    {
		public EnemyTypeSO enemyBaseStats;
		public EnemyType Type { get; set; }

		private float m_Health;
		private float m_Speed;
		private float m_Damage;
		private int m_Level;

		private Rigidbody2D m_Rb;
		private Transform player;
		private PickupFactory pickupFactory;
		private ParticleSystem Explosion;
		private Collider2D col;
		private SpriteRenderer sprite;
		public bool isDead;

		private void OnEnable()
		{
			isDead = false;
		}
		
		private void Start()
		{
			Initilize();
		}

		private void Initilize()
		{
			m_Rb = GetComponent<Rigidbody2D>();
			player = CharacterService.Instance.GetPlayerTransform();
			pickupFactory = PickupFactory.Instance;
			Explosion = transform.GetChild(0).GetComponent<ParticleSystem>();
			Explosion.gameObject.SetActive(false);
			col = GetComponent<Collider2D>();
			sprite = GetComponent<SpriteRenderer>();
		}

		public void SetStats(int stageLevel)
		{
			m_Level = 1 + stageLevel / 5;
			m_Health = enemyBaseStats.baseHealth + (m_Level * 2f);
			m_Speed = enemyBaseStats.baseSpeed;
			m_Damage = enemyBaseStats.baseDamage + (m_Level * 3f);
		}

		public float GetSpeed() => m_Speed;	
		public void SlowDown(float value) => m_Speed /= value;

		private void FixedUpdate()
		{
			MoveToPlayer();
		}

		private void MoveToPlayer()
		{
			if(!isDead)
				m_Rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, m_Speed * Time.deltaTime));
		}

		public void TakeDamage(float damage)
		{
			m_Health -= damage;
			if(m_Health <= 0)
			{
				ActivateDeath();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			StrongholdController stronghold = collision.GetComponent<StrongholdController>();
			if (stronghold)
			{
				if (!stronghold.isDefenceEnabled)
					return;
				//You can use Observer Pattern here.
				stronghold.TakeDamage(m_Damage);
				ActivateDeath();
			}
		}
		private void OnDisable()
		{
			col.enabled = true;
			sprite.enabled = true;
			CharacterService.Instance.EnemyDeath(this);
		}
		
		public void ActivateDeath()
		{
			isDead = true;
			col.enabled = false;
			sprite.enabled = false;
			StartCoroutine(Explode());
		}

		private IEnumerator Explode()
		{
			DropPickup();
			m_Rb.AddTorque(200f);
			Explosion.gameObject.SetActive(true);
			Explosion.Play();
			yield return new WaitForSeconds(Explosion.main.duration);
			Explosion.gameObject.SetActive(false);
			transform.position = transform.parent.position;
			gameObject.SetActive(false);
		}

		private void DropPickup()
		{
			foreach (var drop in enemyBaseStats.dropProperties)
			{
				int chance = Random.Range(0,100);
				if(chance < drop.dropRate)
				{
					CreatePickup(drop.pickupType);
				}
			}
		}

		private void CreatePickup(PickupType pickupType)
		{
			if(pickupType == PickupType.Coin)
			{
				int coinAmount = (int)m_Health;
				pickupFactory.CreatePickup(coinAmount, transform.position);
			}
			else if(pickupType == PickupType.Health)
			{
				float healthAmount = m_Damage / 2;
				pickupFactory.CreatePickup(healthAmount,transform.position);
			}
		}
	}
}
