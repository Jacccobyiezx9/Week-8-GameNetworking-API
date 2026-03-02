using UnityEngine;
using TMPro;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class DogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text breedText;
    [SerializeField] private TMP_Text pageText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private int currentPage = 1;
    [SerializeField] private int maxPages = 29;
    [SerializeField] private Scrollbar scrollbar;

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
        scrollbar.value = 1;
    }

    void OnSuccess(BreedResponse response)
    {
        var sb = new StringBuilder();
        foreach (var breed in response.data)
        {
            sb.AppendLine("Name: " + breed.attributes.Name);
            sb.AppendLine("Description: " + breed.attributes.Description);
            sb.AppendLine();

            sb.AppendLine("Min Lifespan: " + breed.attributes.Life.Min);
            sb.AppendLine("Max Lifespan: " + breed.attributes.Life.Max);
            sb.AppendLine();

            sb.AppendLine("Min Male Weight: " + breed.attributes.male_weight.Min);
            sb.AppendLine("Max Male Weight: " + breed.attributes.male_weight.Max);
            sb.AppendLine();

            sb.AppendLine("Min Female Weight: " + breed.attributes.female_weight.Min);
            sb.AppendLine("Max Female Weight: " + breed.attributes.female_weight.Max);
            sb.AppendLine();

            sb.AppendLine("Hypoallergenic: " + breed.attributes.Hypoallergenic);
            sb.AppendLine();
        }
        pageText.text = currentPage + " / " + maxPages;
        breedText.text = sb.ToString();

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