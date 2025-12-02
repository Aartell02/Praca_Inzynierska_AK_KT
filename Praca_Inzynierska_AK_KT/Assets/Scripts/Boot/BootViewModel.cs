namespace Boot
{
	public static class BootViewModel
	{
		private static readonly BootManager bootManager = BootManager.Instance;
		public static void LoadScene(string sceneName) => bootManager.LoadSceneAsync(sceneName);

		public static void StartGame() => bootManager.StartGame();
	}
}
