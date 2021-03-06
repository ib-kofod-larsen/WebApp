﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ikl.web.Shared
{
    public class Drawing
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("ratios")]
        public string[] Ratios { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public IEnumerable<string> GetSearchableValues()
        {
            yield return Description;
            yield return Path;
            yield return Title;
            foreach (var tag in Tags)
            {
                yield return tag;
            }

            foreach (var ratio in Ratios)
            {
                yield return ratio;
            }
        }
    }
}