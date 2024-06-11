using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
    [SerializeField] private Transform orderParent;
    [SerializeField] private OrderUI orderTemplateUI;

    private void Start() {
        orderTemplateUI.gameObject.SetActive(false);
        OrderManager.Instance.OnOrderSpawned += OrderManager_OnOrderSpawned;
        OrderManager.Instance.OnOrderSucceed += OrderManager_OnOrderSucceed;
    }

    private void OrderManager_OnOrderSucceed(object sender, System.EventArgs e) {
        UpdateUI();
    }

    private void OrderManager_OnOrderSpawned(object sender, System.EventArgs e) {
        UpdateUI();
    }

    private void UpdateUI() {
        foreach(Transform child in orderParent) {
            if(child != orderTemplateUI.transform) {
                Destroy(child.gameObject);
            }
        }
        List<Order> orderList =  OrderManager.Instance.GetCurrentOrders();
        foreach(Order order in orderList) {
            OrderUI orderUI = GameObject.Instantiate(orderTemplateUI);
            orderUI.transform.SetParent(orderParent);
            orderUI.gameObject.SetActive(true);
            orderUI.UpdateUI(order);
        }
    }
}
