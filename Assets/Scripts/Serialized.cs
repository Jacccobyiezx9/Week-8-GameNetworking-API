using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

public class Serialized : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(APIManager.Get<DogBreedData>(APIManager.Breeds,
            (breed) => 
            { 
                var sb = new StringBuilder();
                foreach(var type in breed.Data)
                {
                    sb.AppendLine($"Breed: {type.DogBreedType}");
                }
                Debug.Log(sb.ToString());
            },
            (error) => { }));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class DogBreedData
    {
        [JsonProperty("data")]
        public List<DogBreed> Data;
    }
}
