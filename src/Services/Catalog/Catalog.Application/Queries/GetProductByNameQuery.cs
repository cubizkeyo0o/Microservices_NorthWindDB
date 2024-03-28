﻿using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductByNameQuery : IRequest<List<ProductResponse>>
    {
        public string Name { get; set; }
        public GetProductByNameQuery(string _name)
        {
            Name = _name;
        }
    }
}
