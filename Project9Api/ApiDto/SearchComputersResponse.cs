﻿using Logic.Dto;
using System.Collections.Generic;

namespace Project9Api.ApiDto
{
    public class SearchComputersResponse : BaseResponse
    {
        public SearchComputersResponse() 
        {
            Computers = new List<ProductDto>();
        }

        public List<ProductDto> Computers { get; set; }

        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; } = false;
    }
}
