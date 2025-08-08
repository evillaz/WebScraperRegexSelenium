using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace WebScraperRegexSelenium
{
  class Product
  {
    public string Name { get; set; }
    public string Price { get; set; }
  }
  class Program
  {
    static void Main(string[] args)
    {
      var options = new ChromeOptions();
      options.AddArgument("--headless");
      options.AddArgument("--no-sandbox");
      using var driver = new ChromeDriver(options);

      driver.Navigate().GoToUrl("https://webscraper.io/test-sites/e-commerce/allinone/computers/laptops");
      Thread.Sleep(3000);
      var products = new List<Product>();

      var productElements = driver.FindElements(By.CssSelector(".product-wrapper.card-body"));
      foreach (var productElement in productElements)
      {
        try
        {
          var price = productElement.FindElement(By.CssSelector("span[itemprop='price']")).Text;

          // Extract name
          var name = productElement.FindElement(By.CssSelector("a.title")).Text;

          products.Add(new Product
          {
            Name = name,
            Price = price
          });
        }
        catch (NoSuchElementException)
        {
          // Skip any element that doesn’t match
        }
      }

      // Output all
      foreach (var product in products)
      {
        Console.WriteLine($"Name: {product.Name} | Price: {product.Price}");
      }

      driver.Quit();
    }
  }
}
