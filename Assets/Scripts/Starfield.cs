using UnityEngine;
using System.Collections.Generic;
using Leguar.TotalJSON;


public class Starfield : MonoBehaviour {

    public List<Star> stars;

    public float width;
    public int starCount;
    public int playerCount;
    public float rscale;
    public float rbase;

    public string output;

    public JSON json;
    public JArray jarray;

    public GameObject starPrefab;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            GenerateMap();
        }
    }

    public void GenerateMap() {
        GenerateGalaxy();
        GenerateJson();
    }

    public void GenerateJson() {
        jarray = toJArray(stars);
        json = new JSON();
        json.Add("stars", jarray);
        JArray blank = new JArray();
        json.Add("wormholes", blank);
        output = json.CreateString();
        GUIUtility.systemCopyBuffer = output;
    }

    public JArray toJArray(List<Star> starlist) {
        JArray newja = new JArray();

        for (int i = 0; i < starlist.Count; i++) {
            stars[i].y *= -1f;
            newja.Add(JSON.Serialize(stars[i]));
            stars[i].y *= -1f;
        }

        return newja;
    }

    public Star CreateStar(float x, float y) {

        int i = stars.Count;

        GameObject newStarGO = Instantiate(starPrefab);
        Star s = newStarGO.GetComponent<Star>();
        s.x = x;
        s.y = y;
        s.r = Random.Range(1, 50);
        s.uid = i;
        s.name = "test_" + i.ToString();
        s.e = 0;
        s.i = 0;
        s.s = 0;
        s.st = 0;
        s.g = 0;
        s.puid = (i % playerCount) + 1;

        s.scaleMultipler = rscale;
        s.scaleBaseSize = rbase;

        s.Refresh();

        stars.Add(s);

        return s;
    }

    public void DestroyStar(Star s) {
        stars.Remove(s);
        Destroy(s.gameObject);
        RefreshStarIDs();
    }

    public void DestroyAllStars() {
        if(stars == null) {
            return;
        }
        foreach(Star s in stars) {
            Destroy(s.gameObject);
        }
        stars = new List<Star>();
    }

    public void RefreshStarIDs() {
        for (int i = 0; i < stars.Count; i++) {
            stars[i].uid = i;
            if (!stars[i].customName) {
                stars[i].name = "test_" + i.ToString();
            }
        }
    }

    private void GenerateGalaxy() {
        DestroyAllStars();

        transform.position = new Vector3(0f, 0f, transform.position.z);
        Camera.main.orthographicSize = width / 2f;

        for (int i = 0; i < starCount; i++) {
            Star newStar = CreateStar(Random.Range(-(width / 2f), (width / 2f)), Random.Range(-(width / 2f), (width / 2f)));
        }

    }

    /*private void OnGUI() {
        if (GUI.Button(new Rect(5f, 5f, 120f, 30f), "JSON")) {
            GUIUtility.systemCopyBuffer = output;
        }
        width = int.Parse(GUI.TextField(new Rect(5f, 35f, 120f, 30f), width.ToString()));
    }*/

    private void OnDrawGizmos() {
        if (stars != null) {
            foreach (Star s in stars) {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(new Vector3(s.x, s.y, 0f), (float)s.r*rscale);
            }
        }
    }
}
