using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text ammoText;
    private UIManager _instance;
    private bool isCursorLocked = true;

    public UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                gameObject.AddComponent<UIManager>();
            }

            _instance = this;
            return _instance;
        }
    }

    private void OnEnable()
    {
        PlayerController.onPlayerShoot += UpdateAmmoUI;
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + Rifle.CheckAmmo();
    }

    void Start ()
	{
        _instance = this;
        ammoText = GetComponentInChildren<Text>();
        ammoText.text = "Ammo: " + Rifle.CheckAmmo();
    }
	
	void Update ()
	{
        LockCursor();
    }

    private void LockCursor()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
        }

        if (isCursorLocked == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (isCursorLocked == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
