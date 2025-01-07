using UnityEngine;
using UnityEngine.Networking;
using Leguar.TotalJSON;
using System.Collections;

public class NpGalaxy : MonoBehaviour
{
    public GameObject star;

	public JSON np;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		StartCoroutine(GetRequest("https://np4.ironhelmet.com/api/?game_number=2342&api_version=0.1&code=xX44n6p9MoW6"));
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator GetRequest(string uri) {
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
			Debug.Log("Sending...");
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();

			string[] pages = uri.Split('/');
			int page = pages.Length - 1;

			switch (webRequest.result) {
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.DataProcessingError:
					Debug.LogError(pages[page] + ": Error: " + webRequest.error);
					break;
				case UnityWebRequest.Result.ProtocolError:
					Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
					break;
				case UnityWebRequest.Result.Success:
					//Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
					np = JSON.ParseString(webRequest.downloadHandler.text);
					Build();
					break;
			}
		}
	}

    void Build() {
		Debug.Log(np.Get("scanning_data").ToString());
    }

}
