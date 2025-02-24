﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        public int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize=(value>MaxPageSize) ? MaxPageSize: value;
        }
		private List<string> _brands = [];

		public List<string> Brands 
		{
			get { return _brands; }  // types = boards,gloves
			set 
			{
				_brands = value.SelectMany(x=>x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList(); 
			}
		}
        private List<string> _types = [];

        public List<string> Types
        {
            get { return _types; }  // types = boards,gloves
            set
            {
                _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }
        public string? Sort {  get; set; }

        private string? _search;

        public string Search
        {
            get { return _search ?? ""; }
            set { _search = value.ToLower(); }
        }


    }
}
