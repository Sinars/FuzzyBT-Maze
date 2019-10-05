using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    private Dictionary<Effect, Buff> negativeEffects;

    private PlayerController movementController;

    public RectTransform disabledText;

    private TextMeshProUGUI disabledControl;

    private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
        negativeEffects = new Dictionary<Effect, Buff>();
        disabledControl = disabledText.GetComponent<TextMeshProUGUI>();
        disabledControl.SetText("");
        movementController = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();
	}
	
    public bool HasEffect(Effect effectName) {
        return negativeEffects.ContainsKey(effectName);
    }

    public void AddBuff(Buff buff) {
        if (!negativeEffects.ContainsKey(buff.BuffType))
        switch(buff.BuffType) {
            case Effect.POISONED:
                negativeEffects.Add(buff.BuffType, buff);
                disabledControl.SetText(buff.BuffType.ToString());
                break;
            case Effect.ROOTED:
                negativeEffects.Add(buff.BuffType, buff);
                ResetPlayerAction(true);
                disabledControl.SetText(buff.BuffType.ToString());
                break;
            default:
                break;
        }
    }
    public void NegateBuff(Effect effectName) {
        switch (effectName) {
            case Effect.POISONED:
                negativeEffects.Remove(effectName);
                disabledControl.SetText("");
                break;
            case Effect.ROOTED:
                negativeEffects.Remove(effectName);
                ResetPlayerAction(false);
                disabledControl.SetText("");
                break;
            default:
                break;
        }
    }

    public void Update() {
        foreach (var ef in negativeEffects) {
            if (Time.time > ef.Value.Rate + ef.Value.ElapsedTime) {
                playerHealth.takeDamage(ef.Value.Damage);
                ef.Value.ElapsedTime = Time.time;
            }
        }
        
    }

    private void ResetPlayerAction(bool enable) {
        movementController.Rooted = enable;
        movementController.ResetPosition();

    }
}
