using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    [Serializable]
    public sealed class ConsumableProduct
    {
        public string Id;
        public string Name;
        public string Description;
        public float Price;
    }
    [Serializable]
    public sealed class NonConsumableProduct
    {
        public string Id;
        public string Name;
        public string Description;
        public float Price;
    }
    [SerializeField]
    private ConsumableProduct _200CoinsProduct;
    [SerializeField]
    private ConsumableProduct _1000CoinsProduct;
    [SerializeField]
    private NonConsumableProduct _noAdsProduct;

    private IStoreController _controller;
    private IExtensionProvider _extensions;

    public static IAPManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(_200CoinsProduct.Id, ProductType.Consumable);
        builder.AddProduct(_1000CoinsProduct.Id, ProductType.Consumable);
        builder.AddProduct(_noAdsProduct.Id, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void Purchase200Coins()
    {
        _controller.InitiatePurchase(_200CoinsProduct.Id);
    }
    public void Purchase1000Coins()
    {
        _controller.InitiatePurchase(_1000CoinsProduct.Id);
    }
    public void PurchaseNoAds()
    {
        _controller.InitiatePurchase(_noAdsProduct.Id);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _controller = controller;
        _extensions = extensions;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Product product = purchaseEvent.purchasedProduct;

        if (product.definition.id == _200CoinsProduct.Id)
        {
            //add 200 coins to player
        }
        else if (product.definition.id == _1000CoinsProduct.Id)
        {
            //add 1000 coins to player
        }
        else if (product.definition.id == _noAdsProduct.Id)
        {
            //No ads
        }

        Debug.Log($"Purchase Complete {product.definition.id}");
        return PurchaseProcessingResult.Complete;

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
       
    }

   
}


