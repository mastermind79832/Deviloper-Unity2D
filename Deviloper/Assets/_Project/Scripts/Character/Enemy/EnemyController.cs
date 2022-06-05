using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Service.Character;

namespace Deviloper.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]    
    public class EnemyController : MonoBehaviour
    {
		[Header("Movement")]
		[Range(0,25)]
		public float moveSpeed;

		private Rigidbody2D rb;
		private Transform player;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			player = CharacterService.Instance.GetPlayerTransform();
		}

		private void FixedUpdate()
		{
			MoveToPlayer();
		}

		private void MoveToPlayer()
		{
			rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime));
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
