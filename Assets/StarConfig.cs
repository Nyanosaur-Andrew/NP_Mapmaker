using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StarConfig : MonoBehaviour
{
    public Star currentStar;

    public TMP_InputField id;//int
    public TMP_InputField starname;//string
    public TMP_InputField xpos;//float
    public TMP_InputField ypos;//float
    public TMP_InputField resources;//int
    public TMP_InputField economy;//int
    public TMP_InputField industry;//int
    public TMP_InputField science;//int
    public TMP_InputField ships;//int
    public TMP_InputField gate;//int
    public TMP_InputField playerid;//int

    private Camera2D camScript;
    private Starfield galaxy;

    public GameObject StarPanel;
    public GameObject selection;
    private LineRenderer selectionLine;
    public float circle = 0.25f;

    private bool draggingStar = false;
    private Vector3 dragOffset;

    private void Start() {
        camScript = Camera.main.GetComponent<Camera2D>();
        galaxy = Camera.main.GetComponent<Starfield>();
        selectionLine = selection.GetComponent<LineRenderer>();
        MakeCircle(selectionLine, circle);

        StarPanel.SetActive(false);
        selection.SetActive(false);
    }

    private void Update() {
        MakeCircle(selectionLine, circle);
        bool cursorOverUI = EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0) && !cursorOverUI) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                Star s;
                if (hit.collider.gameObject.TryGetComponent<Star>(out s)) {
                    if (s == currentStar) {
                        camScript.enabled = false;
                        draggingStar = true;
                        dragOffset = new Vector3(hit.point.x, hit.point.y, 0f) - s.transform.position;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && draggingStar) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                Vector3 newPos = new Vector3(hit.point.x, hit.point.y, 0f);
                currentStar.transform.position = newPos - dragOffset;
                currentStar.x = currentStar.transform.position.x;
                currentStar.y = currentStar.transform.position.y;
                currentStar.Refresh();
            }
        }

        if (Input.GetMouseButtonUp(0) && !camScript.isDraggig) {
            draggingStar = false;
            camScript.enabled = true;
            if (!cursorOverUI) {
                RaycastHit hit;
                currentStar = null;
                ClearData();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                    Star s;
                    if (hit.collider.gameObject.TryGetComponent<Star>(out s)) {
                        SelectStar(s);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && !cursorOverUI) {
            RaycastHit hit;
            currentStar = null;
            ClearData();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                Star s;
                if (hit.collider.gameObject.TryGetComponent<Star>(out s)) {
                    galaxy.DestroyStar(s);
                }
            }
        }

        if (currentStar == null) {
            StarPanel.SetActive(false);
            selection.SetActive(false);
        }
        else {
            StarPanel.SetActive(true);
            selection.SetActive(true);
            selection.transform.position = currentStar.transform.position;
        }

    }

    public void SelectStar(Star s) {
        currentStar = s;
        ReadData();
    }

    void MakeCircle(LineRenderer lr, float radius) {
        lr.positionCount = 64;
        float angle = 0;
        for (int i = 0; i < 64; i++) {
            lr.SetPosition(i, new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius, 0f));
            angle += (Mathf.PI * 2) / 64;
        }
    }

    public void ReadData(){
        id.text = currentStar.uid.ToString();
        starname.text = currentStar.name.ToString();
        xpos.text = currentStar.x.ToString();
        ypos.text = currentStar.y.ToString();
        resources.text = currentStar.r.ToString();
        economy.text = currentStar.e.ToString();
        industry.text = currentStar.i.ToString();
        science.text = currentStar.s.ToString();
        ships.text = currentStar.st.ToString();
        gate.text = currentStar.g.ToString();
        playerid.text = currentStar.puid.ToString();
    }

    public void WriteData(){
        Debug.Log("writedata");
        if(currentStar == null) { return; }
        Debug.Log("writing...");
        currentStar.uid = int.Parse(id.text);
        currentStar.x = float.Parse(xpos.text);
        currentStar.y = float.Parse(ypos.text);
        currentStar.r = Mathf.Min(int.Parse(resources.text), 60);
        currentStar.e = int.Parse(economy.text);
        currentStar.i = int.Parse(industry.text);
        currentStar.s = int.Parse(science.text);
        currentStar.st = int.Parse(ships.text);
        currentStar.g = int.Parse(gate.text);
        currentStar.puid = int.Parse(playerid.text);

        if(currentStar.name != starname.text) {
            currentStar.name = starname.text;
            currentStar.customName = true;
        }

        currentStar.Refresh();

    }

    public void ClearData() {
        id.text = "";
        starname.text = "";
        xpos.text = "";
        ypos.text = "";
        resources.text = "";
        economy.text = "";
        industry.text = "";
        science.text = "";
        ships.text = "";
        gate.text = "";
        playerid.text = "";
    }
}
