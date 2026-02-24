using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HtmlAgilityPack;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class test : MonoBehaviour
{
    public TMP_Text txt_message;
    public string TextURL;
    public string[] bodyImg;
    // Start is called before the first frame update
    void Start()
    {
        // URL to fetch HTML content


        // Create UnityWebRequest to fetch the HTML content

        StartCoroutine(ImgDataFetcher(TextURL));
        // Send the request and wait for a response
        StartCoroutine(PlayerName(TextURL));
    }
    IEnumerator ImgDataFetcher(string url)
    {       
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Get the HTML content from the response
            string htmlContent = www.downloadHandler.text;

            // Create HtmlDocument instance from the fetched HTML content
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            // Parse HTML and select <img> elements
            if (doc.DocumentNode != null)
            {
                // Select all <img> elements
                HtmlNodeCollection imgNodes = doc.DocumentNode.SelectNodes("//img");

                if (imgNodes != null)
                {
                    Debug.Log("Images found:");
                    foreach (HtmlNode imgNode in imgNodes)
                    {
                        // Get the value of src attribute from <img> tag
                        string srcValue = imgNode.Attributes["src"].Value;

                        // Get the value of alt attribute from <img> tag (if exists)
                        string altValue = imgNode.Attributes.Contains("alt") ? imgNode.Attributes["alt"].Value : "No alt text";

                        Debug.Log("Image Source: " + srcValue);
                        Debug.Log("Alt Text: " + altValue);
                        switch(altValue)
                        {
                            case "head":
                                bodyImg[0] = srcValue; 
                                break;
                            case "hand":
                                bodyImg[1] = srcValue;
                                break;
                            case "body":
                                bodyImg[2] = srcValue;
                                break;
                            case "leg":
                                bodyImg[3] = srcValue;
                                break;


                        }

                    }
                }
                else
                {
                    Debug.Log("No images found.");
                }
            }
            else
            {
                Debug.Log("Unable to load or parse the document.");
            }
        }
        else
        {
            Debug.Log("Error fetching HTML content: " + www.error);
        }
    }
    IEnumerator PlayerIDDataFetcher(string url)
    {

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();


        if (www.result == UnityWebRequest.Result.Success)
        {
            // Get the HTML content from the response
            string htmlContent = www.downloadHandler.text;

            // Create HtmlDocument instance from the fetched HTML content
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            // Parse HTML and select <p> elements
            if (doc.DocumentNode != null)
            {
                // Select all <p> elements
                HtmlNodeCollection pNodes = doc.DocumentNode.SelectNodes("//p");

                if (pNodes != null)
                {
                    Debug.Log("Paragraphs found:");
                    foreach (HtmlNode pNode in pNodes)
                    {
                        // Get inner text of <p> tag
                        string paragraphText = pNode.InnerText;
                        Debug.Log("Paragraph: " + paragraphText);
                    }
                }
                else
                {
                    Debug.Log("No paragraphs found.");
                }
            }
            else
            {
                Debug.Log("Unable to load or parse the document.");
            }
        }
        else
        {
            Debug.Log("Error fetching HTML content: " + www.error);
        }
    }
    IEnumerator PlayerName(string url)
    {
        // Create UnityWebRequest to fetch the HTML content
        UnityWebRequest www = UnityWebRequest.Get(url);

        // Send the request and wait for a response
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Get the HTML content from the response
            string htmlContent = www.downloadHandler.text;

            // Create HtmlDocument instance from the fetched HTML content
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            // Parse HTML and select <input> elements
            if (doc.DocumentNode != null)
            {
                // Select all <input> elements
                HtmlNodeCollection inputNodes = doc.DocumentNode.SelectNodes("//input");

                if (inputNodes != null)
                {
                    Debug.Log("Input fields found:");
                    foreach (HtmlNode inputNode in inputNodes)
                    {
                        // Get the value of type attribute from <input> tag
                        string typeValue = inputNode.Attributes["type"].Value;

                        // Get the value of name attribute from <input> tag (if exists)
                        string nameValue = inputNode.Attributes.Contains("name") ? inputNode.Attributes["name"].Value : "No name";

                        Debug.Log("Input Type: " + typeValue);
                        Debug.Log("Input Name: " + nameValue);
                    }
                }
                else
                {
                    Debug.Log("No input fields found.");
                }
            }
            else
            {
                Debug.Log("Unable to load or parse the document.");
            }
        }
        else
        {
            Debug.Log("Error fetching HTML content: " + www.error);
        }
    }
}
