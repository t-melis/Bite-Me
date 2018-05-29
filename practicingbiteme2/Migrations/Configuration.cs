namespace practicingbiteme2.Migrations
{
    using practicingbiteme2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<practicingbiteme2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(practicingbiteme2.Models.ApplicationDbContext context)
        {
            var products = new List<Product>
            {
                new Product{Name = "Japanese Cotton Cheesecake", Description = "Puffy delicious Japanese recipe", Price = 25.50m, Category = Category.Cake, ImageURL = "/Photos/Products/Cakes/JapaneseCottonCheesecake.jpg"},
                new Product{Name = "Be my Valentine", Description = "Available only on Valentine's day", Price = 45.50m, Category = Category.Cake, ImageURL = "/Photos/Products/Cakes/Valentine.jpg"},
                new Product{Name = "Pistachio Tart", Description = "Full of pistachios and walnuts", Price = 18.00m, Category = Category.Cake, ImageURL = "/Photos/Products/Cakes/PistachioTart.jpg"},
                new Product{Name = "Pumpkin Spiced Bar", Description = "Delicious and moist spice bar for the fans of pumkin pie", Price = 22.80m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/PumpkinSpicedBar.jpg"},
                new Product{Name = "Macadamia Sponge Cake", Description = "Spongy cake layers filled with ...", Price = 15.50m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/MacadamiaSpongeCake.jpg"},
                new Product{Name = "Black Mamba", Description = "Dark chocolate brownies filled with walnuts", Price = 20.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/DarkChocolateWalnutsBrownies.jpg"},
                new Product{Name = "Brioche Cinamon Rolls", Description = "Perfect for English breakfast", Price = 28.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/BriocheCinamonRolls.jpg"},
                new Product{Name = "Milky Brioche Buns", Description = "Can be served with hot chocolate and fruits", Price = 25.50m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/MIlkyBriocheBuns.jpg"},
                new Product{Name = "Passion on the rocks", Description = "Pear Crumbles With Toffee", Price = 18.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/PearCrumblesWithToffee.jpg"},
                new Product{Name = "Oceanic sweetness", Description = "Portuguese Egg Tart", Price = 18.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/PortugueseEggTart.jpg"},
                new Product{Name = "Red Passion", Description = "Rose Water Cheesecake", Price = 18.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/RoseWaterCheesecake.jpg"},
                new Product{Name = "Impetuous Sichuan", Description = "Tiramisu filled with an idea of Baileys", Price = 18.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/TiramisuBaileys.jpg"},
                new Product{Name = "Twisted Banoffee", Description = "Old school recipe", Price = 18.00m, Category = Category.Pastry, ImageURL = "/Photos/Products/Pastry/TwistedBanoffee.jpg"},
                new Product{Name = "Canarian", Description = "Levander and Almond Cookies", Price = 15.50m, Category = Category.Cookie, ImageURL = "/Photos/Products/Cookies/LevanderAlmondCookies.jpg"},
                new Product{Name = "Matcha & Pistachio", Description = "Matcha cookies with pistachios. Suitable for any sugar free diet", Price = 12.00m, Category = Category.Cookie, ImageURL = "/Photos/Products/Cookies/MatchaPistachioCookies.jpg"},
                new Product{Name = "Crispy desire", Description = "Black & White quinoa lace cookies", Price = 12.00m, Category = Category.Cookie, ImageURL = "/Photos/Products/Cookies/Black&WhiteQuinoaLaceCookies.jpg"},
            };
            products.ForEach(p => context.Products.AddOrUpdate(s => s.Name, p));
            context.SaveChanges();
        }
    }
}
