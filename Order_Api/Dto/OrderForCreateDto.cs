﻿namespace Order_Api.Dto
{
    public class OrderForCreateDto
    {
        public decimal Price { get; set; }
        public string? ProductName { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
