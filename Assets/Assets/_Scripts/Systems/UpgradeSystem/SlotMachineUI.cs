using System.Collections;
using TMPro;
using UnityEngine;

public class SlotMachineUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _slot1;
    [SerializeField] private TMP_Text _slot2;
    [SerializeField] private TMP_Text _slot3;
    [SerializeField] private TMP_Text _resultText;

    public IEnumerator PlaySpin(string[] results, bool isJackpot)
    {
        gameObject.SetActive(true);
        _resultText.text = "";

        // Spin animation — rapidly change symbols
        string[] symbols = { "⚔", "⚡", "🌀", "💀", "🍀" };
        float spinDuration = 1.5f;
        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            _slot1.text = symbols[Random.Range(0, symbols.Length)];
            _slot2.text = symbols[Random.Range(0, symbols.Length)];
            _slot3.text = symbols[Random.Range(0, symbols.Length)];
            elapsed += 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        // Show final results
        _slot1.text = results[0];
        _slot2.text = results[1];
        _slot3.text = results[2];

        if (isJackpot)
        {
            _resultText.text = "JACKPOT!";
        }
        else
        {
            _resultText.text = "Choose an upgrade";
        }

        yield return new WaitForSecondsRealtime(0.8f);

        if (!isJackpot)
        {
            gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}