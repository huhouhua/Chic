using Chic.Core.ObjectMapping;
using Chic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Domain.OrderAggregate
{
    public class Order : Entity<long>, IAggregateRoot, INeedMapper
    {

        public string UserId { get; set; }

        public string UserName { get; set; }

        public Address Address { get; set; }

        public int ItemCount { get; set; }


        protected Order()
        {
        }

        public Order(string userId, string userName, int itemCount, Address address)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.ItemCount = itemCount;
            this.Address = address;

            //this.AddDomainEvent(new OrderCreatedDomainEvent(this));
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;

        }
    }
}
