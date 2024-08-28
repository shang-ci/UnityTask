using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement roonElement;

    private Button pickCardButton;

    private Button backToMapButton;

    [Header("事件广播")]
    public ObjectEventSO loadMapEvent;

    public ObjectEventSO pickCardEvent;


    private void Awake()
    {
        roonElement = GetComponent<UIDocument>().rootVisualElement;

        pickCardButton = roonElement.Q<Button>("PickCardButton");

        backToMapButton = roonElement.Q<Button>("BackToMapButton");

        backToMapButton.clicked += OnBackToMapButtonClicked;

        pickCardButton.clicked += OnPickCardButtonClicked;
    }

    private void OnPickCardButtonClicked()
    {
        pickCardEvent.RaiseEvent(null, this);
    }

    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);
    }

    public void OnFinishPickCardEvent()
    {
        pickCardButton.style.display = DisplayStyle.None;
    }
}



