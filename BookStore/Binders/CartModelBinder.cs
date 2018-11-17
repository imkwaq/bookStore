using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "CartModels";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            // Получить объект Cart из сеанса
            CartModels cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (CartModels)controllerContext.HttpContext.Session[sessionKey];
            }

            // Создать объект Cart если он не обнаружен в сеансе
            if (cart == null)
            {
                cart = new CartModels();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            // Возвратить объект Cart
            return cart;
        }
    }
}