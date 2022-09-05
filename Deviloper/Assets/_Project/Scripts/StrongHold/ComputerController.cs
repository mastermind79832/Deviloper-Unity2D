using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Stronghold
{
    public class ComputerController : MonoBehaviour
    {
        [SerializeField] private Animator anim_UpgradePanelAnimator;

		private int anim_IsActive = Animator.StringToHash("IsActive");

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if(collision.gameObject.TryGetComponent(out Character.PlayerController controller))
				UpgragePanelActive(true);
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.TryGetComponent(out Character.PlayerController controller))
				UpgragePanelActive(false);
		}

		private void UpgragePanelActive(bool isActiive)
		{
			anim_UpgradePanelAnimator.SetBool(anim_IsActive, isActiive);
		}
	}
}
