using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
		[Header("Movement Properties")]
		[Range(2,25)]
        public float MoveSpeed;
        private Vector2 moveDirection;
		
		private Rigidbody2D rb;

		private void Start()
		{
			Initialize();
		}

		private void Update()
		{
			GetInput();
		}

		private void Initialize()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			MoveCharacter();
		}

		private void MoveCharacter()
		{
			Vector2 EffectiveMovement = moveDirection * MoveSpeed * Time.fixedDeltaTime;
			Vector2 movePosition = new Vector2(transform.position.x + EffectiveMovement.x,transform.position.y + EffectiveMovement.y);
			rb.MovePosition(movePosition);
		}

		private void GetInput()
		{
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}
	}
}
