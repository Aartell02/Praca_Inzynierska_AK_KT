using UnityEngine;
using Core;
using UnityEngine.InputSystem;


namespace Core.Services
{
	internal static class PlayerInputService
	{
		static PlayerInputActions _inputActions;
		static PlayerInputService()
		{
			_inputActions = new PlayerInputActions();
			_inputActions.Enable();
		}

		public static Vector2 MousePosition => Mouse.current.position.ReadValue();
		public static Vector2 Move => _inputActions.Player.Move.ReadValue<Vector2>();

		public static void TogglePlayerInputActions(bool option)
		{
			if (option)
				_inputActions.Player.Enable();
			else
				_inputActions.Player.Disable();
		}

		public static void ToggleUIInputActions(bool option)
		{
			if (option)
				_inputActions.UI.Enable();
			else
				_inputActions.UI.Disable();
		}
	}
}
