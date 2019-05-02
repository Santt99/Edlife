using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ObjectInfo : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private string textBeforeName = "Name: ";
    public GameObject selectedObject, panelInfo;
    public Image objectIcon;
    public Sprite actualObject;
    public Text nameObject, moneyAvailable, showedValue, score, valueChanged;
    public Camera mainCamera;
    public Canvas canvas, canvasOther;
    private bool activated;
    private float time;
    public int quantity, valueAnimal, oxygenGiven, scoreAdding;
    public Slider life;
    // Use this for initialization
    void Start () {
        showedValue.text = valueAnimal.ToString();
        quantity = 0;
        activated = false;
        panelInfo.SetActive(false);
	}

    public void Update()
    {
        if (activated && int.Parse(moneyAvailable.text) >= valueAnimal)
        {
            StartCoroutine(Wait());
        }
    }
    
    public void OnPointerEnter (PointerEventData eventData)
    {
        panelInfo.SetActive(true);
        nameObject.text = textBeforeName + selectedObject.name;
        objectIcon.sprite = actualObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        panelInfo.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        canvas.enabled = false;
        canvasOther.enabled = true;
        activated = true;
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activated = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           
            if (Physics.Raycast(ray, out hit)&& activated)
            {
                if(hit.transform.tag  == "Ground")
                {
                    GameObject temporal  = Instantiate(selectedObject, hit.point , transform.rotation);
                    moneyAvailable.text = (int.Parse(moneyAvailable.text) - valueAnimal).ToString();
                    ThingsLists.things.Add(temporal);
                    quantity += 1;
                    valueChanged.text = quantity.ToString();
                    activated = false;
                    life.value += oxygenGiven;
                    score.text = (int.Parse(score.text) + scoreAdding).ToString();
                }
            }



        }
        
    }
}
