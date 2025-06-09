using TMPro;
using UnityEngine;

public class SetHighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreTxt1;
    [SerializeField] private TMP_Text _highScoreTxt2;
    [SerializeField] private TMP_Text _highScoreTxt3;

    void OnEnable()
    {
        _highScoreTxt1.text = $"1: {PlayerPrefs.GetInt("HighScore1", 0)}";
        _highScoreTxt2.text = $"2: {PlayerPrefs.GetInt("HighScore2", 0)}";
        _highScoreTxt3.text = $"3: {PlayerPrefs.GetInt("HighScore3", 0)}";
	}

}
