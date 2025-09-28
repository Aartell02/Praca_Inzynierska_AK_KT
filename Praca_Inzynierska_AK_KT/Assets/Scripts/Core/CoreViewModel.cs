using Core.Services;
using UnityEngine;

namespace Core
{
    public partial class CoreViewModel
    {
		public static Vector2 Move => PlayerInputService.Move;
		public static Vector2 MousePosition => PlayerInputService.MousePosition;
		public static void TogglePlayerInputActions(bool option) => PlayerInputService.TogglePlayerInputActions(option);
		public static void ToggleUIInputActions(bool option) => PlayerInputService.ToggleUIInputActions(option);

	}
}
