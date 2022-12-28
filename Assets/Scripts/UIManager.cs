using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _wonMenu;

    private void Start()
    {
        _wonMenu.SetActive(false);
    }

    public void ShowWonMenu()
    {
        _wonMenu.SetActive(true);
    }

    private void OnEnable()
    {
        ActionHandler.WonGame += ShowWonMenu;
    }

    private void OnDisable()
    {
        ActionHandler.WonGame -= ShowWonMenu;
    }
}
