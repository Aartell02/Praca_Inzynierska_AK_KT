using UnityEngine;
using Core;
using UnityEngine.InputSystem;


namespace Core.Services
{
	public interface IPlayerInputService
	{
		Vector2 MousePos { get; }
		Vector2 Move { get; }
	}

	internal class PlayerInputService : IPlayerInputService
	{
		PlayerInputActions inputActions;

		public Vector2 MousePos => Mouse.current.position.ReadValue();
		public Vector2 Move => inputActions.Player.Move.ReadValue<Vector2>();

		PlayerInputService(PlayerInputActions inputActions)
		{
			this.inputActions = inputActions;
			this.inputActions.Enable();
		}

		~PlayerInputService()
		{
			this.inputActions.Disable();
		}
	}
}
