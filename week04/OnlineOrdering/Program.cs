using System;

class orderingProgram
{
    static void Main (string[] args)
    {
        Console.WriteLine("Online Ordering System");
         }
        // Address class
public class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

// Customer class
public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName()
    {
        return name;
    }

    public string GetAddress()
    {
        return address.GetFullAddress();
    }
}

// Product class
public class Product
{
    private string name;
    private string id;
    private decimal price;
    private int quantity;

    public Product(string name, string id, decimal price, int quantity)
    {
        this.name = name;
        this.id = id;
        this.price = price;
        this.quantity = quantity;
    }

    public decimal GetTotalCost()
    {
        return price * quantity;
    }

    public string GetName()
    {
        return name;
    }

    public string GetId()
    {
        return id;
    }
}

// Order class
public class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal totalCost = 0;
        foreach (var product in products)
        {
            totalCost += product.GetTotalCost();
        }
        decimal shippingCost = customer.LivesInUSA() ? 5 : 35;
        return totalCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (var product in products)
        {
            label += $"{product.GetName()} ({product.GetId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"{customer.GetName()}\n{customer.GetAddress()}";
    }
}

class orderingProgram
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Elm St", "London", "", "UK");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create products
        Product product1 = new Product("Product A", "A1", 10.99m, 2);
        Product product2 = new Product("Product B", "B2", 5.99m, 3);
        Product product3 = new Product("Product C", "C3", 7.99m, 1);

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product2);
        order2.AddProduct(product3);

        // Display order information
        Console.WriteLine("Order 1:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}");

        Console.WriteLine("\nOrder 2:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}");
    }
}

    }