using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using GameSystems;
using Core;
using UnityEngine.InputSystem;
using System.Reflection;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MechanicsPlayModeTests
{
	private IEnumerator LoadGameplay()
	{
		yield return SceneManager.LoadSceneAsync("BootScene");

		while (SceneManager.GetActiveScene().name != "GameplayScene")
			yield return null;

		yield return null;
	}

	[UnityTest]
	public IEnumerator Player_Exists_InScene()
	{
		yield return LoadGameplay();

		PlayerMovement player = null;
		while (player == null)
		{
			player = Object.FindFirstObjectByType<PlayerMovement>();
			yield return null;
		}

		Assert.NotNull(player);
	}

	[UnityTest]
	public IEnumerator Player_HasRequiredComponents()
	{
		yield return LoadGameplay();

		PlayerMovement player = null;
		while (player == null)
		{
			player = Object.FindFirstObjectByType<PlayerMovement>();
			yield return null;
		}

		Assert.NotNull(player.GetComponent<Rigidbody2D>());
		Assert.NotNull(player.GetComponent<PlayerAttack>());
		Assert.NotNull(player.GetComponent<PlayerState>());
		Assert.NotNull(player.GetComponent<LifeStateData>());
	}

	[UnityTest]
	public IEnumerator Enemy_Takes_Damage()
	{
		yield return LoadGameplay();

		var playerGO = new GameObject();
		playerGO.AddComponent<PlayerState>();

		var enemyGO = new GameObject();
		var enemy = enemyGO.AddComponent<LifeStateData>();
		enemyGO.AddComponent<Animator>();

		yield return null;

		enemy.Health = 50;

		int startHp = enemy.Health;

		enemy.TakeDamage(10, playerGO);

		yield return null;

		Assert.Less(enemy.Health, startHp);
	}



	[UnityTest]
	public IEnumerator Knockback_AppliesForce()
	{
		yield return LoadGameplay();

		var attacker = new GameObject();
		attacker.transform.position = Vector2.zero;

		var hitboxGO = new GameObject();

		hitboxGO.AddComponent<BoxCollider2D>();
		hitboxGO.AddComponent<Rigidbody2D>();

		var hitbox = hitboxGO.AddComponent<MeleeHitbox>();

		var enemyGO = new GameObject();
		enemyGO.transform.position = Vector2.right;

		var enemyRB = enemyGO.AddComponent<Rigidbody2D>();
		var enemyCol = enemyGO.AddComponent<BoxCollider2D>();

		hitbox.Initialize(attacker, 1, ~0, 0.2f);

		hitbox.SendMessage("OnTriggerEnter2D", enemyCol);

		yield return new WaitForSeconds(0.05f);

		Assert.Greater(enemyRB.linearVelocity.magnitude, 0f);
	}


	[UnityTest]
	public IEnumerator Player_Gains_XP_On_Kill()
	{
		yield return LoadGameplay();

		yield return new WaitForSeconds(181f);

		var player = Object.FindFirstObjectByType<PlayerState>();
		Assert.NotNull(player);

		var allLife = Object.FindObjectsByType<LifeStateData>(FindObjectsSortMode.None);

		LifeStateData enemy = null;
		foreach (var l in allLife)
		{
			if (!l.GetComponent<PlayerState>())
			{
				enemy = l;
				break;
			}
		}

		Assert.NotNull(enemy);

		float xpBefore = player.Experience;

		enemy.TakeDamage(9999, player.gameObject);

		yield return new WaitForSeconds(0.1f);

		Assert.Greater(player.Experience, xpBefore);
	}


	[UnityTest]
	public IEnumerator Dash_Accelerates_Movement()
	{
		yield return LoadGameplay();

		yield return new WaitForSeconds(0.5f);

		PlayerMovement player = null;
		while (player == null)
		{
			player = Object.FindFirstObjectByType<PlayerMovement>();
			yield return null;
		}

		bool dashLogged = false;

		Application.logMessageReceived += (c, s, t) =>
		{
			if (c.Contains("DASH"))
				dashLogged = true;
		};

		var keyboard = InputSystem.AddDevice<Keyboard>();

		InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.Space));
		InputSystem.Update();

		float timeout = 1.5f;

		while (!dashLogged && timeout > 0f)
		{
			timeout -= Time.deltaTime;
			yield return null;
		}

		Assert.IsTrue(dashLogged);
	}
}
