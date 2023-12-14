using Bookstore.Data;
using Bookstore.Models;

using BookstoreContext context = new BookstoreContext();





//UPDATE DATA
/*
var book = context.Items
    .Where(i => i.Price > 200.00M)
    .FirstOrDefault();
if (book is Item)
    book.Description = "Book is intersting";

// to delete context.Remove(book)
context.SaveChanges();*/

// GET DATA
// fluent API
//var items = context.Items.OrderBy(i => i.Name);
//LINQ syntax
/*
var items = from item in context.Items
            where item.Price > 150.00M
            orderby item.Name
            select item;

foreach (Item i in items)
{
    Console.WriteLine($"Id: {i.Id}");
    Console.WriteLine($"Name: {i.Name}");
    Console.WriteLine($"Price: {i.Price}");
    Console.WriteLine(new string('-', 20));
}*/

// ADD DATA
// add manually data to the table
Item item = new Item()
{
    Name = "To Kill a Mockingbird",
    Description = "A book called 'To Till a MockingBird' written by Harper Lee is a true masterpiece",
    Price = 250.00M
};
context.Items.Add(item);
Item item2 = new Item()
{
    Name = "1984",
    Description = "One of the most popular books written by George Orwell",
    Price = 175.50M
};
context.Items.Add(item2);


