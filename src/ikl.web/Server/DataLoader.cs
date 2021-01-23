using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ikl.web.Shared;

namespace ikl.web.Server
{
    public class DataLoader
    {
        private static readonly HashSet<string> ExcludeList = new HashSet<string>
        {
            "e0dd3f7469be4d218ce70997420ddb7d", // Møbler Far
            "19f1ac308b704221ae8e0ec9d1dcebb7", // Eenfamiliehus Skovborg
            "e6b6d13c24da41ddad1f2203578dd82f", // Linnet & Laursen
            "9f55753ea14541f183f883ff03dda470", // Eget Hus
            "5890f96e30da4a249d654738c265b8e9", // Georg Jensen
            "2ad952f517c142728e92010c1b52b18f", // Hårtotten
            "ef9384128f0f484a96efe34395d76ff5", // Hede Nielsen
            "b1f1525d07aa4431a1446d4fd3031599", // Taastrup Bio
            "4a91e9ff4023408483941404cc9cf39b", // Megiddo
            "4fb495d629b748ce8eaee5587873b22b", // Hus Østergaard
        };
        public static Data Load(string drawingPath = "drawings.json", string customerPath = "customers.json")
        {
            var drawingsJson = File.ReadAllText(drawingPath);
            var drawings = JsonSerializer
                .Deserialize<Drawing[]>(drawingsJson)?
                .Where(d => !(d.Ratios.Length == 1 && d.Ratios[0] == "1:1"))
                .Where(d => !ExcludeList.Contains(d.CustomerId))
                .ToArray() ?? Array.Empty<Drawing>();

            var customersJson = File.ReadAllText(customerPath);
            var customers = JsonSerializer
                .Deserialize<Customer[]>(customersJson)?.Where(c => !ExcludeList.Contains(c.Id))
                .ToArray() ?? Array.Empty<Customer>();
            
            foreach (var item in customers)
            {
                item.Year += 1900;
            }

            foreach (var drawing in drawings)
            {
                var customer = customers.First(c => c.Id == drawing.CustomerId);
                foreach (var customerName in customer.Names)
                {
                    drawing.Tags.Add(customerName);
                }
                drawing.Tags.Add(customer.Year.ToString());
                foreach (var ratio in drawing.Ratios)
                {
                    drawing.Tags.Add(ratio);
                }
                if (drawing.Title.Contains("chair", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("stol", StringComparison.InvariantCultureIgnoreCase) || 
                    drawing.Title.Contains("Hocker", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("Sessel", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("stuhl", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("tabouret", StringComparison.InvariantCultureIgnoreCase))
                {
                    drawing.Tags.Add("stol");
                    drawing.Tags.Add("chair");
                }

                if (drawing.Title.Contains("tisch", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("bord", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("table", StringComparison.InvariantCultureIgnoreCase)
                )
                {
                    drawing.Tags.Add("bord");
                    drawing.Tags.Add("table");
                }
                
                if (drawing.Title.Contains("gestell", StringComparison.InvariantCultureIgnoreCase))
                {
                    drawing.Tags.Add("ramme");
                }

                if (drawing.Title.Contains("Lits", StringComparison.InvariantCultureIgnoreCase))
                {
                    drawing.Tags.Add("seng");
                }

                if (drawing.Title.Contains("Armoire", StringComparison.InvariantCultureIgnoreCase))
                {
                    drawing.Tags.Add("Garderobe");
                }
            }

            var data = new Data(drawings, customers);
            return data;
        }
    }
}