using UnityEngine;
using UnityEngine.UI;

public class AmmoSelected : MonoBehaviour
{
    [SerializeField] private Image _selectedAmmoImage;
    [SerializeField] private Image _ammoImage;

    public void SetSelectedAmmo()
    {
        if (_selectedAmmoImage != null && !_selectedAmmoImage.enabled)
            _selectedAmmoImage.enabled = true;
    }
    public void UnselectAmmo()
    {
        if (_selectedAmmoImage != null && _selectedAmmoImage.enabled)
            _selectedAmmoImage.enabled = false;
    }
    public void SetUsedAmmo()
    {
        if (_ammoImage != null)
            _ammoImage.enabled = false;
        else
        {
            Debugger.AppendText($"No ammo image found");
        }
    }

}
