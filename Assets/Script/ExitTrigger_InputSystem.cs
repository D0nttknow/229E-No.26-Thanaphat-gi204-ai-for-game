using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ExitTrigger_InputSystem : MonoBehaviour
{
    [Header("UI Prompt (Press E)")]
    [SerializeField] private GameObject pressEPrompt;

    [Header("Exit UI")]
    [SerializeField] private GameObject youExitUI;

    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float returnDelaySeconds = 3f;

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
        interactAction = new InputAction("Interact", InputActionType.Button);
        interactAction.AddBinding("<Keyboard>/e");
        interactAction.AddBinding("<Gamepad>/buttonSouth");
    }

    private void OnEnable()
    {
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }

    private void Start()
    {
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
        if (youExitUI != null) youExitUI.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange || triggered) return;

        if (interactAction.WasPressedThisFrame())
            StartCoroutine(ExitFlow());
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

    private IEnumerator ExitFlow()
    {
        triggered = true;

        if (pressEPrompt != null) pressEPrompt.SetActive(false);
        if (youExitUI != null) youExitUI.SetActive(true);

        yield return new WaitForSeconds(returnDelaySeconds);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}