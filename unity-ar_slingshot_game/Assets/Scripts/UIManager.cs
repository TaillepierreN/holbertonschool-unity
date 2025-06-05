using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private GameObject _retryButton;
    [SerializeField] private AmmoSelected[] _ammoCounterTokens;

    private void Start()
    {
        EventManager.Instance.OnScoreUpdated += UpdateScoreText;
        EventManager.Instance.OnAmmoCountUpdated += UpdateAmmoCounter;
        EventManager.Instance.OnResetGame += ResetGame;
        EventManager.Instance.ShowRetry += ShowRetryButton;

    }

    private void OnDestroy()
    {
        EventManager.Instance.OnScoreUpdated -= UpdateScoreText;
        EventManager.Instance.OnAmmoCountUpdated -= UpdateAmmoCounter;
        EventManager.Instance.OnResetGame -= ResetGame;
        EventManager.Instance.ShowRetry -= ShowRetryButton;
    }
    private void UpdateScoreText()
    {
        if (_scoreTxt != null)
        {
            _scoreTxt.text = $"Score: {EventManager.Instance.GameManager.GetScore()}";
        }
    }
    private void UpdateAmmoCounter(int ammoNbr)
    {

        try
        {

            if (_ammoCounterTokens == null || _ammoCounterTokens.Length == 0) return;
            int highlightIndex = ammoNbr - 1;
            int usedIndex = ammoNbr;

            Debugger.AppendText($"Ammo index {ammoNbr} selected.");
            if (usedIndex >= 0 && usedIndex < _ammoCounterTokens.Length)
            {
                _ammoCounterTokens[usedIndex].UnselectAmmo();
                _ammoCounterTokens[usedIndex].SetUsedAmmo();
            }
            if (highlightIndex >= 0 && highlightIndex < _ammoCounterTokens.Length)
            {
                _ammoCounterTokens[highlightIndex].SetSelectedAmmo();
            }
            else
            {
                Debugger.ShowText($"No more ammo to highlight (ammoNbr: {ammoNbr})");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error updating ammo counter: {ex.Message}");
        }
    }
    public void ResetGame()
    {
        ResetAmmoCounter();
        UpdateScoreText();
        _retryButton.SetActive(false);
    }

    public void ResetAmmoCounter()
    {
        foreach (var ammoToken in _ammoCounterTokens)
        {
            ammoToken.UnselectAmmo();
            ammoToken.ResetUsedAmmo();
        }
        _ammoCounterTokens[_ammoCounterTokens.Length - 1].SetSelectedAmmo();
    }

    public void ShowRetryButton()
    {
        _retryButton.SetActive(true);
    }
}
