using UnityEngine;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    public PlayerHealth ph;
    public UIDocument uiDoc;
    private Label healthLabel;
    private VisualElement healthBarMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        healthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarFill");
        InvokeRepeating("healthChange", 0f, 0.5f); // Update health display every 0.5 seconds
    }

    // Update is called once per frame
    void healthChange()
    {
        float healthRatio = (float)ph.curHealth / ph.maxHealth;
        float healthPercent = Mathf.Lerp(0, 100, healthRatio);
        healthBarMask.style.width = Length.Percent(healthPercent);

        //healthLabel.text = $"{ph.curHealth}/{ph.maxHealth}";

    }
}
