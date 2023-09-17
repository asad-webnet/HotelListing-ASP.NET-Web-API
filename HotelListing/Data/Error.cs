﻿using Newtonsoft.Json;

namespace HotelListing.Data
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
