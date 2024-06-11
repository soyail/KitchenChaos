using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public event EventHandler OnOrderSpawned;
    public event EventHandler OnOrderSucceed;

    [SerializeField] private int orderMaxCount;
    [SerializeField] private float OrderSpawnRate;
    [SerializeField] private RecipeList recipeList;
    [SerializeField] private float maxWaitingTime;

    

    private List<Order> orders = new List<Order>();
    private List<Order> timeoutOrders = new List<Order>();
    private float orderTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        while(orders.Count < orderMaxCount) {
            SpawnOrder();
        }
    }

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        orderTimer += Time.deltaTime;
        foreach (Order order in orders) {
            order.waitingTime += Time.deltaTime;
        }
        if (orderTimer > OrderSpawnRate && orders.Count < orderMaxCount) {
            SpawnOrder();
            orderTimer = 0;
        }
        
        UpdateOrderStatus();
    }

    private void SpawnOrder() {
        int idx = UnityEngine.Random.Range(0, recipeList.recipeList.Count);
        Order newOrder = new Order(recipeList.recipeList[idx]);
        orders.Add(newOrder);
        OnOrderSpawned?.Invoke(this, EventArgs.Empty);
        Debug.Log($"Order {newOrder.orderId} with recipe {newOrder.recipe.recipeName} Created.");

    }

    public void RemoveFinishedOrder(Order order) {
        Debug.Log($"Order {order.orderId} with recipe {order.recipe.recipeName} finished.");
        OnOrderSucceed?.Invoke(this, EventArgs.Empty);
        orders.Remove(order);
    }

    private void UpdateOrderStatus() {
        // 完成的订单以及超时订单
        timeoutOrders.Clear();
        foreach (Order order in orders) {
            if(order.waitingTime > maxWaitingTime) {
                timeoutOrders.Add(order);
            }
        }
        HandleTimeoutOrder();

    }

    private void HandleTimeoutOrder() {
        foreach(Order order in timeoutOrders) {
            Debug.Log($"Order {order.orderId} with recipe {order.recipe.recipeName} timed out!");
            orders.Remove(order);
        }
    }
    public bool TryGetValidOrder(List<KitchenObjectDef> kitchenObjectDefs, out Order order) {
        foreach(Order curOrder in orders) {
            if(curOrder.recipe.kitchenObjectDefs.Count == kitchenObjectDefs.Count) {
                bool match = true;
                foreach (KitchenObjectDef curDef in curOrder.recipe.kitchenObjectDefs) {
                    if (!kitchenObjectDefs.Contains(curDef)) {
                        match = false;
                        break;
                    }
                }
                if(match) {
                    order = curOrder;
                    return true;
                }
            }
            
            
        }
        order = null;
        return false;
    }

    public List<Order> GetCurrentOrders() {
        return orders;
    }

}

public class Order {
    private static int nextOrderId = 1;
    public int orderId;
    public Recipe recipe;
    public float waitingTime;
    // Construction Function
    public Order(Recipe recipe) {
        this.orderId = nextOrderId++;
        this.recipe = recipe;
        this.waitingTime = 0;
    }
}