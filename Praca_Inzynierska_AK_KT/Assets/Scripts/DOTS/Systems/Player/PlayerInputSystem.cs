using Core;
using Core.Services;
using DOTS.Components.Player;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS.Systems.Player
{
	partial class PlayerInputSystem : SystemBase
	{
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		protected override void OnUpdate()
		{
			foreach ((RefRW<PlayerDataComponent> playerData, RefRW<PlayerInputComponent> input) in
						 SystemAPI.Query<RefRW<PlayerDataComponent>, RefRW<PlayerInputComponent>>())
			{
				input.ValueRW.Move = CoreViewModel.Move;
				input.ValueRW.MousePosition = CoreViewModel.MousePosition;
				Debug.Log($"input: {input.ValueRO.Move}");
			}
		}
	}
}
