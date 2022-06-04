using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]    
    public class EnemyController : MonoBehaviour
    {
		[Header("Movement")]
		[Range(2,25)]
		public float moveSpeed;

		private Rigidbody2D rb;
		public Transform player;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
		}

		private void FixedUpdate()
		{
			MoveToPlayer();
		}

		private void MoveToPlayer()
		{
			rb.MovePosition( Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime));
		}
	}
}
