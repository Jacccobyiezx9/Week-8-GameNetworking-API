using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public static class APIManager
{
    //https://dogapi.dog/docs/api-v2
    public const string BASE_URL = "https://dogapi.dog/api/v2/breeds?page[size]=10";
    public const string Breeds = "breeds";
    public static IEnumerator Get<T>(string route, Action<T> OnSuccess, Action<string> OnError)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(BASE_URL + route))
        {
            yield return webRequest.SendWebRequest();
            
            if(webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.downloadHandler.text);
                try
                {
                    var objData = JsonConvert.DeserializeObject<T>(webRequest.downloadHandler.text);
                    OnSuccess?.Invoke(objData);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
            else
            {
                Debug.LogError($"Unsuccessfully sent a request to {BASE_URL + route}");
                OnError?.Invoke(webRequest.error);
            }
        }
    }

    
}
[Serializable]
public class DogBreed
{
    [JsonProperty("Id")]
    public string DogBreedId;
    [JsonProperty("type")]
    public string DogBreedType;
}
[Serializable]
public class BreedResponse
{
    public List<BreedData> data;
}

[Serializable]
public class BreedData
{
    public string id;
    public string type;
    public BreedAttributes attributes;
}

[Serializable]
public class BreedAttributes
{
    public string name;
    public string description;
}

