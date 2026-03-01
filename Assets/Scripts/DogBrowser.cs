using UnityEngine;
using TMPro;
using System.Collections;

public class DogUI : MonoBehaviour
{
    public TMP_Text breedText;

    void Start()
    {
        StartCoroutine(APIManager.Get<BreedResponse>(
            "",
            OnSuccess,
            OnError
        ));
    }

    void OnSuccess(BreedResponse response)
    {
        breedText.text = "";

        foreach (var breed in response.data)
        {
            breedText.text +=
                $"Name: {breed.attributes.name}\n" +
                $"Description: {breed.attributes.description}\n\n";
        }
    }

    void OnError(string error)
    {
        breedText.text = "Error: " + error;
    }
}