using UnityEngine;

public class Star : MonoBehaviour
{
    [UnityEngine.SerializeField]
    public int uid;//a unique integer > 0
    [UnityEngine.SerializeField]
    new public string name;//a name
    [UnityEngine.SerializeField]
    public float x;//x pos
    [UnityEngine.SerializeField]
    public float y;//y pos
    [UnityEngine.SerializeField]
    public int r;//resource value(5-60)
    [UnityEngine.SerializeField]
    public int e;//starting economy
    [UnityEngine.SerializeField]
    public int i;//starting economy
    [UnityEngine.SerializeField]
    public int s;//starting economy
    [UnityEngine.SerializeField]
    public int st;//starting ship strength
    [UnityEngine.SerializeField]
    public int g;//starting gate(0 or 1)
    [UnityEngine.SerializeField]
    public int puid;//the uid of the owning player(optional)

    public float scaleMultipler;
    public float scaleBaseSize;

    public bool customName = false;

    public void Refresh() {
        transform.localScale = Vector3.one * (scaleBaseSize + (r * scaleMultipler));
        transform.position = new Vector3(x, y, 0f);
    }
}
