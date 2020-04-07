using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private CornPool _cornPool;
    [SerializeField] private Finish _finish;

    [SerializeField] private GameObject _panelLose;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _textWin;

    private void Start()
    {
        _cornPool.GetComponent<CornPool>().OnAllCornsDisable += ShowLosePanel;
        _finish.GetComponent<Finish>().OnEndRound += ShowWinPanel;
        
    }

    private void ShowLosePanel()
    {
        _panelLose.SetActive(true);
    }

    private void ShowWinPanel()
    {
        _panelWin.SetActive(true);
        _textWin.GetComponent<TextMeshProUGUI>().text = $"Кукурузок спасено: {_finish.GetComponent<Finish>().FinalCornCout}"; 
    }
}
