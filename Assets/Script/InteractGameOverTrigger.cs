using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractGameOverTrigger_InputSystem : MonoBehaviour
{
    [Header("UI Prompt (กด E)")]
    [SerializeField] private GameObject pressEPrompt;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float returnDelaySeconds = 5f;

    private bool playerInRange;
    private bool triggered;

    private InputAction interactAction;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void Awake()
    {
        // E (Keyboard) + south button (Gamepad) เผื่อไว้
        interactAction = new InputAction("Interact", InputActionType.Button);
        interactAction.AddBinding("<Keyboard>/e");
        interactAction.AddBinding("<Gamepad>/buttonSouth");
    }

    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();

    private void Start()
    {
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange || triggered) return;

        if (interactAction.WasPressedThisFrame())
            StartCoroutine(GameOverFlow());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (pressEPrompt != null) pressEPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
    }

    private IEnumerator GameOverFlow()
    {
        triggered = true;

        if (pressEPrompt != null) pressEPrompt.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(true);

        yield return new WaitForSeconds(returnDelaySeconds);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}