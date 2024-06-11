

using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private Transform kitchenObjectParent;
    [SerializeField] private Image iconTemplate;

    private void Start() {
        iconTemplate.gameObject.SetActive(false);
    }
    public void UpdateUI(Order order) {
        foreach(KitchenObjectDef kitchenObjectDef in order.recipe.kitchenObjectDefs) {
            Image newIcon = GameObject.Instantiate(iconTemplate);
            newIcon.transform.SetParent(kitchenObjectParent);
            newIcon.sprite = kitchenObjectDef.sprite;
            newIcon.gameObject.SetActive(true);
        }

    }
    
}
