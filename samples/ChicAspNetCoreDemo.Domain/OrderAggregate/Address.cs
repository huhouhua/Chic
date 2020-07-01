﻿using System;
using System.Collections.Generic;
using System.Text;
using Chic.Domain.Abstractions;

namespace ChicAspNetCoreDemo.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }


        public string City { get; private set; }


        public string ZipCode { get; private set; }

        public Address()
        {

        }

        public Address(string street, string city, string zipcode)
        {
            Street = street;
            City = city;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return ZipCode;
        }


    }
}
