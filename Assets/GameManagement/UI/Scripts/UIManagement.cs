using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour {

    public static UIManagement UI;

    public Button playerInput;

    public void Awake() {
        if (UI == null) {
            UI = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    public void SetEnabledStatus(bool status) {
        playerInput.interactable = status;
    }

    public void ActivateButton() {
        playerInput.Select();
    }

}
