using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _winner;
    [SerializeField] TMPro.TMP_InputField _input;
    [SerializeField] BattleEvents _battleEvents;

    void Start()
    {
        _battleEvents.OnBattleEnded += OnBattleEnded;
    }

    public void OnClick()
    {
        var seed = string.IsNullOrEmpty(_input.text) ? 0 : int.Parse(_input.text);
        _battleEvents.BattleStartRequested(seed);
        gameObject.SetActive(false);
    }

    void OnBattleEnded(TeamConfig winner)
    {
        _winner.text = winner == null ? "Draw" : $"{winner.name} has won";
        gameObject.SetActive(true);
    }
}
