using NaughtyAttributes;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] WSInputReader _inputReader;
    [ShowNonSerializedField] RuneType[] _runesToStart;

    [SerializeField] TextMeshProUGUI _hint;

    [SerializeField] LoadLevel _levelLoader;

    int _currentStep = 0;
    bool _inputValid = false, _noRuneVisible = true;

    private void Start()
    {
        _inputReader.OnRuneValidated += ProcessInput;
        _inputReader.OnRuneGone += RuneDisappeared;

        var runes = new RuneType[]{ RuneType.Water, RuneType.Fire, RuneType.Wood, RuneType.Stone };
        _runesToStart = new RuneType[2];
        _runesToStart[0] = runes[Random.Range(0, runes.Length)];
        _runesToStart[1] = runes.Where(rune => rune != _runesToStart[0]).ToArray()[Random.Range(0, runes.Length-1)];

        StartCoroutine(Tutorial());
    }

    void OnDisable()
    {
        _inputReader.OnRuneValidated -= ProcessInput;
        _inputReader.OnRuneGone -= RuneDisappeared;
    }


    private void ProcessInput(RuneType type)
    {
        if (type == _runesToStart[_currentStep])
            _inputValid = true;

        _noRuneVisible = false;
    }
    private void RuneDisappeared()
    {
        _noRuneVisible = true;
    }

    IEnumerator Tutorial()
    {
        _currentStep = 0;
        _inputValid = false;

        _hint.text = $"Dessine la rune {GetRuneText(_runesToStart[0])} pour continuer";
        yield return new WaitUntil(() => _inputValid);
        
        _currentStep++;
        _inputValid = false;

        _hint.text = "";
        yield return new WaitUntil(() => _noRuneVisible);

        _hint.text = $"Maintenant dessine la rune {GetRuneText(_runesToStart[1])}";
        yield return new WaitUntil(() => _inputValid);


        _hint.text = $"Felicitation, tu es pret !";

        yield return new WaitForSeconds(3);

        _levelLoader.Load();
    }

    string GetRuneText(RuneType type)
    {
        switch (type)
        {
            case RuneType.Water:
                return "<color=blue> aquatique </color>";
            case RuneType.Fire:
                return "<color=red> pyrique </color>";
            case RuneType.Wood:
                return "<color=green> sylvestre </color>";
            case RuneType.Stone:
                return "<color=yellow> terrestre </color>";
            default:
                return "<rune-name>";

        }
    }
}
