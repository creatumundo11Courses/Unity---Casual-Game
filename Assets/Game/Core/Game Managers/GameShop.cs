using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-9999)]
public class GameShop : MonoBehaviour
{
    public static GameShop Instance;

    public enum MyProductCategory
    { 
        Character 
        //add more categories...                                             
    }
    [Serializable]
    public sealed class MyProduct
    {
        public string Id;
        public MyProductCategory Category;
        public int Price;
        public int IsPurchased = 0;//0 false : 1 true
        public int IsEquipped= 0;//0 false : 1 true
        public Button PurchasedBtn;
    }
    [SerializeField]
    private List<MyProduct> Products;

    public event Action<MyProduct> OnPurchaseProduct; 
    public event Action<MyProduct> OnEquipProduct;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (var product in Products)
        {
            int isPurchased = PlayerPrefs.GetInt(product.Id + "_isPurchased", 0);
            int isEquipped = PlayerPrefs.GetInt(product.Id + "_isEquipped" + product.Category, 0);
            product.IsPurchased = isPurchased;
            product.IsEquipped = isEquipped;
            product.PurchasedBtn.onClick.AddListener(() => Purchase(product.Id));
        }

        MyProduct characterEquipped = GetProductByCategory(MyProductCategory.Character);
        if (characterEquipped == null)
        {
            Purchase("Default");
            Equip("Default");
        }
        else
        {
            Purchase(characterEquipped.Id);
            Equip(characterEquipped.Id);
        }
    }

    public MyProduct GetProductByCategory(MyProductCategory category)
    {
        MyProduct product = Products.Find(x => x.Category == category && x.IsEquipped == 1);

        if (product == null) return null;

        return product;
    }

    private void Purchase(string id)
    {
        MyProduct product = Products.Find(x => x.Id == id);
        if (product == null) return;        
        if (product.IsPurchased == 1)
        {
            Equip(product.Id);
            return;
        }

        if (GameMode.Coins < product.Price) return;

        GameMode.Instance.RemoveCoins(product.Price);
        product.IsPurchased = 1;
        product.PurchasedBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";

        PlayerPrefs.SetInt(product.Id + "_isPurchased",product.IsPurchased);
        OnPurchaseProduct?.Invoke(product);
    }

    private void Equip(string id)
    {
        MyProduct product = Products.Find(x => x.Id == id);
        if (product == null) return;

        UnequipAllByCategory(product.Category);

        product.IsEquipped = 1;
        product.PurchasedBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        PlayerPrefs.SetInt(product.Id + "_isEquipped" + product.Category, product.IsEquipped);
        PlayerPrefs.Save();
        OnEquipProduct?.Invoke(product);

    }

    private void UnequipAllByCategory(MyProductCategory category)
    {
        foreach (MyProduct product in Products.FindAll(x => x.Category == category))
        {
            product.IsEquipped = 0;
            product.PurchasedBtn.GetComponentInChildren<TextMeshProUGUI>().text =
                product.IsPurchased == 1
                ? "Equip"
                : product.Price.ToString();
            PlayerPrefs.SetInt(product.Id + "_isEquipped" + product.Category, 0);
        }
    }
}
