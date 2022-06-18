using System;
using UnityEngine;

namespace Deviloper.Character
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
		private int m_Level;

		[Header("Movement Properties")]
		[Range(2,25)]
        public float moveSpeed;
        private Vector2 m_MoveDirection;
		
		private Rigidbody2D rb;

		private void Start()
		{
			Initialize();
		}
		private void Initialize()
		{
			m_Level = 1;
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if(UiController.Instance.isGamePlaying)
				GetInput();
		}

		private void FixedUpdate()
		{
			MoveCharacter();
		}

		private void MoveCharacter()
		{
			Vector2 EffectiveMovement = GetEffectiveMoveSpeed() * Time.fixedDeltaTime * m_MoveDirection;
			Vector2 movePosition = (Vector2)transform.position + EffectiveMovement;
			rb.MovePosition(movePosition);
		}

		private float GetEffectiveMoveSpeed()
		{
			return moveSpeed + (m_Level / 5);
		}

		private void GetInput()
		{
			m_MoveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{

			if (collision.transform.TryGetComponent(out EnemyController enemy))
			{
				UiController.Instance.GameOver();
			}
		}
	}
}
