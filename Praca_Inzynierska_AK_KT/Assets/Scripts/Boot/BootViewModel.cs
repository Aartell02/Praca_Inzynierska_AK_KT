namespace Boot
{
	public static class BootViewModel
	{
		private static readonly BootManager bootManager = BootManager.Instance;

		public static void StartGame() => bootManager.StartGame();

		public static void LoadFloor() => bootManager.LoadFloor();

		public static void FinishGame(bool isWon) => bootManager.FinishGame(isWon);
	}
}
