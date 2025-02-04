﻿using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;
using Moq;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class CustomerEndPointTest
{
    [TestMethod]
    public void Customer_GetCustomerList_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        var page = 1;
        var pageSize = 5;
        NameValueCollection parameters = new NameValueCollection
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<Customer>>>($"/customers", parameters);
        object mockResult = TestHelpers.GetApiPagedResult(new List<Customer> { testCustomer });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.GetCustomerList(page, pageSize);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Customer_AddCustomer_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        var customerForCreation = new CustomerForCreation();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<Customer>>($"/customers", customerForCreation, null);
        object mockResult = TestHelpers.GetApiResult(testCustomer);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.AddCustomer(customerForCreation);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Customer_GetCustomer_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<Customer>>($"/customers/{testCustomer.Id}", null);
        object mockResult = TestHelpers.GetApiResult(testCustomer);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.GetCustomer(testCustomer.Id.Value);
            Assert.IsNotNull(result.Data);
        });
    }

    [TestMethod]
    public void Customer_UpdateCustomer_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<Customer>>($"/customers/{testCustomer.Id}", testCustomer, null);
        object mockResult = TestHelpers.GetApiResult(testCustomer);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            
            testCustomer.Id = null;
            Assert.ThrowsException<InvalidValueException>(() => uut.UpdateCustomer(testCustomer));
            
            testCustomer.Id = 4711;
            var result = uut.UpdateCustomer(testCustomer);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Customer_GetOrdersForCustomer_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        var testOrder = new Order();
        var page = 1;
        var pageSize = 5;
        NameValueCollection parameters = new NameValueCollection
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        };
            
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<Order>>>($"/customers/{testCustomer.Id}/orders", parameters);
        object mockResult = TestHelpers.GetApiPagedResult(new List<Order> { testOrder });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.GetOrdersForCustomer(testCustomer.Id.Value, page, pageSize);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
        });
    }
    
    [TestMethod]
    public void Customer_GetAddressesForCustomer_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        var testCustomerAddress = new CustomerAddress();
        var page = 1;
        var pageSize = 5;
        NameValueCollection parameters = new NameValueCollection
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<CustomerAddress>>>($"/customers/{testCustomer.Id}/addresses", parameters);
        object mockResult = TestHelpers.GetApiPagedResult(new List<CustomerAddress> { testCustomerAddress });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.GetAddressesForCustomer(testCustomer.Id.Value, page, pageSize);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
        });
    }

    [TestMethod]
    public void Customer_AddAddressToCustomer_Test()
    {
        var testCustomer = new Customer { Id = 4711 };
        var testCustomerAddress = new CustomerAddress { Id = 4712, CustomerId = testCustomer.Id };
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<CustomerAddress>>($"/customers/{testCustomer.Id}/addresses", testCustomerAddress, null);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.AddAddressToCustomer(testCustomerAddress);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Customer_GetCustomerAddress_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<CustomerAddress>>($"/customers/addresses/{testCustomerAddress.Id}", null);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.GetCustomerAddress(testCustomerAddress.Id.Value);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Customer_UpdateCustomerAddress_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<CustomerAddress>>($"/customers/addresses/{testCustomerAddress.Id}", testCustomerAddress, null);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.UpdateCustomerAddress(testCustomerAddress);
            Assert.IsNotNull(result.Data);
        });
    }
    
    
    [TestMethod]
    public void Customer_PatchCustomerAddress_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };

        var fieldsToPatch = new Dictionary<string, string>
        {
            { "FirstName", "Foo" }
        };
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Patch<ApiResult<CustomerAddress>>($"/customers/addresses/{testCustomerAddress.Id}", null, fieldsToPatch);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerEndPoint(restClient);
            var result = uut.PatchCustomerAddress(testCustomerAddress.Id.Value, fieldsToPatch);
            Assert.IsNotNull(result.Data);
        });
    }
}