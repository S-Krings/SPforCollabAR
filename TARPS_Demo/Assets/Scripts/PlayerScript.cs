using Mirror;
using TMPro;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
    public TextMeshPro playerNameText;
    public GameObject floatingInfo;

    private Material playerMaterialClone;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    const string playerPrefsNameKey = "PlayerName";
    const string playerPrefsColourKey = "PlayerColour";

    [SerializeField] Material playerMaterial;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
        //playerMaterialClone = new Material(GetComponent<Renderer>().material);
        //playerMaterialClone.color = _New;
        //GetComponent<Renderer>().material = playerMaterialClone;
        playerMaterial.color = _New;
    }

    public override void OnStartLocalPlayer()
    {
        //Camera.main.transform.SetParent(transform);
        //Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        //GameObject mainCamera = GameObject.Find("Main Camera");
        //this.gameObject.transform.SetParent(Camera.main.transform);

        //floatingInfo.transform.localPosition = new Vector3(0, 0.5f, 0.6f);
        //floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        floatingInfo.SetActive(false);

        if (PlayerPrefs.HasKey(playerPrefsNameKey)) playerName = PlayerPrefs.GetString(playerPrefsNameKey);
        else playerName = "Player" + Random.Range(100, 999);

        if (PlayerPrefs.HasKey(playerPrefsColourKey)) playerColor = new ColourEnum().GetColour(PlayerPrefs.GetInt(playerPrefsColourKey));
        else playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        
        CmdSetupPlayer(playerName, playerColor);

        //handL.SetActive(false);
        //handR.SetActive(false);
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = _name;
        playerColor = _col;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            // make non-local players run this
            floatingInfo.transform.LookAt(Camera.main.transform);
            return;
        }

        //float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        //float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        //transform.Rotate(0, moveX, 0);
        //transform.Translate(0, 0, moveZ);
        //transform.rotation = Camera.main.transform.rotation;
        //transform.position = Camera.main.transform.position;
    }

    public void setPlayerColour(Color colour)
    {
        Debug.Log("Setting Player Material Colour");
        playerMaterial.color = colour;
    }
}