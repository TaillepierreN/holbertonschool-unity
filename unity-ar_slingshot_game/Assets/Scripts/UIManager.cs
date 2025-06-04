using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private AmmoSelected[] _ammoCounterTokens;

    private void Start()
    {
        EventManager.Instance.OnScoreUpdated += UpdateScoreText;
        EventManager.Instance.OnAmmoCountUpdated += UpdateAmmoCounter;

    }

    private void OnDestroy()
    {
        EventManager.Instance.OnScoreUpdated -= UpdateScoreText;
        EventManager.Instance.OnAmmoCountUpdated -= UpdateAmmoCounter;
    }
    public void UpdateScoreText()
    {
        if (_scoreTxt != null)
        {
            _scoreTxt.text = $"Score: {EventManager.Instance.GameManager.GetScore()}";
        }
    }
    public void UpdateAmmoCounter(int ammoNbr)
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
}
