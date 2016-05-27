using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ProductInterfaces;
using System.Collections;
using ProductInterfaces;

namespace ProductService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFProductService" in both code and config file together.
    public class WCFProductService : IWCFProductService
    {

        public List<string> ListProducts()
        {
            Console.WriteLine("List function() has been called by a client.");
            List<string> productsList = new List<string>();
            try
            {
                using(adventureworksEntities2 database = new adventureworksEntities2())
                {
                    var products = from p in database.products
                                   select p.ProductNumber;
                    productsList = productsList.ToList();

                    #region Alternate Solution
                    foreach (var p in database.products)
                    {
                        productsList.Add(p.ProductNumber);
                    }
                    #endregion

                    #region Create Product
                    /*product pr = new product();
                    pr.Name = "asda";

                    database.products.Add(pr);*/
                    #endregion
                }

            }
            catch
            {

            }

            return productsList;
        }





        public ProductData GetProduct(string productNumber)
        {
            Console.WriteLine("GetProduct() functiion called by client.");

            ProductData pr = null;
            try
            {
                using (adventureworksEntities2 database = new adventureworksEntities2())
                {
                    var products = from p in database.products
                                   where p.ProductNumber == productNumber
                                   select p;

                    product prod = products.FirstOrDefault();

                    pr = new ProductData();
                    pr.Name = prod.Name;
                    pr.ProductNumber = prod.ProductNumber;
                    pr.color = prod.Color;


                    /*Console.WriteLine(pr.Name);
                    Console.WriteLine(pr.ProductNumber);
                    Console.WriteLine(pr.color);*/
                    /*var products = from p in database.products
                                   select p.ProductNumber;
                    productsList = productsList.ToList();*/

                }
            }
            catch
            {

            }

            return pr;
            
        }
    }
}
