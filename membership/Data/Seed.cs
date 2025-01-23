using membership.Data;
using membership.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class SeedData
{
    public static void Initialize(MembershipDbContext context)
    {

        // Seed Levels
        if (!context.Levels.Any())
        {
            var levels = new List<Level>
        {
            new Level { LevelTitle = "Bronze", MinTotalTransaction = 0, MaxTotalTransaction = 4000 },
            new Level { LevelTitle = "Silver", MinTotalTransaction = 4001, MaxTotalTransaction = 10000 },
            new Level { LevelTitle = "Gold", MinTotalTransaction = 10001, MaxTotalTransaction = 20000 }
        };

            context.Levels.AddRange(levels);
            context.SaveChanges();
        }



        if (!context.Members.Any())
        {
            for (int i = 1; i < 5; i++)
            {
                Member member = new Member();
                member.Username = $"Member{i}";
                member.TotalPoints = 0;
                member.PasswordHash = $"password{i}";
                member.IsAdmin = false;
                context.Members.Add(member);
                context.SaveChanges();
            }
            Member admin = new Member();
            admin.Username = "admin";
            admin.TotalPoints = 0;
            admin.PasswordHash = "admin";
            admin.IsAdmin = true;
            context.Members.Add(admin);
            context.SaveChanges();
        }

        if (!context.Transactions.Any())
        {
            List<Member>members = context.Members.ToList();
            foreach (var member in members)
            {
                Random random = new Random();
                int randomNumber = random.Next(5, 26); 

                for (int i = 1; i < randomNumber; i++)
                {
                    Transaction transaction = new Transaction();
                    transaction.Total = random.Next(50, 5000);
                    transaction.Member = member;
                    transaction.TransactionTitle = $"Transaction {i}";
                    member.TotalPoints += (int)Math.Floor(transaction.Total * 0.05);
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                }

            }
        }

        if (!context.RedemptionItems.Any()) {
            var levels = context.Levels.ToList();
            var BronzeItems = new List<RedemptionItem>
            {
                // Bronze items
                new RedemptionItem { Title = "Water Bottle", Description = "A durable 1L stainless steel water bottle for everyday use.", RequiredPoints = 100, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Coffee Mug", Description = "Ceramic coffee mug with a sleek design.", RequiredPoints = 150, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Notebook", Description = "A 100-page ruled notebook for notes and ideas.", RequiredPoints = 200, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Pen Set", Description = "A set of premium gel pens for smooth writing.", RequiredPoints = 250, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Keychain", Description = "A stylish metal keychain with a company logo.", RequiredPoints = 300, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Reusable Grocery Bag", Description = "An eco-friendly reusable grocery bag.", RequiredPoints = 350, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Hand Sanitizer Pack", Description = "A pack of 3 portable hand sanitizers.", RequiredPoints = 400, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Phone Stand", Description = "A foldable phone stand for hands-free use.", RequiredPoints = 450, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Desk Organizer", Description = "A compact desk organizer to keep your workspace tidy.", RequiredPoints = 500, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Sunglasses", Description = "A pair of stylish unisex sunglasses.", RequiredPoints = 550, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Baseball Cap", Description = "A comfortable and stylish baseball cap.", RequiredPoints = 600, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "USB Flash Drive", Description = "16GB USB flash drive for storing files.", RequiredPoints = 650, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Mini Plant Pot", Description = "A small potted plant to brighten up your desk.", RequiredPoints = 700, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "T-shirt", Description = "A cotton T-shirt with a cool design.", RequiredPoints = 750, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Coaster Set", Description = "A set of 4 stylish drink coasters.", RequiredPoints = 800, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Wall Clock", Description = "A sleek and modern wall clock.", RequiredPoints = 850, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Car Air Freshener", Description = "A pack of 3 long-lasting car air fresheners.", RequiredPoints = 900, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Headphones", Description = "A pair of wired over-ear headphones.", RequiredPoints = 950, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Portable Coffee Cup", Description = "A leak-proof travel coffee cup.", RequiredPoints = 1000, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Notebook and Pen Combo", Description = "A matching notebook and pen set.", RequiredPoints = 1050, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Eco-Friendly Lunchbox", Description = "A BPA-free reusable lunchbox.", RequiredPoints = 1100, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Travel Pillow", Description = "A compact and comfortable travel pillow.", RequiredPoints = 1150, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Waterproof Phone Pouch", Description = "A pouch to keep your phone safe during water activities.", RequiredPoints = 1200, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Yoga Mat", Description = "A lightweight yoga mat for fitness enthusiasts.", RequiredPoints = 1250, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Insulated Cooler Bag", Description = "A portable cooler bag to keep your food and drinks cool.", RequiredPoints = 1300, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "LED Desk Lamp", Description = "A USB-powered LED desk lamp.", RequiredPoints = 1350, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Travel Adapter", Description = "A universal travel adapter for your electronics.", RequiredPoints = 1400, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Fitness Resistance Bands", Description = "A set of 3 resistance bands for workouts.", RequiredPoints = 1450, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Bluetooth Selfie Stick", Description = "A Bluetooth-enabled selfie stick for capturing moments.", RequiredPoints = 1500, Level = levels.First(l => l.LevelTitle == "Bronze") },
                new RedemptionItem { Title = "Collapsible Water Bottle", Description = "A space-saving collapsible water bottle.", RequiredPoints = 1550, Level = levels.First(l => l.LevelTitle == "Bronze") }
            };
            context.RedemptionItems.AddRange(BronzeItems);
            context.SaveChanges();

            var SilverItems = new List<RedemptionItem>
            {

                new RedemptionItem { Title = "Bluetooth Speaker", Description = "A portable Bluetooth speaker with rich sound quality.", RequiredPoints = 2000, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Electric Kettle", Description = "A compact electric kettle for quick boiling.", RequiredPoints = 2100, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Wireless Mouse", Description = "An ergonomic wireless mouse for daily use.", RequiredPoints = 2200, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Smart Scale", Description = "A smart scale to track weight and health metrics.", RequiredPoints = 2300, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Thermos Flask", Description = "A 1.5L insulated thermos flask to keep beverages hot or cold.", RequiredPoints = 2400, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Power Bank", Description = "10,000mAh power bank for charging on the go.", RequiredPoints = 2500, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Travel Backpack", Description = "A durable and spacious backpack for travel and work.", RequiredPoints = 2600, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Desk Fan", Description = "A USB-powered compact desk fan for cooling.", RequiredPoints = 2700, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Electric Hair Trimmer", Description = "A rechargeable hair trimmer for precise grooming.", RequiredPoints = 2800, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Mini Projector", Description = "A portable mini projector for movies and presentations.", RequiredPoints = 2900, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Smart Watch", Description = "A fitness tracker and smart watch with notifications.", RequiredPoints = 3000, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Noise Cancelling Headphones", Description = "Wireless headphones with active noise cancellation.", RequiredPoints = 3100, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Cordless Vacuum Cleaner", Description = "A lightweight cordless vacuum for quick cleaning.", RequiredPoints = 3200, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Electric Toothbrush", Description = "A rechargeable electric toothbrush for better oral hygiene.", RequiredPoints = 3300, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Digital Picture Frame", Description = "A 7-inch digital frame to display your favorite photos.", RequiredPoints = 3400, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Instant Pot", Description = "A multifunctional electric pressure cooker for quick meals.", RequiredPoints = 3500, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Car Jump Starter", Description = "A compact car jump starter with USB charging capabilities.", RequiredPoints = 3600, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Steam Iron", Description = "A powerful steam iron for wrinkle-free clothes.", RequiredPoints = 3700, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Kitchen Blender", Description = "A high-performance blender for smoothies and shakes.", RequiredPoints = 3800, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Weighted Blanket", Description = "A weighted blanket for better sleep and relaxation.", RequiredPoints = 3900, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Gaming Keyboard", Description = "An RGB mechanical gaming keyboard for precise controls.", RequiredPoints = 4000, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Air Purifier", Description = "A compact air purifier to improve indoor air quality.", RequiredPoints = 4100, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Streaming Media Player", Description = "A device to stream your favorite shows and movies.", RequiredPoints = 4200, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Wireless Earbuds", Description = "True wireless earbuds with long battery life.", RequiredPoints = 4300, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Indoor Exercise Bike", Description = "A compact exercise bike for home workouts.", RequiredPoints = 4400, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Smart Home Camera", Description = "A wireless indoor camera with app control.", RequiredPoints = 4500, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Luggage Set", Description = "A 2-piece lightweight luggage set for travel.", RequiredPoints = 4600, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Electric Griddle", Description = "A non-stick electric griddle for family meals.", RequiredPoints = 4700, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Smart Thermostat", Description = "A thermostat to control home temperature remotely.", RequiredPoints = 4800, Level = levels.First(l => l.LevelTitle == "Silver") },
                new RedemptionItem { Title = "Portable Fire Pit", Description = "A compact and easy-to-use fire pit for outdoor use.", RequiredPoints = 4900, Level = levels.First(l => l.LevelTitle == "Silver") }
            };
            context.RedemptionItems.AddRange(SilverItems);
            context.SaveChanges();

            var GoldItems = new List<RedemptionItem>
            {
                new RedemptionItem { Title = "Smartphone", Description = "A high-end smartphone with the latest features.", RequiredPoints = 5000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Laptop", Description = "A lightweight laptop with powerful performance for work and play.", RequiredPoints = 5500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "4K Smart TV", Description = "A 55-inch 4K UHD Smart TV for an immersive viewing experience.", RequiredPoints = 6000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Gaming Console", Description = "A next-gen gaming console for hours of entertainment.", RequiredPoints = 6500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Watch", Description = "A premium luxury watch to complement your style.", RequiredPoints = 7000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "High-End Sound System", Description = "A surround sound system for an amazing audio experience.", RequiredPoints = 7500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Vacation Package", Description = "A 3-day, 2-night luxury vacation package.", RequiredPoints = 8000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Electric Bike", Description = "A sleek electric bike for eco-friendly commuting.", RequiredPoints = 8500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Professional Camera", Description = "A DSLR camera with advanced features for photography enthusiasts.", RequiredPoints = 9000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Gaming PC", Description = "A powerful gaming PC with top-notch specifications.", RequiredPoints = 9500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Premium Coffee Machine", Description = "An espresso machine for brewing the perfect cup of coffee.", RequiredPoints = 10000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Handbag", Description = "A designer handbag crafted with premium materials.", RequiredPoints = 10500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Smart Refrigerator", Description = "A smart refrigerator with advanced cooling technology.", RequiredPoints = 11000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Home Theater System", Description = "A full home theater system with a projector and speakers.", RequiredPoints = 11500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Spa Day", Description = "A premium spa day experience for ultimate relaxation.", RequiredPoints = 12000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Drone", Description = "A high-quality drone with 4K video recording capabilities.", RequiredPoints = 12500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "High-End Fitness Equipment", Description = "A treadmill with advanced features for home workouts.", RequiredPoints = 13000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Mattress", Description = "A premium mattress for the ultimate sleep experience.", RequiredPoints = 13500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Electric Scooter", Description = "A modern electric scooter for city travel.", RequiredPoints = 14000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Smart Home Bundle", Description = "A set of smart home devices including a smart speaker, lights, and thermostat.", RequiredPoints = 14500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Premium Wine Set", Description = "A collection of fine wines for connoisseurs.", RequiredPoints = 15000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Jewelry", Description = "A piece of exquisite jewelry for special occasions.", RequiredPoints = 15500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Outdoor Grill", Description = "A high-end outdoor grill for barbeque lovers.", RequiredPoints = 16000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Advanced Air Purifier", Description = "An air purifier with HEPA filters and smart features.", RequiredPoints = 16500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Smartwatch Pro Edition", Description = "A smartwatch with premium health-tracking features.", RequiredPoints = 17000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Premium Art Supplies", Description = "A set of high-quality art supplies for artists.", RequiredPoints = 17500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Luggage", Description = "A durable and stylish luggage set for frequent travelers.", RequiredPoints = 18000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Premium Electric Grill", Description = "An electric grill with smart temperature controls.", RequiredPoints = 18500, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Electric Standing Desk", Description = "A motorized standing desk for a healthier workspace.", RequiredPoints = 19000, Level = levels.First(l => l.LevelTitle == "Gold") },
                new RedemptionItem { Title = "Luxury Dining Experience", Description = "A fine dining experience at a 5-star restaurant.", RequiredPoints = 19500, Level = levels.First(l => l.LevelTitle == "Gold") }
            };
            context.RedemptionItems.AddRange(GoldItems);
            context.SaveChanges();
        }

    }
}
