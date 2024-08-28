using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RestRoomPanel : MonoBehaviour
{
    private VisualElement roomElement;

    private Button restButton,backToMapButton;

    public Effect restEffect;

    public ObjectEventSO loadMapEvent;

    private CharacterBase player;

    private void OnEnable(){
        roomElement = GetComponent<UIDocument>().rootVisualElement;

        restButton = roomElement.Q<Button>("RestButton");

        backToMapButton = roomElement.Q<Button>("BackToMapButton");

        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);

        restButton.clicked += OnRestButtonClicked;

        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null,this);
    }

    private void OnRestButtonClicked()
    {
        restEffect.Execute(player,null);

        restButton.SetEnabled(false);
    }
}
