using UnityEngine;
using Core;


namespace Core.Services
{
	public interface IPlayerInputService
	{
		Vector2 Move { get; }
	}

	internal class PlayerInputService : IPlayerInputService
	{
		PlayerInputActions inputActions;
		public Vector2 Move => inputActions.Player.Move.ReadValue<Vector2>();
	}
}
