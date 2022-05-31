using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{

    [SerializeField] Text celestialName;
    [SerializeField] Slider celestialSlider;
    private Celestial celestial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }


    void OnMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {              
            if (hit.transform.gameObject.GetComponent<Celestial>() != null)
            {
                celestialName.text = hit.transform.name;
                celestial = hit.transform.gameObject.GetComponent<Celestial>();
                celestialSlider.value = celestial.scaleFactor;
            }
        }
    }

    public void OnSliderChange()
    {
        if(celestial != null)
        {
            celestial.ScaleCelestial(celestialSlider.value);
        }
    }
}

