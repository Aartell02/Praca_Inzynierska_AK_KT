using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private PlayerInputActions UIControls;
    [SerializeField] PauseMenu pauseMenu;
    private void Awake()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        UIControls = new PlayerInputActions();
        UIControls.Enable();
        
    }
    private void OnEnable()
    {
        UIControls.UI.Pause.started += OnPause;
    }
    private void OnDisable()
    {
        UIControls.UI.Pause.started -= OnPause;
        UIControls.Disable();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (GameIsPaused) pauseMenu.Resume();
        else pauseMenu.Pause();
    }
    

}

