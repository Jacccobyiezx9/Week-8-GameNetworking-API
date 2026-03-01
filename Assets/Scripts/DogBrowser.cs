using UnityEngine;
using TMPro;
using System.Collections;

public class DogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text breedText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private int currentPage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadPage();
    }

    void LoadPage()
    {
        string url = $"{APIManager.BASE_URL}breeds?page[number]={currentPage}&page[size]=10";

        StartCoroutine(APIManager.Get<BreedResponse>(
            url,
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

        prevButton.SetActive(currentPage > 1);
        nextButton.SetActive(response.links.next != null);
    }

    void OnError(string error)
    {
        breedText.text = "Error: " + error;
    }

    public void NextPage()
    {
        currentPage++;
        LoadPage();
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            LoadPage();
        }
    }
}