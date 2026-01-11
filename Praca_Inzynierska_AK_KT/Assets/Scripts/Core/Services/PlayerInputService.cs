using UnityEngine;
using Core;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices.WindowsRuntime;


namespace Core.Services
{
	public static class PlayerInputService
	{
		public static PlayerInputActions _inputActions;
		static PlayerInputService()
		{
			_inputActions = new PlayerInputActions();
			_inputActions.Enable();
		}

		public static Vector2 MousePosition => Mouse.current.position.ReadValue();
		public static Vector2 Move => _inputActions.Player.Move.ReadValue<Vector2>();
		public static bool Pause => _inputActions.Player.Escape.IsPressed();
		public static bool Cancel => _inputActions.UI.Cancel.IsPressed();
		public static bool Sprint => _inputActions.Player.Sprint.IsPressed();
		public static float LeftMouseButton => _inputActions.Player.Attack.ReadValue<float>();
	}
}
