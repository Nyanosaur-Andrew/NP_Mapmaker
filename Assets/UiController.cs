using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UiController : MonoBehaviour
{
    public Starfield galaxy;
    public StarConfig starConfig;

    public TMP_InputField inputStarCount;
    public TMP_InputField inputMapWidth;
    public TMP_InputField inputPlayerCount;
    public Button buttonGenerateMap;
    public Button buttonGenerateJson;

    public GameObject jsonPanel;
    public TMP_InputField jsonField;

    private Camera cam;

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void AddText(string textin);

    void Start()
    {
        cam = Camera.main;

        inputStarCount.text = "200";
        inputMapWidth.text = "10";
        inputPlayerCount.text = "2";
        UpdateValues();
    }

    void Update()
    {
        
    }

    public void UpdateValues() {
        galaxy.starCount = int.Parse(inputStarCount.text);
        galaxy.width = int.Parse(inputMapWidth.text);
        galaxy.playerCount = int.Parse(inputPlayerCount.text);
    }

    public void GenerateMap() {
        galaxy.GenerateMap();
    }

    public void AddStar() {
        Star s = galaxy.CreateStar(0f, 0f);
        starConfig.SelectStar(s);
        cam.transform.position = new Vector3(0f, 0f, cam.transform.position.z);
    }

    public void GenerateJson() {
        //jsonPanel.SetActive(true);
        galaxy.GenerateJson();
        jsonField.text = galaxy.output;
        //jsonField.onFocusSelectAll = true;
        //jsonField.Select();
        AddText(galaxy.output);
    }

}
